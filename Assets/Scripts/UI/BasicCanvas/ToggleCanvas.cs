using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.UI;

[RequireComponent(typeof(TrackedDeviceGraphicRaycaster))]
[RequireComponent(typeof(WindowFollow))]
public class ToggleCanvas : BaseCanvas
{
    public CanvasSet canvasSet;
    public Transform showTransform;
    public Transform hideTransform;
    public Button toggleControllerButton;

    private WindowFollow windowFollow;

    protected virtual void Awake()
    {
        windowFollow = GetComponent<WindowFollow>();
        toggleControllerButton.onClick.AddListener(OnToggleControllerButtonClicked);
        canvasSet.onShow.AddListener(OnCanvasShow);
        canvasSet.onHide.AddListener(OnCanvasHide);
    }

    protected void OnDestroy()
    {
        toggleControllerButton.onClick.RemoveListener(OnToggleControllerButtonClicked);
        canvasSet.onShow.RemoveListener(OnCanvasShow);
        canvasSet.onHide.RemoveListener(OnCanvasHide);
    }

    private void OnCanvasHide()
    {
        windowFollow.target = hideTransform;
    }

    private void OnCanvasShow()
    {
        windowFollow.target = showTransform;
    }
    
    private void OnToggleControllerButtonClicked()
    {
        canvasSet.ToggleAllCanvas();
    }
}