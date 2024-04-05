using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Kick : MonoBehaviour
{
    private float multiplier = 1f;
    private Vector3 lastPosition;
    private Vector3 speed;

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(speed + "Kicked");
        BallController ballController = other.GetComponent<BallController>();
        if (ballController == null) return;
        ballController.onBallKicked?.Invoke(speed);
        ballController.ApplyForce(speed * multiplier);
    }

    private void Update()
    {
        var position = transform.position;
        speed = (position - lastPosition) / Time.deltaTime;
        lastPosition = position;
    }
}
