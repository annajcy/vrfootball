using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : SingletonBase<CanvasManager>
{
    private Dictionary<string, BaseCanvas> canvases = new Dictionary<string, BaseCanvas>();
    private Dictionary<string, bool> canvasState = new Dictionary<string, bool>();
    private bool disableUI = false;

    public T Get<T>() where T : BaseCanvas
    {
        string canvasName = typeof(T).Name;
        if (!canvases.ContainsKey(canvasName)) return null;
        return canvases[canvasName] as T;
    }

    public void Add<T>(T canvas) where T : BaseCanvas
    {
        string canvasName = canvas.gameObject.name;
        if (canvases.ContainsKey(canvasName)) return;
        canvases.Add(canvasName, canvas);
    }

    public void Remove<T>(T canvas) where T : BaseCanvas
    {
        string canvasName = canvas.gameObject.name;
        if (!canvases.ContainsKey(canvasName)) return;
        canvases.Remove(canvasName);
    }

    private void StoreCanvasState()
    {
        canvasState.Clear();
        foreach (var canvas in canvases)
            canvasState.Add(canvas.Key, canvas.Value.IsActive());
    }

    private void SetCanvasState()
    {
        foreach (var state in canvasState)
        {
            var canvas = canvases[state.Key];
            if (state.Value) canvases[state.Key].Show();
            else canvas.Hide();
        }
    }

    public void HideAllUI()
    {
        StoreCanvasState();
        foreach (var canvas in canvases)
            canvas.Value.Hide();
        disableUI = true;
    }

    public void RestoreAllUI()
    {
        if (!disableUI) return;
        SetCanvasState();
        disableUI = false;
    }

    public void ToggleAllUI()
    {
        if (disableUI) RestoreAllUI();
        else HideAllUI();
    }

}
