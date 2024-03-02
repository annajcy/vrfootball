using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoManager : SingletonMono<MonoManager>
{
    private UnityAction awakeEvent;
    private UnityAction startEvent;
    private UnityAction updateEvent;
    private UnityAction onDestroyEvent;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        awakeEvent?.Invoke();
    }

    private void Start() { startEvent?.Invoke(); }
    private void Update() { updateEvent?.Invoke(); }
    private void OnDestroy() { onDestroyEvent?.Invoke(); }

    public void AddUpdateListener(UnityAction action) { updateEvent += action; }
    public void RemoveUpdateListener(UnityAction action) { updateEvent -= action; }
    public void AddStartListener(UnityAction action) { startEvent += action; }
    public void RemoveStartListener(UnityAction action) { startEvent -= action; }
    public void AddOnDestroyListener(UnityAction action) { onDestroyEvent += action; }
    public void RemoveOnDestroyListener(UnityAction action) { onDestroyEvent -= action; }
}


