using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PenaltyGamePauseCanvas : BaseCanvas
{
    public Button quitButton;
    public Button resumeButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(OnUpgradeButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnDestroy()
    {
        resumeButton.onClick.RemoveListener(OnUpgradeButtonClicked);
        quitButton.onClick.RemoveListener(OnQuitButtonClicked);
    }

    private void OnUpgradeButtonClicked()
    {
        PenaltyGameManager.Instance().GameStart();
    }

    private void OnQuitButtonClicked()
    {
        PenaltyGameManager.Instance().GameQuit();
    }
}
