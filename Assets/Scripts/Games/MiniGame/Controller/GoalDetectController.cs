using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoalDetectController : MonoBehaviour
{
    public UnityEvent onBallScored;
    private bool IsOnLeft(Vector3 a, Vector3 b)
    {
        a.y = 0;
        b.y = 0;
        return Vector3.Dot(a, b) > 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Ball") return;
        if (IsOnLeft(this.transform.forward, other.gameObject.transform.position - this.transform.position))
            onBallScored?.Invoke();
    }
}
