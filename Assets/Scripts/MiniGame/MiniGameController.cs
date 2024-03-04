using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.Serialization;

public enum MiniGameState
{
    PAUSE,
    RUNNING
}

public enum LevelCheckState
{
    PASS,
    MAXED,
    FAILED
}

public class MiniGameController : MonoBehaviour
{
    public static MiniGameController Instance;

    public Transform ballRespawnTransform;
    public Transform goalnetRespawnTransform;
    public Transform ballTransform;
    public Transform goalnetTransform;
    public GoalMove goalMove;

    public MiniKickScoreCanvas miniKickScoreCanvas;
    public MiniKickTimeCanvas miniKickTimeCanvas;
    public MiniKickStartCanvas miniKickStartCanvas;
    public MiniKickContinueCanvas miniKickContinueCanvas;
    public MiniKickPauseCanvas miniKickPauseCanvas;

    public Dictionary<int, GameInfo> levelInfo = new Dictionary<int, GameInfo>();

    private int maxLevel = 0;
    private int lastScore = 0;
    private int score = 0;
    private int level = 1;
    private float timeRemaining = 0f;

    private GameInfo curGameInfo;

    private MiniGameState state = MiniGameState.PAUSE;

    private void Awake()
    {
        Instance = this;

        InitDictionary();
        EventManager.Instance().AddEventListener("OnBallScored", OnBallScored);

        miniKickContinueCanvas.btnContinue.onClick.AddListener(GameContinue);
        miniKickContinueCanvas.btnRetry.onClick.AddListener(GameRetry);
        miniKickContinueCanvas.btnQuit.onClick.AddListener(GameQuit);

        miniKickStartCanvas.btnStart.onClick.AddListener(GameStart);

        miniKickPauseCanvas.btnQuit.onClick.AddListener(GameQuit);
        miniKickPauseCanvas.btnResume.onClick.AddListener(GameResume);
        miniKickPauseCanvas.btnRetry.onClick.AddListener(GameRetry);
    }

    void InitAllUI()
    {
        CanvasManager.Instance().Get<MiniKickScoreCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickStartCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickTimeCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickContinueCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickPauseCanvas>().Hide();
    }

    void HideAllUI()
    {
        CanvasManager.Instance().Get<MiniKickScoreCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickStartCanvas>().Show();
        CanvasManager.Instance().Get<MiniKickTimeCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickContinueCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickPauseCanvas>().Hide();
    }

    void Start()
    {
        Respawn();
        InitAllUI();
        UpdateGameInfo();
        UpdateAllUI();
        Pause();
    }

