using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BodyTrackerCalibrationCanvas : BaseCanvas
{
    public Button calibrateButton;
    public Button skipCalibrationButton;
    public Button exitButton;

    private void Awake()
    {
        skipCalibrationButton.onClick.AddListener(OnSkipCalibrationButtonClicked);
        calibrateButton.onClick.AddListener(OnCalibratedButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnDestroy()
    {
        skipCalibrationButton.onClick.RemoveListener(OnSkipCalibrationButtonClicked);
        calibrateButton.onClick.RemoveListener(OnCalibratedButtonClicked);
        exitButton.onClick.RemoveListener(OnExitButtonClicked);
    }

    private void OnCalibratedButtonClicked()
    {
        BodyTrackerManager.Instance().GetStateMachine().ChangeState<BodyTrackerCalibratingState>();
    }

    private void OnSkipCalibrationButtonClicked()
    {
        BodyTrackerManager.Instance().GetStateMachine().ChangeState<BodyTrackerPlayingState>();
    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
    }

}