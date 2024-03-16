using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyTrackerUncalibratedState : BaseState
{
    private List<bool> defaultCanvasState = new List<bool>() { true, false, false ,false };

    public override void OnEnter()
    {
        CanvasSetManager.Instance().GetCanvasSet<BodyTrackerCanvasSet>().SetState(defaultCanvasState);
        BodyTrackerManager.Instance().HideAvatar();

        var connectState = BodyTrackerManager.Instance().GetConnectState();
        var trackingState = BodyTrackerManager.Instance().GetTrackingState();

#if UNITY_EDITOR
        connectState = 2;
        trackingState = 0;
#endif

        CanvasSetManager.Instance().GetCanvasSet<BodyTrackerCanvasSet>().
            GetCanvas<BodyTrackerCalibratingCanvas>().
            skipCalibrationBtn.gameObject.
            SetActive(connectState == 2 && trackingState == 0);
    }

    public override void OnUpdate() { }

    public override void OnQuit() { }
}