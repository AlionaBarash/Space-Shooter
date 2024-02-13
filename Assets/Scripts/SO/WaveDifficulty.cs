using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveDifficulty", menuName = "Scriptable Objects/Wave Difficulty")]
public class WaveDifficulty : ScriptableObject
{
    [field: SerializeField]
    public int Weight { get; private set; }

    public int[] weightValues;
    public Wave[] waves;

    public void ChangeWeight()
    {
        for (int i = 0; i < weightValues.Length - 1; i++)
        {
            if (Weight == weightValues[i])
            {
                Weight = weightValues[i + 1];
                break;
            }
        }
    }

    public void ResetWeightValues()
    {
        Weight = weightValues[0];
    }
}
