using System.Collections.Generic;

public class PenaltyGamePauseState : BaseState
{
    private List<bool> defaultCanvasState = new List<bool>()
    {
        true, true, false, true, false
    };
    public override void OnEnter()
    {
        CanvasSetManager.Instance().GetCanvasSet<PenaltyGameCanvasSet>().SetState(defaultCanvasState);
    }

    public override void OnUpdate() { }

    public override void OnQuit() { }
}