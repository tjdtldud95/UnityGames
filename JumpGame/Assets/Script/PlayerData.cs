using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;
    public bool goCheckPoint;
    int playingNum;
    int maxScore;
    int usingShildNum;

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

    public void SetplayingNum()
    {
        PlayerPrefs.SetInt("playingNum", playingNum + 1);
        PlayerPrefs.Save();
    }

    public int GetplayingNum()
    {
        return PlayerPrefs.GetInt("playingNum");
    }

    public void SetusingShildNum(int num)
    {
        PlayerPrefs.SetInt("usingShildNum", usingShildNum+num);
        PlayerPrefs.Save();
    }

    public int GetusingShildNum()
    {
        return PlayerPrefs.GetInt("usingShildNum");
    }

    

}
