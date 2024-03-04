using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class MiniGameInputManager : MonoBehaviour
{
    public InputActionReference pauseGame;
    public InputActionReference respawnBall;
    void Start()
    {
        pauseGame.action.started += OnGamePause;
        respawnBall.action.started += OnBallRespawn;
    }

    private void OnBallRespawn(InputAction.CallbackContext obj)
    {
        MiniGameController.Instance.BallRespawn();
    }

    private void OnGamePause(InputAction.CallbackContext obj)
    {
        MiniGameController.Instance.GamePause();
    }

    private void OnDestroy()
    {
        pauseGame.action.started -= OnGamePause;
        respawnBall.action.started -= OnBallRespawn;
    }
}
