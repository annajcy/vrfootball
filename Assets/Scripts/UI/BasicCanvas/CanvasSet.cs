using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class CanvasSet : MonoBehaviour
{
    public List<BaseCanvas> canvasList;
    public List<BaseCanvas> hideAllExclusionList;
    public List<bool> defaultState;
    public UnityEvent onShow;
    public UnityEvent onHide;

    private Dictionary<string, int> canvasIndexDict;
    private List<bool> lastHiddenState;
    private bool isShow = true;

    protected virtual void Awake()
    {
        InitIndex();
        SetState(defaultState);
    }

    public bool IsShow()
    {
        return isShow;
    }

    private List<bool> GetNowState()
    {
        List<bool> result = new List<bool>();
        foreach (var canvas in canvasList)
            result.Add(canvas.IsActive());
        return result;
    }

    public void SetState(List<bool> state)
    {
        for (int i = 0; i < state.Count; i++)
        {
            if (state[i]) canvasList[i].Show();
            else canvasList[i].Hide();
        }
        onShow?.Invoke();
    }

    public void ShowAllCanvas()
    {
        if (isShow) return;
        onShow?.Invoke();
        isShow = true;
        SetState(lastHiddenState);
    }

    public void HideAllCanvas()
    {
        if (!isShow) return;
        onHide?.Invoke();
        isShow = false;
        lastHiddenState = GetNowState();
        foreach (var canvas in canvasList)
            if (!hideAllExclusionList.Contains(canvas))
                canvas.Hide();
    }

    public void ForceHideAllCanvas()
    {
        if (!isShow)
        {
            onHide?.Invoke();
            foreach (var canvas in canvasList)
                canvas.Hide();
            return;
        }

        onHide?.Invoke();
        isShow = false;
        lastHiddenState = GetNowState();
        foreach (var canvas in canvasList)
            canvas.Hide();
    }

    public void ToggleAllCanvas()
    {
        if (isShow) HideAllCanvas();
        else ShowAllCanvas();
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