using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PenaltyGameSettingCanvas : BaseCanvas
{
    public TextMeshProUGUI kickRemainingTimeText;
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
        PenaltyGameManager.Instance().GamePause();
    }

    public void UpdateKickRemainingTimeText(float time)
    {
        kickRemainingTimeText.text = "剩余时间: " + (int)time + "s";
    }
}
