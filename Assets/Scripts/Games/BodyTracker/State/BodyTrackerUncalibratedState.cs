using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyTrackerUncalibratedState : BaseState
{
    private List<bool> defaultCanvasState = new List<bool>() { true, false, false ,false, true };

    public override void OnEnter()
    {
        CanvasSetManager.Instance().GetCanvasSet<BodyTrackerCanvasSet>().SetState(defaultCanvasState);
        CanvasSetManager.Instance().GetCanvasSet<BodyTrackerCanvasSet>().
            GetCanvas<BodyTrackerCalibrationCanvas>().
            skipCalibrationButton.gameObject.
            SetActive(BodyTrackerManager.Instance().IsStateValid());
        BodyTrackerManager.Instance().HideAvatar();
    }

    public override void OnUpdate() { }

    public override void OnQuit() { }
}