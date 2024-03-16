using System;
using UnityEngine;
using UnityEngine.UI;

public class BodyTrackerCalibratingCanvas : BaseCanvas
{
    public Button calibrateBtn;
    public Button skipCalibrationBtn;
    public Button exitBtn;

    private void Awake()
    {
        skipCalibrationBtn.onClick.AddListener(OnSkipCalibrationButtonClicked);
        calibrateBtn.onClick.AddListener(OnCalibratedButtonClicked);
        exitBtn.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnDestroy()
    {
        skipCalibrationBtn.onClick.RemoveListener(OnSkipCalibrationButtonClicked);
        calibrateBtn.onClick.RemoveListener(OnCalibratedButtonClicked);
        exitBtn.onClick.RemoveListener(OnExitButtonClicked);
    }

    private void OnCalibratedButtonClicked()
    {
        BodyTrackerManager.Instance().stateMachine.ChangeState<BodyTrackerCalibratingState>();
    }

    private void OnSkipCalibrationButtonClicked()
    {
        BodyTrackerManager.Instance().stateMachine.ChangeState<BodyTrackerPlayingState>();
    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
    }

}