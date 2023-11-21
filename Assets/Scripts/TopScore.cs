using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopScore 
{
    static public List<int> topScore = new List<int>(new int[3]);

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

    public void SaveTopScoreResults()
    {
        for (int i = 0; i < topScore.Count; i++)
        {
            PlayerPrefs.SetInt($"{i + 1} place", topScore[i]);
        }
    }
}
