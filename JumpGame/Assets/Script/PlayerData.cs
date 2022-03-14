using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;
    int maxScore;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);
    }

    public void SetScore(int score)
    {
        if (PlayerPrefs.GetInt("maxScore") > score) return;

        PlayerPrefs.SetInt("maxScore", score);
        PlayerPrefs.Save();
    }

    public int GetScore()
    {
        return PlayerPrefs.GetInt("maxScore"); 
    }

    

}
