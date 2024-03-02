using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventData<T> : IEventData
{
    private UnityAction<T> actions;
    public EventData(UnityAction<T> action) { actions += action; }
    public void AddAction(UnityAction<T> action) { actions += action; }
    public void RemoveAction(UnityAction<T> action) { actions -= action; }
    public void Invoke(T param) { actions?.Invoke(param); }
    public void Clear() { actions = null; }
}

public class EventData : IEventData
{
    private UnityAction actions;
    public EventData(UnityAction action) { actions += action; }
    public void AddAction(UnityAction action) { actions += action; }
    public void RemoveAction(UnityAction action) { actions -= action; }
    public void Invoke() { actions?.Invoke(); }
    public void Clear() { actions = null; }
}


