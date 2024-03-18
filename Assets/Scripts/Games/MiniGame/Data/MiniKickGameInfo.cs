using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MiniKickGameInfo
{
    public int timeDuration;
    public int requiredScore;
    public GoalMovement goalMovement;

    public MiniKickGameInfo()
    {
        timeDuration = 60;
        requiredScore = 10;
        goalMovement = new GoalMovement();
    }

    public MiniKickGameInfo(int timeDuration, int requiredScore, int goalMoveSpeed, List<Transform> coordinates)
    {
        this.timeDuration = timeDuration;
        this.requiredScore = requiredScore;
        goalMovement = new GoalMovement(goalMoveSpeed, coordinates);
    }
}
