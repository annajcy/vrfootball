using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyTrackerGameSelectCanvas : BaseCanvas
{
    public Button miniKickGameButton;
    public Button penaltyButton;
    public Button passingButton;
    public Button goalKeepingButton;
    public Button freeKickButton;
    public Button ladderTrainButton;

    public Transform miniKickGameTransform;
    public Transform penaltyTransform;
    public Transform passingTransform;
    public Transform goalKeepingTransform;
    public Transform freeKickTransform;
    public Transform ladderTrainTransform;

    public WindowFollow windowFollow;

    private void Awake()
    {
        miniKickGameButton.onClick.AddListener(OnMiniKickGameButtonClicked);
        penaltyButton.onClick.AddListener(OnPenaltyButtonClicked);
    }

    private void OnDestroy()
    {
        miniKickGameButton.onClick.RemoveListener(OnMiniKickGameButtonClicked);
        penaltyButton.onClick.RemoveListener(OnPenaltyButtonClicked);
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
