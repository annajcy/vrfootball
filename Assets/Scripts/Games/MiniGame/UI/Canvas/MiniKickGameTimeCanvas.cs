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

    private void Awake()
    {
        pauseButton.onClick.AddListener(OnPauseButtonClicked);
    }

    private void OnDestroy()
    {
        pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
    }

    private void OnPauseButtonClicked()
    {
        MiniKickGameManager.Instance().GetStateMachine().ChangeState<MiniKickGamePauseState>();
    }
}
