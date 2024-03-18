using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BodyTrackerSettingCanvas : BaseCanvas
{
    public Button showMirrorButton;
    public Button alignGroundButton;
    public Button recalibrateButton;
    public Button exitButton;

    private void Awake()
    {
        showMirrorButton.onClick.AddListener(OnShowMirrorButtonClicked);
        alignGroundButton.onClick.AddListener(OnAlignGroundButtonClicked);
        recalibrateButton.onClick.AddListener(OnRecalibratedButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnDestroy()
    {
        showMirrorButton.onClick.RemoveListener(OnShowMirrorButtonClicked);
        alignGroundButton.onClick.RemoveListener(OnAlignGroundButtonClicked);
        recalibrateButton.onClick.RemoveListener(OnRecalibratedButtonClicked);
        exitButton.onClick.RemoveListener(OnExitButtonClicked);
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
        BodyTrackerManager.Instance().GetStateMachine().ChangeState<BodyTrackerCalibratingState>();
    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
    }

}