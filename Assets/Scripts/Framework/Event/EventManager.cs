using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : SingletonBase<EventManager>
{
    private readonly Dictionary<string, IEventData> events = new();

    public void AddEventListener<T>(string name, UnityAction<T> action)
    {
        if (events.TryGetValue(name, out var entry))
            (entry as EventData<T>)?.AddAction(action);
        else 
            events.Add(name, new EventData<T>(action));
    }
    
    public void EventTrigger(string name)
    {
        if (events.TryGetValue(name, out var entry))
            (entry as EventData)?.Invoke();
    }

    public void RemoveEventListener(string name, UnityAction action)
    {
        if (events.TryGetValue(name, out var entry))
            (entry as EventData)?.RemoveAction(action);
    }
    
    public void EventTrigger<T>(string name, T param)
    {
        if (events.TryGetValue(name, out var entry))
            (entry as EventData<T>)?.Invoke(param);
    }
    
    public void AddEventListener(string name, UnityAction action)
    {
        if (events.TryGetValue(name, out var entry))
            (entry as EventData)?.AddAction(action);
        else 
            events.Add(name, new EventData(action));
    }

    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if (events.TryGetValue(name, out var entry))
            (entry as EventData<T>)?.RemoveAction(action);
    }
    
    public void Clear() { events.Clear(); }
}
