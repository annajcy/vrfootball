using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        InitDictionary();
        EventManager.Instance().AddEventListener("OnBallScored", OnBallScored);

        miniKickContinueCanvas.btnContinue.onClick.AddListener(OnContinueGameClicked);
        miniKickContinueCanvas.btnRetry.onClick.AddListener(OnRetryClicked);
        miniKickContinueCanvas.btnQuit.onClick.AddListener(OnQuitGameClicked);

        miniKickStartCanvas.btnStart.onClick.AddListener(OnStartGameClicked);

        miniKickPauseCanvas.btnQuit.onClick.AddListener(OnQuitGameClicked);
        miniKickPauseCanvas.btnResume.onClick.AddListener(OnGameResumeClicked);
    }

    void Start()
    {
        Respawn();
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
            Pause();
            var nowState = GetTimerUpState();
            if (nowState == LevelCheckState.PASS)
            {
                CanvasManager.Instance().Get<MiniKickContinueCanvas>().Show();
                miniKickContinueCanvas.textTitle.text = "通过level " + (level - 1).ToString() + " !";
            }
            else if (nowState == LevelCheckState.FAILED)
            {
                CanvasManager.Instance().Get<MiniKickContinueCanvas>().Show();
                miniKickContinueCanvas.textTitle.text = "未能通过level " + (level - 1).ToString();
                miniKickContinueCanvas.btnContinue.gameObject.SetActive(false);
            }
            else if (nowState == LevelCheckState.MAXED)
            {
                CanvasManager.Instance().Get<MiniKickContinueCanvas>().Show();
                miniKickContinueCanvas.textTitle.text = "已经完全通关, 得分是: " + score.ToString();
                miniKickContinueCanvas.btnContinue.gameObject.SetActive(false);
            }
        }
    }

    private void OnDestroy()
    {
        EventManager.Instance().RemoveEventListener("OnBallScored", OnBallScored);

        miniKickContinueCanvas.btnContinue.onClick.RemoveListener(OnContinueGameClicked);
        miniKickContinueCanvas.btnRetry.onClick.RemoveListener(OnRetryClicked);
        miniKickContinueCanvas.btnQuit.onClick.RemoveListener(OnQuitGameClicked);

        miniKickStartCanvas.btnStart.onClick.RemoveListener(OnStartGameClicked);

        miniKickPauseCanvas.btnQuit.onClick.RemoveListener(OnQuitGameClicked);
        miniKickPauseCanvas.btnResume.onClick.RemoveListener(OnGameResumeClicked);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        state = MiniGameState.PAUSE;
        goalMove.isEnabled = false;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        state = MiniGameState.RUNNING;
        goalMove.isEnabled = true;
    }

    private void UpdateGameInfo()
    {
        curGameInfo = levelInfo[level];
        timeRemaining = curGameInfo.timeDuration;
        goalMove.goalMovement = curGameInfo.goalMovement;
    }

    public void OnGamePauseClicked()
    {
        CanvasManager.Instance().Get<MiniKickPauseCanvas>().Show();
        Pause();
    }

    private void OnGameResumeClicked()
    {
        CanvasManager.Instance().Get<MiniKickPauseCanvas>().Hide();
        Resume();
    }

    private void OnStartGameClicked()
    {
        level = 1;
        score = 0;
        Respawn();
        UpdateGameInfo();
        UpdateAllUI();
        CanvasManager.Instance().Get<MiniKickStartCanvas>().Hide();
        Resume();
    }

    private void OnContinueGameClicked()
    {
        lastScore = score;
        level++;
        Respawn();
        UpdateGameInfo();
        UpdateAllUI();
        CanvasManager.Instance().Get<MiniKickContinueCanvas>().Hide();
        Resume();
    }

    private void OnRetryClicked()
    {
        score = lastScore;
        Respawn();
        UpdateGameInfo();
        UpdateAllUI();
        CanvasManager.Instance().Get<MiniKickContinueCanvas>().Hide();
        Resume();
    }

    private void OnQuitGameClicked()
    {
        level = 1;
        score = 0;
        Respawn();
        UpdateGameInfo();
        UpdateAllUI();
        CanvasManager.Instance().Get<MiniKickContinueCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickPauseCanvas>().Hide();
        CanvasManager.Instance().Get<MiniKickStartCanvas>().Show();
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
        miniKickScoreCanvas.scoreText.text = score.ToString();
        miniKickScoreCanvas.levelText.text = level.ToString();
        miniKickScoreCanvas.requiredScoreText.text = curGameInfo.requiredScore.ToString();
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
        levelInfo.Add(1, new GameInfo(60, 10, 0,
            new List<Vector3>()
            {
                new Vector3(0, 0, 0),
            }));
        levelInfo.Add(2, new GameInfo(60, 20, 10,
            new List<Vector3>()
            {
                new Vector3(1, 0, 0),
                new Vector3(-1, 0, 0),
            }));
        levelInfo.Add(3, new GameInfo(70, 30, 12,
            new List<Vector3>()
            {
                new Vector3(1, 0, 0),
                new Vector3(-1, 0, 0),
                new Vector3(0, 0, -2),
            }));
        maxLevel = levelInfo.Keys.Max();
    }

}
