using System;
using System.Collections;
using System.Collections.Generic;
using Pico.Platform;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR;

public class BodyTrackingManager : MonoBehaviour
{
    public enum LegTrackingDemoState
    {
        START,
        CALIBRATING,
        CALIBRATED,
        PLAYING
    }

    public MiniGameController miniGameController;
    public static BodyTrackingManager Instance;
    private static List<XRInputSubsystem> inputSubsystems = new();

    public GameObject XROrigin;
    public GameObject Avatar;

    [HideInInspector] public LegTrackingDemoState CurrentLegTrackingDemoState;
    private BodyTrackerSampler bodyTrackerSampler;
    private float startFootHeight;
    private float startXROriginY;
    private float initXROriginY;
    private Transform avatarLeftFoot;

    private GameObject avatarObj;
    private Transform avatarRightFoot;
    private int leftFootStepOnAction;
    private int leftFootStepOnLastAction;
    private int rightFootStepOnAction;
    private int rightFootStepOnLastAction;

    private bool swiftCalibratedState;
    private float avatarScale;

    private void Awake()
    {
        Instance = this;
        initXROriginY = startXROriginY = XROrigin.transform.localPosition.y;
        CoreService.Initialize();
    }

    // Start is called before the first frame update
    private void Start()
    {
        Avatar.SetActive(false);
        SubsystemManager.GetInstances(inputSubsystems);
        foreach (var t in inputSubsystems) t.TryRecenter();
        UpdateFitnessBandState();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
            UpdateFitnessBandState();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            UpdateFitnessBandState();
    }

    private void StartGame(float height)
    {
        CurrentLegTrackingDemoState = LegTrackingDemoState.CALIBRATED;

        var xrOriginPos = XROrigin.transform.localPosition;
        xrOriginPos.y = startXROriginY = initXROriginY;
        XROrigin.transform.localPosition = xrOriginPos;

        //load avatar
        StartCoroutine(LoadAvatar(height));
    }

    public void AlignGround()
    {
        if (avatarObj == null)
        {
            Debug.LogError("There is no loaded avatar!");
            return;
        }

        startFootHeight = Mathf.Min(avatarLeftFoot.transform.position.y, avatarRightFoot.transform.position.y);
        var xrOriginPos = XROrigin.transform.localPosition;
        xrOriginPos.y = startXROriginY + -(startFootHeight - bodyTrackerSampler.soleHeight);
        XROrigin.transform.localPosition = xrOriginPos;
        startXROriginY = xrOriginPos.y;

        Debug.Log($"BodyTrackingManager.AlignGround: StartFootHeight = {startFootHeight}, xrOriginPos = {xrOriginPos}");
    }

    [ContextMenu("LoadAvatar")]
    public void StartGame()
    {
        try
        {
            var task = SportService.GetUserInfo();
            if (task != null)
                task.OnComplete(rsp =>
                {
                    if (!rsp.IsError)
                    {
                        if (rsp.Data.Stature > 50)
                            PlayerPrefManager.Instance.PlayerPrefData.height = rsp.Data.Stature;

                        Debug.Log($"SportService.GetUserInfo: Success, Height = {rsp.Data.Stature}");
                    }
                    else
                        Debug.LogWarning($"SportService.GetUserInfo: Failed, msg = {rsp.Error}");

                    StartGame(PlayerPrefManager.Instance.PlayerPrefData.height);
                });
            else
                StartGame(PlayerPrefManager.Instance.PlayerPrefData.height);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
            StartGame(PlayerPrefManager.Instance.PlayerPrefData.height);
        }
    }

    private IEnumerator LoadAvatar(float height)
    {
        if (height <= 50)
        {
            height = 175;
            Debug.LogWarning($"LoadAvatar: Height = {height} is too small, it be set to 175, please check!");
        }

        SubsystemManager.GetInstances(inputSubsystems);
        foreach (var t in inputSubsystems) t.TryRecenter();

        avatarObj = Avatar;
        avatarObj.transform.localScale = Vector3.one;
        avatarObj.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        avatarObj.SetActive(true);

        bodyTrackerSampler = avatarObj.GetComponent<BodyTrackerSampler>();

        leftFootStepOnAction = leftFootStepOnLastAction = bodyTrackerSampler.LeftTouchGroundAction;
        rightFootStepOnAction = rightFootStepOnLastAction = bodyTrackerSampler.RightTouchGroundAction;

        avatarLeftFoot = bodyTrackerSampler.BonesList[10];
        avatarRightFoot = bodyTrackerSampler.BonesList[11];

        avatarScale = height / 175;
        bodyTrackerSampler.UpdateBonesLength(avatarScale);

        Avatar.SetActive(false);
        yield return new WaitForEndOfFrame();
        AlignGround();

        Avatar.SetActive(true);
        CanvasManager.Instance().Get<BodyTrackerSettingCanvas>().Show();
        CanvasManager.Instance().Get<GameSelectCanvas>().Show();
        CanvasManager.Instance().Get<MiniKickStartCanvas>().Show();
        CurrentLegTrackingDemoState = LegTrackingDemoState.PLAYING;

        Debug.Log($"BodyTrackingManager.LoadAvatar: Avatar = {avatarObj.name}, height = {height}");
    }

    private void UpdateFitnessBandState()
    {
        PXR_Input.SetSwiftMode(PlayerPrefManager.Instance.PlayerPrefData.bodyTrackMode);

        //Update Swift calibration state after resuming
        var calibrated = -1;
        PXR_Input.GetFitnessBandCalibState(ref calibrated);
        swiftCalibratedState = calibrated == 1;
        if (swiftCalibratedState)
        {
            CanvasManager.Instance().Get<StartupCanvas>().Hide();
            StartGame();
            Debug.Log($"BodyTrackingManager.UpdateFitnessBandState: calibrated = {calibrated}");
        }
        else
        {
            if (avatarObj != null && avatarObj.activeSelf) avatarObj.SetActive(false);

            var connectState = new PxrFitnessBandConnectState();
            PXR_Input.GetFitnessBandConnectState(ref connectState);

            BodyTrackerResult bodyTrackerResult = new BodyTrackerResult();
            var trackingState = PXR_Input.GetBodyTrackingPose(0, ref bodyTrackerResult);

#if UNITY_EDITOR
                connectState.num = 2;
                trackingState = 0;
#endif

            miniGameController.GameQuit();

            CanvasManager.Instance().Get<TrainSelectCanvas>().Hide();
            CanvasManager.Instance().Get<GameSelectCanvas>().Hide();
            CanvasManager.Instance().Get<BodyTrackerSettingCanvas>().Hide();
            CanvasManager.Instance().Get<MiniKickStartCanvas>().Hide();
            CanvasManager.Instance().Get<MiniKickScoreCanvas>().Hide();
            CanvasManager.Instance().Get<MiniKickTimeCanvas>().Hide();
            CanvasManager.Instance().Get<MiniKickPauseCanvas>().Hide();
            CanvasManager.Instance().Get<MiniKickContinueCanvas>().Hide();

            var startupCanvas = CanvasManager.Instance().Get<StartupCanvas>();
            startupCanvas.Show();
            startupCanvas.
                btnContinue.gameObject
                .SetActive(connectState.num == 2 && trackingState == 0);

            Debug.Log(
                $"BodyTrackingManager.UpdateFitnessBandState: connectedNum = {connectState.num}, trackingState = {trackingState}");
        }
    }

    public void HideAvatar()
    {
        if (avatarObj != null)
            avatarObj.SetActive(false);
    }
}