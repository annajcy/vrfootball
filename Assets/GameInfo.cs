using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo
{
    public int timeDuration;
    public int requiredScore;
    public GoalMovement goalMovement;

    public GameInfo()
    {
        timeDuration = 60;
        requiredScore = 10;
        goalMovement = new GoalMovement();
    }

    public GameInfo(int timeDuration, int requiredScore, int goalMoveSpeed, List<Vector3> coordinates)
    {
        this.timeDuration = timeDuration;
        this.requiredScore = requiredScore;
        goalMovement = new GoalMovement(goalMoveSpeed, coordinates);
    }
}
