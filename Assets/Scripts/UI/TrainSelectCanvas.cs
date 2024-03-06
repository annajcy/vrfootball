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
    public Button btnMainMenu;

    protected override void Awake()
    {
        base.Awake();
        btnPenalty.onClick.AddListener(Penalty);
        btnPass.onClick.AddListener(Pass);
        btnBack.onClick.AddListener(Back);
        btnMainMenu.onClick.AddListener(MainMenu);
    }

    private void MainMenu()
    {
        
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
        EnvironmentManager.Instance.SetActiveEnvironment(EActiveEnvironment.STADIUM);
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
