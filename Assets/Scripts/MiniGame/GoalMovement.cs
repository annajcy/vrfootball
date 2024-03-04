using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalMovement
{
    public int speed;
    public List<Vector3> coordinates = new List<Vector3>();

    public GoalMovement()
    {
        speed = 10;
        coordinates.Add(Vector3.zero);
    }

    public GoalMovement(int speed, List<Vector3> coordinates)
    {
        this.speed = speed;
        foreach (var position in coordinates)
            this.coordinates.Add(position);
    }
}