    private void Update()
    {
        if (state == MiniGameState.PAUSE) return;
        timeRemaining -= Time.deltaTime;
        miniKickTimeCanvas.textTimeRemaining.text = ((int)timeRemaining).ToString();
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            GoalNetRespawn();
            BallRespawn();
            CanvasManager.Instance().Get<BodyTrackerSettingCanvas>().Hide();
            CanvasManager.Instance().Get<GameSelectCanvas>().Hide();
            CanvasManager.Instance().Get<MiniKickContinueCanvas>().Show();
            CanvasManager.Instance().Get<MiniKickPauseCanvas>().Hide();
            CanvasManager.Instance().Get<MiniKickStartCanvas>().Hide();
            CanvasManager.Instance().Get<MiniKickTimeCanvas>().Show();
            CanvasManager.Instance().Get<MiniKickScoreCanvas>().Show();
            Pause();
            var nowState = GetTimerUpState();
            if (nowState == LevelCheckState.PASS)
            {
                miniKickContinueCanvas.textTitle.text = "通过level " + level;
            }
            else if (nowState == LevelCheckState.FAILED)
            {
                miniKickContinueCanvas.textTitle.text = "未通过level " + level;
                miniKickContinueCanvas.btnContinue.gameObject.SetActive(false);
            }
            else if (nowState == LevelCheckState.MAXED)
            {
                miniKickContinueCanvas.textTitle.text = "已通关, 得分是: " + score;
                miniKickContinueCanvas.btnContinue.gameObject.SetActive(false);
            }
        }
    }

    private void OnDestroy()
    {
        EventManager.Instance().RemoveEventListener("OnBallScored", OnBallScored);

        miniKickContinueCanvas.btnContinue.onClick.RemoveListener(GameContinue);
        miniKickContinueCanvas.btnRetry.onClick.RemoveListener(GameRetry);
        miniKickContinueCanvas.btnQuit.onClick.RemoveListener(GameQuit);

        miniKickStartCanvas.btnStart.onClick.RemoveListener(GameStart);

        miniKickPauseCanvas.btnQuit.onClick.RemoveListener(GameQuit);
        miniKickPauseCanvas.btnResume.onClick.RemoveListener(GameResume);
        miniKickPauseCanvas.btnRetry.onClick.RemoveListener(GameRetry);
    }

    public void Pause()
    {
        state = MiniGameState.PAUSE;
        goalMove.isEnabled = false;
    }

    public void Resume()
    {
        state = MiniGameState.RUNNING;
        goalMove.isEnabled = true;
    }

    private void UpdateGameInfo()
    {
        curGameInfo = levelInfo[level];
        timeRemaining = curGameInfo.timeDuration;
        goalMove.goalMovement = curGameInfo.goalMovement;
    }

    public void GamePause()
    {
        if (state == MiniGameState.PAUSE) return;
        CanvasManager.Instance().Get<BodyTrackerSettingCanvas>().Hide();
        CanvasManager.Instance().Get<GameSelectCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickContinueCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickPauseCanvas>().Show();
        CanvasManager.Instance().Get<MiniKickStartCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickTimeCanvas>().Show();
        CanvasManager.Instance().Get<MiniKickScoreCanvas>().Show();
        Pause();
    }

    private void GameResume()
    {
        CanvasManager.Instance().Get<BodyTrackerSettingCanvas>().Hide();
        CanvasManager.Instance().Get<GameSelectCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickContinueCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickPauseCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickStartCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickTimeCanvas>().Show();
        CanvasManager.Instance().Get<MiniKickScoreCanvas>().Show();
        Resume();
    }

    private void GameStart()
    {
        level = 1;
        score = 0;
        Respawn();
        UpdateGameInfo();
        UpdateAllUI();
        CanvasManager.Instance().Get<BodyTrackerSettingCanvas>().Hide();
        CanvasManager.Instance().Get<GameSelectCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickContinueCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickPauseCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickStartCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickTimeCanvas>().Show();
        CanvasManager.Instance().Get<MiniKickScoreCanvas>().Show();
        Resume();
    }

    private void GameContinue()
    {
        lastScore = score;
        level++;
        Respawn();
        UpdateGameInfo();
        UpdateAllUI();
        CanvasManager.Instance().Get<BodyTrackerSettingCanvas>().Hide();
        CanvasManager.Instance().Get<GameSelectCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickContinueCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickPauseCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickStartCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickTimeCanvas>().Show();
        CanvasManager.Instance().Get<MiniKickScoreCanvas>().Show();
        Resume();
    }

    private void GameRetry()
    {
        score = lastScore;
        Respawn();
        UpdateGameInfo();
        UpdateAllUI();
        CanvasManager.Instance().Get<BodyTrackerSettingCanvas>().Hide();
        CanvasManager.Instance().Get<GameSelectCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickContinueCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickPauseCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickStartCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickTimeCanvas>().Show();
        CanvasManager.Instance().Get<MiniKickScoreCanvas>().Show();
        Resume();
    }

    public void GameQuit()
    {
        level = 1;
        score = 0;
        Respawn();
        UpdateGameInfo();
        UpdateAllUI();
        CanvasManager.Instance().Get<BodyTrackerSettingCanvas>().Show();
        CanvasManager.Instance().Get<GameSelectCanvas>().Show();
        CanvasManager.Instance().Get<MiniKickContinueCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickPauseCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickStartCanvas>().Show();
        CanvasManager.Instance().Get<MiniKickTimeCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickScoreCanvas>().Hide();
        Pause();
    }

    public void Respawn()
    {
        BallRespawn();
        GoalNetRespawn();
    }

    public void BallRespawn()
    {
        ballTransform.position = ballRespawnTransform.position;
    }

    public void GoalNetRespawn()
    {
        goalnetTransform.position = goalnetRespawnTransform.position;
    }

    private void OnBallScored()
    {
        score ++;
        miniKickScoreCanvas.scoreText.text = score.ToString();
    }

    void UpdateAllUI()
    {
        miniKickScoreCanvas.scoreText.text = "分数：" + score.ToString();
        miniKickScoreCanvas.levelText.text = "等级：" + level.ToString();
        miniKickScoreCanvas.requiredScoreText.text = "目标分数：" + curGameInfo.requiredScore.ToString();
        miniKickTimeCanvas.textTimeRemaining.text = ((int)timeRemaining).ToString();
    }

    private LevelCheckState GetTimerUpState()
    {
        if (score < curGameInfo.requiredScore) return LevelCheckState.FAILED;
        if (level + 1 > maxLevel) return LevelCheckState.MAXED;
        return LevelCheckState.PASS;
    }

    public void InitDictionary()
    {
        levelInfo.Add(1, new GameInfo(10, 0, 0,
            new List<Vector3>()
            {
                new Vector3(0, 0, 0),
            }));
        levelInfo.Add(2, new GameInfo(10, 0, 1,
            new List<Vector3>()
            {
                new Vector3(1, 0, 0),
                new Vector3(-1, 0, 0),
            }));
        levelInfo.Add(3, new GameInfo(10, 0, 2,
            new List<Vector3>()
            {
                new Vector3(1, 0, 0),
                new Vector3(-1, 0, 0),
                new Vector3(0, 0, -2),
            }));
        maxLevel = levelInfo.Keys.Max();
    }

}
