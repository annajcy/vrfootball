using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MiniKickGameStartCanvas : BaseCanvas
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
        MiniKickGameManager.Instance().GetStateMachine().ChangeState<MiniKickGamePlayingState>();
    }

}
