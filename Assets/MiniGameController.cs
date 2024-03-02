using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MiniGameController : MonoBehaviour
{
    public Transform respawnTransform;
    public GameObject ball;
    public MiniKickScoreCanvas miniKickScoreCanvas;
    public DetectScore detectScore;
    private int score = 0;

    public void BallRespawn()
    {
        ball.transform.position = respawnTransform.position;
    }

    public void ResetGame()
    {
        BallRespawn();
    }

    void Start()
    {
        detectScore.onBallScored += OnBallScored;
        miniKickScoreCanvas.scoreText.text = score.ToString();
    }

    private void OnBallScored()
    {
        score ++;
        miniKickScoreCanvas.scoreText.text = score.ToString();
    }

    private void OnDestroy()
    {
        detectScore.onBallScored -= OnBallScored;
    }
}
