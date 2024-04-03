using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PenaltyGameUpgradeCanvas : BaseCanvas
{
    public TextMeshProUGUI titleText;
    public Button upgradeButton;
    public Button quitButton;

    private void Awake()
    {
        upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnDestroy()
    {
        upgradeButton.onClick.RemoveListener(OnUpgradeButtonClicked);
        quitButton.onClick.RemoveListener(OnQuitButtonClicked);
    }

    private void OnUpgradeButtonClicked()
    {

        if (PenaltyGameManager.Instance().isPlayerKick)
        {
            PenaltyGameManager.Instance().isPlayerKick = false;
            PenaltyGameManager.Instance().GameStart();
        }
        else
        {
            PenaltyGameManager.Instance().isPlayerKick = true;
            PenaltyGameManager.Instance().GameStart();
        }
    }

    private void OnQuitButtonClicked()
    {
        PenaltyGameManager.Instance().GameQuit();
    }

}
