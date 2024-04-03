

using System.Collections.Generic;

public class PenaltyGameGoalKeepState : BaseState
{
    private bool state = false;

    private List<bool> defaultCanvasState = new List<bool>()
    {
        true, true, false, false, false
    };

    public override void OnEnter()
    {
        state = false;
        PenaltyGameManager.Instance().goalNetController.
            goalDetectController.EnableBallScoreDetection();
        PenaltyGameManager.Instance().ballController.Respawn();
        PenaltyGameManager.Instance().ballController.Show();
        PenaltyGameManager.Instance().isScored = false;
        PenaltyGameManager.Instance().canvasAnchorMode.SetToGoalKeeper();
        PenaltyGameManager.Instance().strikerController.GenerateRandomGoalTarget();
        PenaltyGameManager.Instance().UpdateUI();
        CanvasSetManager.Instance().GetCanvasSet<PenaltyGameCanvasSet>().
            SetState(defaultCanvasState);
        PenaltyGameManager.Instance().goalkeeperController.Hide();
        PenaltyGameManager.Instance().strikerController.Show();
        PenaltyGameManager.Instance().ResetTimer(2.0f);
    }

    public override void OnUpdate()
    {
        PenaltyGameManager.Instance().UpdateTimer();
        PenaltyGameManager.Instance().UpdateUI();
        if (PenaltyGameManager.Instance().IsTimerUp())
        {
            if (!state)
            {
                PenaltyGameManager.Instance().strikerController.SwitchPose(StrikerAction.Kick);
                PenaltyGameManager.Instance().ResetTimer();
                state = true;
            }
            else PenaltyGameManager.Instance().GameUpgrade();
        }
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