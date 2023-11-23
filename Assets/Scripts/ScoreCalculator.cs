using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreCalculator
{
    public static List<int> topScore = new List<int>();

    private int _tempScoreValue;


    public void AddToTopScore(int score) 
    {
        if (score < topScore[^1])
        {
            return;
        }

        for (int i = 0; i < topScore.Count; i++)
        {
            if (score == topScore[i])
            {
                break;
            }
            else if (score > topScore[i] && i == topScore.Count - 1)
            {
                topScore[i] = score;

                break;
            }
            else if (score > topScore[i])
            {
                _tempScoreValue = topScore[i];

                topScore[i] = score;

                topScore.Insert(i + 1, _tempScoreValue);

                topScore.Remove(topScore[^1]);

                break;
            }
        }
    }
}





