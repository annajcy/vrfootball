using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMono<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T instance;
    public static T Instance()
    {
        if (instance != null) return instance;
        var gameObject = new GameObject { name = typeof(T).ToString() };
        instance = gameObject.AddComponent<T>();
        return instance;
    }
}
