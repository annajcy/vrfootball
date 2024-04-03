using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GoalNetController : MonoBehaviour
{
    public bool isMovementEnabled = false;
    public Transform goalNetRespawnTransform;
    public GoalDetectController goalDetectController;
    public GoalMovement goalMovement;

    private Vector3 start;
    private Vector3 end;
    private int index = 0;
    private float time = 0f;

    private void Update()
    {
        if (!isMovementEnabled) return;
        if (goalMovement == null) return;
        if (index + 1 > goalMovement.coordinates.Count) index = 0;

        start = goalMovement.coordinates[index].position;
        end = index + 1 == goalMovement.coordinates.Count ? goalMovement.coordinates[0].position : goalMovement.coordinates[index + 1].position;

        time += Time.deltaTime * goalMovement.speed;
        transform.position = Vector3.Lerp(start, end, time);

        if (transform.position != end) return;
        time = 0f;
        index++;
    }

    public void Respawn()
    {
        var transform1 = transform;
        transform1.position = goalNetRespawnTransform.position;
        transform1.rotation = goalNetRespawnTransform.rotation;
    }

    public void EnableMovement()
    {
        isMovementEnabled = true;
    }

    public void DisableMovement()
    {
        isMovementEnabled = false;
    }
}
