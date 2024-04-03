using System.Collections.Generic;

public class PenaltyGameQuitState : BaseState
{
    private List<bool> defaultCanvasState = new List<bool>()
    {
        false, false, false, false, true
    };

    public override void OnEnter()
    {
        CanvasSetManager.Instance().GetCanvasSet<PenaltyGameCanvasSet>().SetState(defaultCanvasState);
        PenaltyGameManager.Instance().ResetInfo();
    }

    public override void OnUpdate() { }

    public override void OnQuit()
    {
        PenaltyGameManager.Instance().ResetTimer();
        PenaltyGameManager.Instance().roundPlayed = 0;
    }
}