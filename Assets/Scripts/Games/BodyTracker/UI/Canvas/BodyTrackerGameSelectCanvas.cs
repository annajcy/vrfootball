using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyTrackerGameSelectCanvas : BaseCanvas
{
    public Button penaltyButton;
    public Button passingButton;
    public Button goalKeepingButton;
    public Button freeKickButton;
    public Button miniKickGameButton;
    public Button ladderTrainButton;

    public WindowFollow windowFollow;
    public Transform penaltyTransform;
    public Transform passingTransform;
    public Transform goalKeepingTransform;
    public Transform freeKickTransform;
    public Transform miniKickGameTransform;
    public Transform ladderTrainTransform;

    private void Awake()
    {
        miniKickGameButton.onClick.AddListener(OnMiniKickGameButtonClicked);
    }

    private void OnDestroy()
    {
        miniKickGameButton.onClick.RemoveListener(OnMiniKickGameButtonClicked);
    }

    private void OnMiniKickGameButtonClicked()
    {
        CanvasSetManager.Instance().GetCanvasSet<BodyTrackerCanvasSet>().HideAllCanvas();
        windowFollow.target = miniKickGameTransform;
    }

}
