using System;
using System.Collections;
using System.Collections.Generic;
using Pico.Platform;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR;

public class BodyTrackerManager : SingletonMonoGameObject<BodyTrackerManager>
{
    public GameObject xrOrigin;
    public GameObject avatar;
    public float height = 175.0f;

    private static List<XRInputSubsystem> inputSubsystems = new();
    private BodyTrackerSampler bodyTrackerSampler;
    private float startFootHeight;
    private float startXROriginY;
    private float initXROriginY;
    private Transform avatarRightFoot;
    private Transform avatarLeftFoot;
    private StateMachine stateMachine;

    protected override void Awake()
    {
        base.Awake();
        InitStateMachine();
        initXROriginY = startXROriginY = xrOrigin.transform.localPosition.y;
    }

    private void Start()
    {
        InitInputSystem();
        stateMachine.ChangeState<BodyTrackerUncalibratedState>();
    }

    private void Update()
    {
        stateMachine.UpdateState();
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

    public StateMachine GetStateMachine()
    {
        return stateMachine;
    }

    private void InitStateMachine()
    {
        stateMachine = new StateMachine();
        stateMachine.AddState<BodyTrackerCalibratingState>();
        stateMachine.AddState<BodyTrackerPlayingState>();
        stateMachine.AddState<BodyTrackerUncalibratedState>();
    }

    private void InitBodyTrackerSampler()
    {
        bodyTrackerSampler = avatar.GetComponent<BodyTrackerSampler>();
        avatarLeftFoot = bodyTrackerSampler.bonesList[10];
        avatarRightFoot = bodyTrackerSampler.bonesList[11];
        bodyTrackerSampler.UpdateBonesLength(GetAvatarScale());
    }

    private void InitInputSystem()
    {
        SubsystemManager.GetInstances(inputSubsystems);
        foreach (var t in inputSubsystems) t.TryRecenter();
    }

    public void StartGame()
    {
        var xrOriginPos = xrOrigin.transform.localPosition;
        xrOriginPos.y = startXROriginY = initXROriginY;
        xrOrigin.transform.localPosition = xrOriginPos;
        StartCoroutine(LoadAvatar());
    }

    public void AlignGround()
    {
        startFootHeight = Mathf.Min(avatarLeftFoot.transform.position.y, avatarRightFoot.transform.position.y);
        var xrOriginPos = xrOrigin.transform.localPosition;
        xrOriginPos.y = startXROriginY + -(startFootHeight - bodyTrackerSampler.soleHeight);
        xrOrigin.transform.localPosition = xrOriginPos;
        startXROriginY = xrOriginPos.y;
    }

    private void InitAvatarTransform()
    {
        avatar.transform.localScale = Vector3.one;
        avatar.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    public void ShowAvatar()
    {
        avatar.SetActive(true);
    }

    public void HideAvatar()
    {
        avatar.SetActive(false);
    }

    private float GetAvatarScale()
    {
        return height / 175;
    }

    private IEnumerator LoadAvatar()
    {
        InitInputSystem();
        InitAvatarTransform();
        InitBodyTrackerSampler();
        HideAvatar();
        yield return new WaitForEndOfFrame();
        AlignGround();
        ShowAvatar();
        Debug.Log($"BodyTrackingManager.LoadAvatar: Avatar = {avatar.name}, height = {height}");
    }

    public bool GetCalibrated()
    {
        int calibrated = -1;
        PXR_Input.GetFitnessBandCalibState(ref calibrated);
        return calibrated == 1;
    }

    private void SetSwiftMode(int mode)
    {
        PXR_Input.SetSwiftMode(mode);
    }

    private int GetConnectState()
    {
        var connectState = new PxrFitnessBandConnectState();
        PXR_Input.GetFitnessBandConnectState(ref connectState);
        return connectState.num;
    }

    private int GetTrackingState()
    {
        BodyTrackerResult bodyTrackerResult = new BodyTrackerResult();
        var trackingState = PXR_Input.GetBodyTrackingPose(0, ref bodyTrackerResult);
        return trackingState;
    }

    public bool IsStateValid()
    {
        var connectState = Instance().GetConnectState();
        var trackingState = Instance().GetTrackingState();

#if UNITY_EDITOR
        connectState = 2;
        trackingState = 0;
#endif

        return connectState == 2 && trackingState == 0;
    }

    public void Calibrate()
    {
        PXR_Input.OpenFitnessBandCalibrationAPP();
    }

    private void UpdateFitnessBandState()
    {
        SetSwiftMode(1);
        if (GetCalibrated())
            stateMachine.ChangeState<BodyTrackerPlayingState>();
        else
            stateMachine.ChangeState<BodyTrackerUncalibratedState>();
    }
}