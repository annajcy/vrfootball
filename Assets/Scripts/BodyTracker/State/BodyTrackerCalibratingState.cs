using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BodyTrackerCalibratingState : BaseState
{
    public override void OnEnter()
    {
        CanvasSetManager.Instance().GetCanvasSet<BodyTrackerCanvasSet>().HideAllCanvas();
        BodyTrackerManager.Instance().Calibrate();
    }

    public override void OnUpdate() { }

    public override void OnQuit()
    {
        CanvasSetManager.Instance().GetCanvasSet<BodyTrackerCanvasSet>().RestoreAllCanvas();
    }

}