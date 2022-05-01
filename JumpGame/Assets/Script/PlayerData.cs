using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerArray
{
    public string name;
    public Image[] images;
    public GameObject selectButton;

    public void OpenCharacter()
    {
        selectButton.SetActive(true);
        int len = images.Length;

        for(int i=0;i<len;i++)
        {
            images[i].color = Color.white;
        }
    }
}

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;
    public PlayerArray[] playerImage;
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

    private void Start()
    {
        OpenCharacter();
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


    public void OpenCharacter()
    {
        int len = playerImage.Length;
        int score = GetScore();
        if (score >= 50 && score < 100) //pain
        {
            for (int i = 0; i < len; i++)
            {
                if (playerImage[i].name.Equals("PainDust"))
                {
                    playerImage[i].OpenCharacter();
                    break;
                }
            }
        }

        if (score >= 100) //heart
        {
            for (int i = 0; i < len; i++)
            {
                if (playerImage[i].name.Equals("HeartDust"))
                {
                    playerImage[i].OpenCharacter();
                    break;
                }
            }
        }

        if (GetplayingNum() >= 100) //chick
        {
            for (int i = 0; i < len; i++)
            {
                if (playerImage[i].name.Equals("ChickDust"))
                {
                    playerImage[i].OpenCharacter();
                    break;
                }
            }
        }


        if (GetusingShildNum() >= 50) // baby
        {
            for (int i = 0; i < len; i++)
            {
                if (playerImage[i].name.Equals("BabyDust"))
                {
                    playerImage[i].OpenCharacter();
                    break;
                }
            }
        }


    }



    

}
