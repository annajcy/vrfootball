using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallController : MonoBehaviour
{
    public Transform ballRespawnTransform;
    public UnityEvent<Vector3> onBallKicked;
    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Respawn()
    {
        rigidbody.velocity = Vector3.zero;
        var transform1 = transform;
        transform1.position = ballRespawnTransform.position;
        transform1.rotation = ballRespawnTransform.rotation;
    }

    public void ApplyForce(Vector3 speed)
    {
        rigidbody.AddForce(speed, ForceMode.Impulse);
    }

    public void KickToTarget(Transform target, int strength)
    {
        Vector3 direction = Vector3.Normalize(target.position - transform.position);
        ApplyForce(direction * strength);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}