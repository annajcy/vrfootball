using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class BaseCanvas : MonoBehaviour
{
    private GameObject display;
    protected virtual void Awake()
    {
        CanvasManager.Instance().Add(this);
        display = GetComponentInChildren<VerticalLayoutGroup>().gameObject;
    }

    protected virtual void OnDestroy()
    {
        CanvasManager.Instance().Remove(this);
    }

    public void Hide()
    {
        display.SetActive(false);
    }

    public bool IsActive()
    {
        return display.activeSelf;
    }

    public void ToggleState()
    {
        if (IsActive()) Hide();
        else Show();
    }

    public void Show()
    {
        display.SetActive(true);
    }
}
