using System;
using UnityEngine;
using UnityEngine.UI;

public class BodyTrackerSettingCanvas : BaseCanvas
{
    public Button showMirrorBtn;
    public Button alignGroundBtn;
    public Button recalibrateBtn;
    public Button exitBtn;

    private void Awake()
    {
        showMirrorBtn.onClick.AddListener(OnShowMirrorButtonClicked);
        alignGroundBtn.onClick.AddListener(OnAlignGroundButtonClicked);
        recalibrateBtn.onClick.AddListener(OnRecalibratedButtonClicked);
        exitBtn.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnShowMirrorButtonClicked()
    {
        CanvasSetManager.Instance().
            GetCanvasSet<BodyTrackerCanvasSet>().
            GetCanvas<BodyTrackerMirrorCanvas>().
            Toggle();
    }

    private void OnAlignGroundButtonClicked()
    {
        BodyTrackerManager.Instance().AlignGround();
    }

    private void OnRecalibratedButtonClicked()
    {
        BodyTrackerManager.Instance().stateMachine.ChangeState<BodyTrackerCalibratingState>();
    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        showMirrorBtn.onClick.RemoveListener(OnShowMirrorButtonClicked);
        alignGroundBtn.onClick.RemoveListener(OnAlignGroundButtonClicked);
        recalibrateBtn.onClick.RemoveListener(OnRecalibratedButtonClicked);
        exitBtn.onClick.RemoveListener(OnExitButtonClicked);
    }
}