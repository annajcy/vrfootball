using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.UI;

[RequireComponent(typeof(TrackedDeviceGraphicRaycaster))]
public class BaseCanvas : MonoBehaviour
{
    public UnityEvent onShow;
    public UnityEvent onHide;

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        onShow?.Invoke();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        onHide?.Invoke();
    }

    public void Toggle()
    {
        if (IsActive()) Hide();
        else Show();
    }
}