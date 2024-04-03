using System.Collections.Generic;
using UnityEngine;

public class PenaltyGameUpgradeState : BaseState
{
    private List<bool> defaultCanvasState = new List<bool>()
    {
        true, true, true, false, false
    };

    public override void OnEnter()
    {
        PenaltyGameManager.Instance().roundPlayed++;
        PenaltyGameManager.Instance().UpdateUI();
        CanvasSetManager.Instance().GetCanvasSet<PenaltyGameCanvasSet>().SetState(defaultCanvasState);
        var title = CanvasSetManager.Instance().
            GetCanvasSet<PenaltyGameCanvasSet>()
            .GetCanvas<PenaltyGameUpgradeCanvas>()
            .titleText;

        if (PenaltyGameManager.Instance().isPlayerKick)
            title.text = PenaltyGameManager.Instance().isScored ? "进球了" : "没有进球";
        else
        {
            title.text = PenaltyGameManager.Instance().isScored ? "没有守住" : "守住了";
            PenaltyGameManager.Instance().canvasAnchorMode.SetToStriker();
        }

        var winner = PenaltyGameManager.Instance().CheckWinner();
        var upgradeButton = CanvasSetManager.Instance().
            GetCanvasSet<PenaltyGameCanvasSet>().
            GetCanvas<PenaltyGameUpgradeCanvas>().upgradeButton.gameObject;

        upgradeButton.SetActive(winner == 0);

        if (winner == 1) title.text = "你赢了";
        else if (winner == 2) title.text = "你输了";

    }

    public override void OnUpdate() { }

    public override void OnQuit()
    {
        PenaltyGameManager.Instance().ResetTimer();
    }
}