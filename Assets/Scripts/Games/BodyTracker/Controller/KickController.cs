using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Kick : MonoBehaviour
{
    private float multiplier = 0.1f;
    private Vector3 lastPosition;
    private Vector3 speed;

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.attachedRigidbody.AddForce(speed * multiplier, ForceMode.Impulse);
    }

    private void Update()
    {
        speed = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }
}
