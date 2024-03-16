using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CanvasSet : MonoBehaviour
{
    public List<BaseCanvas> canvasList;
    public List<bool> defaultState;

    private Dictionary<string, int> canvasIndexDict;
    private List<bool> lastHiddenState;
    private bool isHidden = false;

    private List<bool> GetNowState()
    {
        List<bool> result = new List<bool>();
        foreach (var canvas in canvasList)
            result.Add(canvas.IsActive);
        return result;
    }

    protected virtual void Awake()
    {
        InitIndex();
        SetState(defaultState);
    }

    public void SetState(List<bool> state)
    {
        for (int i = 0; i < state.Count; i++)
        {
            if (state[i]) canvasList[i].Show();
            else canvasList[i].Hide();
        }
    }

    public void RestoreAllCanvas()
    {
        if (!isHidden) return;
        isHidden = false;
        SetState(lastHiddenState);
    }

    public void HideAllCanvas()
    {
        if (isHidden) return;
        isHidden = true;
        lastHiddenState = GetNowState();
        foreach (var canvas in canvasList)
            canvas.Hide();
    }

    public void ToggleAll()
    {
        if (isHidden) RestoreAllCanvas();
        else HideAllCanvas();
    }

    private void InitIndex()
    {
        canvasIndexDict = new Dictionary<string, int>();
        for (int i = 0; i < canvasList.Count; i++)
            canvasIndexDict.Add(canvasList[i].name, i);
    }

    public T GetCanvas<T>() where T : BaseCanvas
    {
        string canvasName = typeof(T).Name;
        if (canvasIndexDict.TryGetValue(canvasName, out int index))
            return canvasList[index] as T;
        return null;
    }
}