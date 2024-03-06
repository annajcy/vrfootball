using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.XR.PXR;

public class StartupCanvas : BaseCanvas
{
    public Button btnContinue;
    public Button btnCalibrate;
    public Button btnExit;

    protected override void Awake()
    {
        base.Awake();
        btnContinue.onClick.AddListener(() =>
        {
            CanvasManager.Instance().Get<StartupCanvas>().Hide();
            BodyTrackingManager.Instance.StartGame();
        });
        btnCalibrate.onClick.AddListener(() =>
        {
            CanvasManager.Instance().Get<StartupCanvas>().Hide();
            PXR_Input.OpenFitnessBandCalibrationAPP();
            BodyTrackingManager.Instance.CurrentLegTrackingDemoState =
                ELegTrackingDemoState.CALIBRATING;
        });
        btnExit.onClick.AddListener(Application.Quit);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        btnContinue.onClick.RemoveAllListeners();
        btnCalibrate.onClick.RemoveAllListeners();
        btnExit.onClick.RemoveAllListeners();
    }

}