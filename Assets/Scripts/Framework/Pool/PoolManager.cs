using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoolManager : SingletonBase<PoolManager>
{
    private readonly Dictionary<string, PoolData> pools = new();
    public GameObject PoolGameObject;
    
    public GameObject Pop(string name)
    {
        GameObject gameObject;
        if (pools.ContainsKey(name) && pools[name].PoolList.Count > 0)
            gameObject = pools[name].Pop();
        else
            gameObject = ResourceManager.Instance().Load<GameObject>(name);
        return gameObject;
    }
    
    public void PopAsync(string name, UnityAction<GameObject> action)
    {
        if (pools.ContainsKey(name) && pools[name].PoolList.Count > 0)
            action.Invoke(pools[name].Pop());
        else
            ResourceManager.Instance().LoadAsync<GameObject>(name, action.Invoke);
    }

    public void Push(GameObject gameObject)
    {
        if (PoolGameObject == null) PoolGameObject = new GameObject("PoolManager");
        if (pools.TryGetValue(gameObject.name, out var pool))
            pool.Push(gameObject);
        else 
            pools.Add(gameObject.name, new PoolData(gameObject));
    }

    public void Clear()
    {
        pools.Clear();
        PoolGameObject = null;
    }
}
