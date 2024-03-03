using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalMove : MonoBehaviour
{
    public GoalMovement goalMovement;
    public bool isEnabled = false;

    private int index = 0;
    private Vector3 start;
    private Vector3 end;

    private float time = 0f;

    private void Update()
    {
        if (!isEnabled) return;
        if (goalMovement == null) return;

        if (index + 1 > goalMovement.coordinates.Count) index = 0;

        start = goalMovement.coordinates[index];
        end = index + 1 == goalMovement.coordinates.Count ? goalMovement.coordinates[0] : goalMovement.coordinates[index + 1];

        time += Time.deltaTime * goalMovement.speed;
        this.transform.position = Vector3.Lerp(start, end, time);

        if (this.transform.position == end)
        {
            time = 0f;
            index++;
        }

    }
}
