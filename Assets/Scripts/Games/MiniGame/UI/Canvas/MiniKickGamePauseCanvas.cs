using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class MiniKickGamePauseCanvas : BaseCanvas
{
    public Button retryButton;
    public Button quitButton;
    public Button resumeButton;

    private void Awake()
    {
        retryButton.onClick.AddListener(OnRetryButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
        resumeButton.onClick.AddListener(OnResumeButtonClicked);
    }

    private void OnDestroy()
    {
        retryButton.onClick.RemoveListener(OnRetryButtonClicked);
        quitButton.onClick.RemoveListener(OnQuitButtonClicked);
        resumeButton.onClick.RemoveListener(OnResumeButtonClicked);
    }

    private void OnResumeButtonClicked()
    {
        MiniKickGameManager.Instance().GetStateMachine().ChangeState<MiniKickGamePlayingState>();
    }

    private void OnQuitButtonClicked()
    {
        MiniKickGameManager.Instance().GetStateMachine().ChangeState<MiniKickGameQuitState>();
    }

    private void OnRetryButtonClicked()
    {
        MiniKickGameManager.Instance().GameRetry();
        MiniKickGameManager.Instance().GetStateMachine().ChangeState<MiniKickGamePlayingState>();
    }
}
