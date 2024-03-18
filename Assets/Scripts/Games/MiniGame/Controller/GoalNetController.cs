using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GoalNetController : MonoBehaviour
{
    private bool isMovementEnabled = false;
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

        if (transform.position == end)
        {
            time = 0f;
            index++;
        }
    }

    public void Respawn()
    {
        transform.position = goalNetRespawnTransform.position;
        transform.rotation = goalNetRespawnTransform.rotation;
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
