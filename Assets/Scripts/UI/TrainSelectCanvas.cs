using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class TrainSelectCanvas : BaseCanvas
{
    public Button btnPenalty;
    public Button btnBack;
    public Button btnPass;

    protected override void Awake()
    {
        base.Awake();
        btnPenalty.onClick.AddListener(Penalty);
        btnPass.onClick.AddListener(Pass);
        btnBack.onClick.AddListener(Back);
    }

    private void Start()
    {
        CanvasManager.Instance().Get<TrainSelectCanvas>().Hide();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        btnBack.onClick.RemoveListener(Back);
        btnPenalty.onClick.RemoveListener(Penalty);
        btnPass.onClick.RemoveListener(Pass);
    }

    private void Penalty()
    {
        EnvironmentManager.Instance.SetActiveEnvironment(ActiveEnvironment.STADIUM);
        MiniGameController.Instance.gameObject.SetActive(false);
    }

    private void Pass()
    {

    }

    private void Back()
    {
        CanvasManager.Instance().Get<TrainSelectCanvas>().Hide();
    }
}
