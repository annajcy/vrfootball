using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayPush : MonoBehaviour
{
    [SerializeField] private int delayTime = 1;
    private void OnEnable() { Invoke(nameof(Push), delayTime); }
    private void Push() { PoolManager.Instance().Push(gameObject); }
}
