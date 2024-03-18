using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MiniKickGameTimeCanvas : BaseCanvas
{
    public TextMeshProUGUI timeRemainingText;
    public Button pauseButton;
    public Button respawnButton;

    private void Awake()
    {
        pauseButton.onClick.AddListener(OnPauseButtonClicked);
        respawnButton.onClick.AddListener(OnRespawnButtonClicked);
    }

    private void OnDestroy()
    {
        pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
        respawnButton.onClick.RemoveListener(OnRespawnButtonClicked);
    }

    private void OnPauseButtonClicked()
    {
        MiniKickGameManager.Instance().GetStateMachine().ChangeState<MiniKickGamePauseState>();
    }

    private void OnRespawnButtonClicked()
    {
        MiniKickGameManager.Instance().RespawnBall();
    }
}
