using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoalDetectController : MonoBehaviour
{
    public UnityEvent onBallScored;
    private bool isDetectScore = true;

    private static bool IsOnLeft(Vector3 a, Vector3 b)
    {
        a.y = 0;
        b.y = 0;
        return Vector3.Dot(a, b) > 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Scored");
        if (!isDetectScore) return;
        BallController ballController = other.GetComponent<BallController>();
        if (ballController == null) return;
        // if (IsOnLeft(this.transform.forward, other.gameObject.transform.position - this.transform.position))
        //     return;
        onBallScored?.Invoke();
    }

    public void EnableBallScoreDetection()
    {
        isDetectScore = true;
    }

    public void DisableBallScoreDetection()
    {
        isDetectScore = false;
    }
}
