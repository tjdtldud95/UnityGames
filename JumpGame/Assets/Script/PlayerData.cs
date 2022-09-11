using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;
    public bool goCheckPoint;
    public bool isClear;
    public int checkPointCount;
    int lostTime;
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

    void Start()
    {
        SetCheckPointTime();
    }
    public void SetScore(int score)
    {
        if (PlayerPrefs.GetInt("maxScore") > score) return;

        if (score <= 1000)
            score = 950;

        PlayerPrefs.SetInt("maxScore", score);
        PlayerPrefs.Save();
    }


    public int GetcheckPointCount()
    {
        return PlayerPrefs.GetInt("checkPointCount");
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

    public void SetlostTime()
    {
        PlayerPrefs.SetInt("lostTime", DateTime.Now.Hour);
        PlayerPrefs.Save();
    }


    void SetCheckPointTime()
    {
        int NowHour = 0, PreHour = 0;

        NowHour = DateTime.Now.Hour;
        PreHour = PlayerPrefs.GetInt("lostTime");

        int loopCount = NowHour - PreHour;
        
        for(int i=2;i<=loopCount;i*=2)
        {
            AddCheckPointCount();
        }
    }

    public void MinorCheckPointCount()
    {
        if (PlayerPrefs.GetInt("checkPointCount") <= 0)
            return;

        if (PlayerPrefs.GetInt("checkPointCount") == 5)
            SetlostTime();

        PlayerPrefs.SetInt("checkPointCount", checkPointCount-1);
        PlayerPrefs.Save();
    }


    public void AddCheckPointCount()
    {
        if (PlayerPrefs.GetInt("checkPointCount") >= 4)
            return;

        PlayerPrefs.SetInt("checkPointCount", checkPointCount + 1);
        PlayerPrefs.Save();
    }
}
