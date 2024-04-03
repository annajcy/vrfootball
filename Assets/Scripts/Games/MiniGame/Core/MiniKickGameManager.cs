using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms.Impl;

public class MiniKickGameManager : StateMachineObject<MiniKickGameManager>
{
    public GoalNetController goalNetController;
    public BallController ballController;
    public List<MiniKickGameInfo> levelInfoList;

    private int lastScore = 0;
    private int score = 0;
    private int level = 1;
    private float timeRemaining = 0f;
    private MiniKickGameInfo curMiniKickGameInfo;

    protected override void Awake()
    {
        base.Awake();
        goalNetController.goalDetectController.onBallScored.AddListener(OnBallScored);
    }

    private void Start()
    {
        stateMachine.ChangeState<MiniKickGameQuitState>();
    }

    private void OnDestroy()
    {
        goalNetController.goalDetectController.onBallScored.RemoveListener(OnBallScored);
    }

    protected override void AddStates()
    {
        stateMachine.AddState<MiniKickGameQuitState>();
        stateMachine.AddState<MiniKickGamePauseState>();
        stateMachine.AddState<MiniKickGamePlayingState>();
        stateMachine.AddState<MiniKickGameUpgradeState>();
    }

    private void EnableScoreDetection()
    {
        goalNetController.goalDetectController.EnableBallScoreDetection();
        goalNetController.EnableMovement();
    }

    private void DisableScoreDetection()
    {
        goalNetController.goalDetectController.DisableBallScoreDetection();
        goalNetController.DisableMovement();
    }

    public float GetRemainingTime()
    {
        return timeRemaining;
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetScore()
    {
        return score;
    }

    public void GamePause()
    {
        DisableScoreDetection();
    }

    public void GameStart()
    {
        Respawn();
        EnableScoreDetection();
    }

    public void GameUpgrade()
    {
        Respawn();
        EnableScoreDetection();
        UpgradeValue();
        UpdateGameInfo();
    }

    public void GameRetry()
    {
        Respawn();
        EnableScoreDetection();
        RollBackValue();
        UpdateGameInfo();
    }

    public void GameQuit()
    {
        Respawn();
        DisableScoreDetection();
        ResetValue();
        UpdateGameInfo();
    }

    public void Respawn()
    {
        RespawnBall();
        RespawnGoalNet();
    }

    public void RespawnBall()
    {
        ballController.Respawn();
    }

    public void RespawnGoalNet()
    {
        goalNetController.Respawn();
    }

    private int GetListIndex()
    {
        return level - 1;
    }

    private void ResetValue()
    {
        lastScore = 0;
        score = 0;
        level = 1;
    }

    private void RollBackValue()
    {
        score = lastScore;
    }

    private void UpgradeValue()
    {
        lastScore = score;
        level++;
    }

    public void UpdateRemainingTime()
    {
        timeRemaining -= Time.deltaTime;
    }

    private void UpdateGameInfo()
    {
        curMiniKickGameInfo = levelInfoList[GetListIndex()];
        timeRemaining = curMiniKickGameInfo.timeDuration;
        goalNetController.goalMovement = curMiniKickGameInfo.goalMovement;
    }

    public void UpdateUI()
    {
        var canvasSet = CanvasSetManager.Instance().GetCanvasSet<MiniKickGameCanvasSet>();
        var scoreCanvas = canvasSet.GetCanvas<MiniKickGameScoreCanvas>();
        var timeCanvas = canvasSet.GetCanvas<MiniKickGameTimeCanvas>();
        scoreCanvas.scoreText.text = "分数：" + score;
        scoreCanvas.levelText.text = "等级：" + level;
        scoreCanvas.requiredScoreText.text = "目标分数：" + curMiniKickGameInfo.requiredScore;
        timeCanvas.timeRemainingText.text = "倒计时：" + (int)timeRemaining + "s";
    }

    private void OnBallScored()
    {
        score ++;
        UpdateUI();
    }

    public LevelCheckState GetTimerUpState()
    {
        if (score < curMiniKickGameInfo.requiredScore) return LevelCheckState.Failed;
        if (level == levelInfoList.Count) return LevelCheckState.Maxed;
        return LevelCheckState.Pass;
    }

    [ContextMenu(nameof(InitInfoList))]
    public void InitInfoList()
    {
        levelInfoList = new List<MiniKickGameInfo>
        {
            new MiniKickGameInfo(10, 0, 0, new List<Transform>()),
            new MiniKickGameInfo(10, 0, 1, new List<Transform>()),
            new MiniKickGameInfo(10, 0, 2, new List<Transform>())
        };
    }
}
