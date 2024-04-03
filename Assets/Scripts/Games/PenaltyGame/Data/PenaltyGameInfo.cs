using System;
using UnityEngine.Events;

[Serializable]
public class PenaltyGameInfo
{
    public int aiScore = 0;
    public int playerScore = 0;

    public UnityEvent<int, int> onGoal;

    public void Clear()
    {
        aiScore = 0;
        playerScore = 0;
    }

    public void aiGoal()
    {
        aiScore++;
        onGoal?.Invoke(playerScore, aiScore);
    }

    public void playerGoal()
    {
        playerScore++;
        onGoal?.Invoke(playerScore, aiScore);
    }


}