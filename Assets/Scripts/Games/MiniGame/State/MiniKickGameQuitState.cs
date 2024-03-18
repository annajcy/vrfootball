using System.Collections.Generic;

public class MiniKickGameQuitState : BaseState
{
    private List<bool> defaultCanvasState = new List<bool>()
    {
        true, false, false, false, false
    };

    public override void OnEnter()
    {
        CanvasSetManager.Instance().GetCanvasSet<MiniKickGameCanvasSet>().SetState(defaultCanvasState);
        MiniKickGameManager.Instance().GameQuit();
    }

    public override void OnUpdate() { }

    public override void OnQuit() { }
}