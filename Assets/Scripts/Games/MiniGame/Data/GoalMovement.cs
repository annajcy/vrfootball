using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GoalMovement
{
    public int speed;
    public List<Transform> coordinates;

    public GoalMovement()
    {
        speed = 10;
        coordinates = new List<Transform>();
    }

    public GoalMovement(int speed, List<Transform> coordinates)
    {
        this.coordinates = new List<Transform>();
        this.speed = speed;
        foreach (var position in coordinates)
            this.coordinates.Add(position);
    }
}
