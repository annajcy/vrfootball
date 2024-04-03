using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PenaltyGameStartCanvas : BaseCanvas
{
    public Button startButton;

    private void Awake()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    private void OnDestroy()
    {
        startButton.onClick.RemoveListener(OnStartButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        PenaltyGameManager.Instance().GameStart();
    }
}
