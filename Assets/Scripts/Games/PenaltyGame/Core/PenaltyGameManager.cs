using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class PenaltyGameManager : StateMachineObject<PenaltyGameManager>
{
    public GoalkeeperController goalkeeperController;
    public StrikerController strikerController;
    public BallController ballController;
    public GoalNetController goalNetController;
    public CanvasAnchorMode canvasAnchorMode;
    public PenaltyGameInfo penaltyGameInfo;

    public bool isPlayerKick = true;
    public bool isScored = false;
    private float kickTimer = 5.0f;
    public int roundPlayed = 0;

    protected override void Awake()
    {
        base.Awake();
        goalNetController.goalDetectController.
            onBallScored.AddListener(OnBallScored);
        ballController.
            onBallKicked.AddListener(OnBallKicked);
    }

    private void Start()
    {
        ballController.Hide();
        goalkeeperController.Hide();
        strikerController.Hide();
        GameQuit();
    }

    private void OnDestroy()
    {
        goalNetController.goalDetectController.
            onBallScored.RemoveListener(OnBallScored);
        ballController.
            onBallKicked.RemoveListener(OnBallKicked);
    }

    protected override void AddStates()
    {
        GetStateMachine().AddState<PenaltyGameQuitState>();
        GetStateMachine().AddState<PenaltyGameUpgradeState>();
        GetStateMachine().AddState<PenaltyGameKickState>();
        GetStateMachine().AddState<PenaltyGameGoalKeepState>();
        GetStateMachine().AddState<PenaltyGamePauseState>();
    }

    public bool IsTimerUp()
    {
        return kickTimer <= 0;
    }

    public void ResetTimer(float time = 5.0f)
    {
        kickTimer = time;
    }

    public void UpdateTimer()
    {
        kickTimer -= Time.deltaTime;
    }

    public float GetKickTimer()
    {
        return kickTimer;
    }

    public void ResetInfo()
    {
        isPlayerKick = true;
        penaltyGameInfo.Clear();
    }

    public void UpdateUI()
    {
        var canvasSet = CanvasSetManager.Instance().GetCanvasSet<PenaltyGameCanvasSet>();
        var scoreCanvas = canvasSet.GetCanvas<PenaltyGameScoreCanvas>();
        var settingCanvas = canvasSet.GetCanvas<PenaltyGameSettingCanvas>();
        settingCanvas.UpdateKickRemainingTimeText(GetKickTimer());
        scoreCanvas.UpdateScoreText(penaltyGameInfo.playerScore, penaltyGameInfo.aiScore);
    }

    private void OnBallScored()
    {
        if (stateMachine.GetNowStateName() == "PenaltyGameKickState")
        {
            penaltyGameInfo.playerGoal();
        }
        else if (stateMachine.GetNowStateName() == "PenaltyGameGoalKeepState")
        {
            penaltyGameInfo.aiGoal();
        }

        isScored = true;
        GameUpgrade();
    }

    private void OnBallKicked(Vector3 speed)
    {
        goalkeeperController.React(speed);
    }

    public void GameQuit()
    {
        GetStateMachine().ChangeState<PenaltyGameQuitState>();
    }

    public void GameStart()
    {
        if (isPlayerKick) GetStateMachine().ChangeState<PenaltyGameKickState>();
        else GetStateMachine().ChangeState<PenaltyGameGoalKeepState>();
    }

    public void GameUpgrade()
    {
        GetStateMachine().ChangeState<PenaltyGameUpgradeState>();
    }

    public void GamePause()
    {
        GetStateMachine().ChangeState<PenaltyGamePauseState>();
    }

    public int CheckWinner()
    {
        int r = 10 - roundPlayed;
        int c1 = penaltyGameInfo.playerScore, c2 = penaltyGameInfo.aiScore;

        if (r < 0)
        {
            if (roundPlayed % 2 == 0)
            {
                if (c1 < c2) return 2;
                else if (c1 > c2) return 1;
                else return 0;
            }

            return 0;
        }

        if (c1 == c2) return 0;

        if (c1 < c2)
        {
            c1 += r / 2;
            if (c1 < c2) return 2;
            return 0;
        }
        else
        {
            c2 += r / 2 + r % 2;
            if (c2 < c1) return 1;
            return 0;
        }
    }
}