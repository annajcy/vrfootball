using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MiniKickGameUpgradeCanvas : BaseCanvas
{
    public TextMeshProUGUI titleText;
    public Button upgradeButton;
    public Button retryButton;
    public Button quitButton;

    private void Awake()
    {
        upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
        retryButton.onClick.AddListener(OnRetryButtonButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnDestroy()
    {
        upgradeButton.onClick.RemoveListener(OnUpgradeButtonClicked);
        retryButton.onClick.RemoveListener(OnRetryButtonButtonClicked);
        quitButton.onClick.RemoveListener(OnQuitButtonClicked);
    }

    private void OnQuitButtonClicked()
    {
        MiniKickGameManager.Instance().GetStateMachine().ChangeState<MiniKickGameQuitState>();
    }

    private void OnRetryButtonButtonClicked()
    {
        MiniKickGameManager.Instance().GameRetry();
        MiniKickGameManager.Instance().GetStateMachine().ChangeState<MiniKickGamePlayingState>();
    }

    private void OnUpgradeButtonClicked()
    {
        MiniKickGameManager.Instance().GameUpgrade();
        MiniKickGameManager.Instance().GetStateMachine().ChangeState<MiniKickGamePlayingState>();
    }
}
