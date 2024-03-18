using System.Collections.Generic;

public class MiniKickGamePauseState : BaseState
{
    private List<bool> defaultCanvasState = new List<bool>()
    {
        false, false, true, true, true
    };

    public override void OnEnter()
    {
        CanvasSetManager.Instance().GetCanvasSet<MiniKickGameCanvasSet>().SetState(defaultCanvasState);
        MiniKickGameManager.Instance().GamePause();
    }

    public override void OnUpdate() { }

    public override void OnQuit() { }
}