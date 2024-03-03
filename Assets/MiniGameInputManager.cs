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
    public MiniGameController miniGameController;
    void Start()
    {
        pauseGame.action.started += OnGamePause;
    }

    private void OnGamePause(InputAction.CallbackContext obj)
    {
        miniGameController.OnGamePauseClicked();
    }

    private void OnDestroy()
    {
        pauseGame.action.started -= OnGamePause;
    }
}
