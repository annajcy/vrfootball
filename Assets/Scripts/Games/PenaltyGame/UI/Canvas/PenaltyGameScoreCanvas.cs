using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PenaltyGameScoreCanvas : BaseCanvas
{
    public TextMeshProUGUI scoreText;

    public void UpdateScoreText(int userScore, int aiScore)
    {
        scoreText.text = userScore + "   :   " + aiScore;
    }
}
