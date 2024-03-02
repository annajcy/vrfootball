using System;
using TMPro;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.UI;

public class GameSelectCanvas : BaseCanvas
{
    public Button btnTrain;
    public Button btnCreate;
    public Button btnExit;

    protected override void Awake()
    {
        base.Awake();
        btnTrain.onClick.AddListener(() =>
        {
            CanvasManager.Instance().Get<TrainSelectCanvas>().Show();
        });
        btnExit.onClick.AddListener(Application.Quit);
        btnCreate.onClick.AddListener(() =>
        {

        });
    }

    private void Start()
    {
        CanvasManager.Instance().Get<GameSelectCanvas>().Hide();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        btnCreate.onClick.RemoveAllListeners();
        btnTrain.onClick.RemoveAllListeners();
        btnExit.onClick.RemoveAllListeners();
    }
}