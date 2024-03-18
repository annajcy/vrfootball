using System;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Transform ballRespawnTransform;

    public void Respawn()
    {
        transform.position = ballRespawnTransform.position;
        transform.rotation = ballRespawnTransform.rotation;
    }
}