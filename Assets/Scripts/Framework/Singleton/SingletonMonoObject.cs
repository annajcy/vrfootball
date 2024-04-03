using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMonoObject<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T instance;

    protected virtual void Awake()
    {
        instance = this as T;
    }

    public static T Instance()
    {
        return instance as T;
    }
}
