using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyTrackerGameSelectCanvas : BaseCanvas
{
    public Button quitButton;
    public Button miniKickGameButton;
    public Button penaltyButton;

    public Transform miniKickGameTransform;
    public Transform penaltyTransform;

    public WindowFollow windowFollow;

    private void Awake()
    {
        miniKickGameButton.onClick.AddListener(OnMiniKickGameButtonClicked);
        penaltyButton.onClick.AddListener(OnPenaltyButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnDestroy()
    {
        miniKickGameButton.onClick.RemoveListener(OnMiniKickGameButtonClicked);
        penaltyButton.onClick.RemoveListener(OnPenaltyButtonClicked);
        quitButton.onClick.RemoveListener(OnQuitButtonClicked);
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    private void OnMiniKickGameButtonClicked()
    {
        CanvasSetManager.Instance().GetCanvasSet<BodyTrackerCanvasSet>().HideAllCanvas();
        windowFollow.target = miniKickGameTransform;
    }

    private void OnPenaltyButtonClicked()
    {
        CanvasSetManager.Instance().GetCanvasSet<BodyTrackerCanvasSet>().HideAllCanvas();
        windowFollow.target = penaltyTransform;
    }

}
