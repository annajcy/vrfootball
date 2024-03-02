using System;
using TMPro;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.UI;

public class BodyTrackerSettingCanvas : BaseCanvas
{
    public Button btnCalibration;
    public Button btnAlignGround;
    public Button btnShowMirror;

    protected override void Awake()
    {
        base.Awake();
        btnCalibration.onClick.AddListener(() =>
        {
            PXR_Input.OpenFitnessBandCalibrationAPP();
            BodyTrackingManager.Instance.CurrentLegTrackingDemoState = BodyTrackingManager.LegTrackingDemoState.CALIBRATING;
            BodyTrackingManager.Instance.HideAvatar();
        });
        btnAlignGround.onClick.AddListener(() =>
        {
            BodyTrackingManager.Instance.AlignGround();
        });
        btnShowMirror.onClick.AddListener(() =>
        {
            CanvasManager.Instance().Get<MirrorCanvas>().ToggleState();
        });
    }

    private void Start()
    {
        CanvasManager.Instance().Get<BodyTrackerSettingCanvas>().Hide();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        btnCalibration.onClick.RemoveAllListeners();
        btnAlignGround.onClick.RemoveAllListeners();
        btnShowMirror.onClick.RemoveAllListeners();
    }
}