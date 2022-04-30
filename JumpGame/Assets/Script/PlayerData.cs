using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerArray
{
    public Image[] images;
}


public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;
    public PlayerArray[] playerImage;
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
