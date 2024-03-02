using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MiniGameInputManager : MonoBehaviour
{
    public InputActionReference resetGame;
    public MiniGameController miniGameController;
    void Start()
    {
        resetGame.action.started += OnGameReset;
    }

    private void OnGameReset(InputAction.CallbackContext obj)
    {
        miniGameController.ResetGame();
    }

    private void OnDestroy()
    {
        resetGame.action.started -= OnGameReset;
    }
}
