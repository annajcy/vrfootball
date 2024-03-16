using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyTrackerPlayingState : BaseState
{
    private List<bool> defaultCanvasState = new List<bool>() { false, true, true, false, true };

    public override void OnEnter()
    {
        CanvasSetManager.Instance().GetCanvasSet<BodyTrackerCanvasSet>().SetState(defaultCanvasState);
        BodyTrackerManager.Instance().StartGame();
    }

    public override void OnUpdate() { }

    public override void OnQuit() { }




}