using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSetManager : SingletonMonoGameObject<CanvasSetManager>
{
    public List<CanvasSet> canvasSetList;
    private Dictionary<string, int> canvasSetIndexDict;

    protected override void Awake()
    {
        base.Awake();
        InitIndex();
    }

    private void InitIndex()
    {
        canvasSetIndexDict = new Dictionary<string, int>();
        for (int i = 0; i < canvasSetList.Count; i++)
            canvasSetIndexDict.Add(canvasSetList[i].name, i);
    }

    public T GetCanvasSet<T>() where T : CanvasSet
    {
        string canvasSetName = typeof(T).Name;
        if (canvasSetIndexDict.TryGetValue(canvasSetName, out var canvasSetIndex))
            return canvasSetList[canvasSetIndex] as T;
        return null;
    }

    private void OnDestroy()
    {
        canvasSetList.Clear();
    }
}