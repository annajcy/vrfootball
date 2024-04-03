using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PenaltyGameKickState : BaseState
{
    private List<bool> defaultCanvasState = new List<bool>()
    {
        true, true, false, false, false
    };

    public override void OnEnter()
    {
        PenaltyGameManager.Instance().goalNetController.
            goalDetectController.EnableBallScoreDetection();
        PenaltyGameManager.Instance().ballController.Respawn();
        PenaltyGameManager.Instance().ballController.Show();
        PenaltyGameManager.Instance().isScored = false;
        PenaltyGameManager.Instance().UpdateUI();
        CanvasSetManager.Instance().GetCanvasSet<PenaltyGameCanvasSet>().
            SetState(defaultCanvasState);
        PenaltyGameManager.Instance().goalkeeperController.Show();
        PenaltyGameManager.Instance().strikerController.Hide();
    }

    public override void OnUpdate()
    {
        PenaltyGameManager.Instance().UpdateTimer();
        PenaltyGameManager.Instance().UpdateUI();
        if (PenaltyGameManager.Instance().IsTimerUp())
            PenaltyGameManager.Instance().GameUpgrade();
    }

    public override void OnQuit()
    {
        PenaltyGameManager.Instance().goalNetController.
            goalDetectController.DisableBallScoreDetection();
        PenaltyGameManager.Instance().ballController.Hide();
        PenaltyGameManager.Instance().goalkeeperController.Hide();
        PenaltyGameManager.Instance().strikerController.Hide();
    }
}