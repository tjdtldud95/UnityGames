using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int maxScore = 0;
    public void SetScore(int score)
    {
        if (maxScore > score) return;

        maxScore = score;
    }

    public int GetScore()
    {
        return maxScore; 
    }
}
