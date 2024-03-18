using System.Collections.Generic;
using UnityEngine;

public class MiniKickGamePlayingState : BaseState
{
    private List<bool> defaultCanvasState = new List<bool>()
    {
        false, false, false, true, true
    };

    public override void OnEnter()
    {
        CanvasSetManager.Instance().GetCanvasSet<MiniKickGameCanvasSet>().SetState(defaultCanvasState);
        MiniKickGameManager.Instance().GameStart();
    }

    public override void OnUpdate()
    {
        MiniKickGameManager.Instance().UpdateRemainingTime();
        MiniKickGameManager.Instance().UpdateUI();
        if (MiniKickGameManager.Instance().GetRemainingTime() <= 0)
        {
            MiniKickGameManager.Instance().GamePause();
            MiniKickGameManager.Instance().GetStateMachine().ChangeState<MiniKickGameUpgradeState>();
        }

    }

    public override void OnQuit()
    {

    }
}