using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : MonoBehaviour
{
    [SerializeField] private float multiplyer = 0.1f;
    private Vector3 lastPosition;
    private Vector3 speed;

    private void OnTriggerEnter(Collider other)
    {
        other.attachedRigidbody.AddForce(speed * multiplyer, ForceMode.Impulse);
    }

    void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        speed = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }
}
