using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public InputActionReference toggleUI;
    void Start()
    {
        toggleUI.action.started += OnUIToggled;
    }

    private void OnUIToggled(InputAction.CallbackContext obj)
    {
       CanvasManager.Instance().ToggleAllUI();
    }

    private void OnDestroy()
    {
        toggleUI.action.started -= OnUIToggled;
    }
}
