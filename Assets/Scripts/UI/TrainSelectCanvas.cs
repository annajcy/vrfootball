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

    protected override void Awake()
    {
        base.Awake();
        btnPenalty.onClick.AddListener(() =>
        {

        });
        btnBack.onClick.AddListener(() =>
        {
            CanvasManager.Instance().Get<TrainSelectCanvas>().Hide();
        });
    }

    private void Start()
    {
        CanvasManager.Instance().Get<TrainSelectCanvas>().Hide();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        btnBack.onClick.RemoveAllListeners();
        btnPenalty.onClick.RemoveAllListeners();
    }
}
