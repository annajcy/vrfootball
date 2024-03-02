using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MiniGameController : MonoBehaviour
{
    public Transform respawnTransform;
    public Transform ballTransform;
    public GameObject ball;
    public MiniKickScoreCanvas miniKickScoreCanvas;


    private int score = 0;
    private int level = 1;


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
        EventManager.Instance().AddEventListener("OnBallScored", OnBallScored);
        miniKickScoreCanvas.scoreText.text = score.ToString();
    }

    private void OnBallScored()
    {
        score ++;
        miniKickScoreCanvas.scoreText.text = score.ToString();
    }

    private void OnDestroy()
    {
        EventManager.Instance().RemoveEventListener("OnBallScored", OnBallScored);
    }
}
