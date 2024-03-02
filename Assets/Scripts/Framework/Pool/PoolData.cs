using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolData
{
    private readonly GameObject fatherGameObject;
    public readonly List<GameObject> PoolList;

    public PoolData(GameObject gameObject)
    {
        fatherGameObject = new GameObject(gameObject.name)
        { transform = { parent = PoolManager.Instance().PoolGameObject.transform } };
        PoolList = new List<GameObject>();
        Push(gameObject);
    }

    public void Push(GameObject gameObject)
    {
        PoolList.Add(gameObject);
        gameObject.transform.parent = fatherGameObject.transform;
        gameObject.SetActive(false);
    }

    public GameObject Pop(Transform newTransform = null)
    {
        var gameObject = PoolList.First();
        PoolList.RemoveAt(0);
        gameObject.SetActive(true);
        gameObject.transform.parent = newTransform;
        return gameObject;
    }
}
