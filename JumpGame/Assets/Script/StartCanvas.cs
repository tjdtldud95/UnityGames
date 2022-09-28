using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerArray
{
    public string name;
    public Image[] images;
    public bool isOpen;
    public GameObject howOpen;
    public GameObject introduce;
    public void OpenCharacter()
    {
        int len = images.Length;
        for (int i = 0; i < len; i++)
        {
            images[i].color = Color.white;
        }
        howOpen.SetActive(false);
        introduce.SetActive(true);
        isOpen = true;
    }
}

public class StartCanvas : MonoBehaviour
{
    public PlayerArray[] playerImage;
    public TextMeshProUGUI[] ob;
    public GameObject[] CheckPoint;
    private void Start()
    {
        if (PlayerData.instance.isClear)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }

        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        string score;
        score = PlayerData.instance.GetScore().ToString();
        if(PlayerData.instance.isClear)
            ob[1].text = score;
        else
            ob[0].text = score;
        OpenCharacter();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            SystemDown();
    }
    public void GoInScene()
    {
        SceneManager.LoadScene("InGame");
    }


    public void SystemDown()
    {
        Application.Quit();
    }


    public void OpenCharacter()
    {
        int len = playerImage.Length;
        int score = PlayerData.instance.GetScore();
        if (score >= 5 && score <= 10) //pain
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

        if (score >= 10) //heart
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

        if (PlayerData.instance.GetplayingNum() >= 100) //chick
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


        if (PlayerData.instance.GetusingShildNum() >= 50) // baby
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


    public void OpenCheckPointimage()
    {
        int Countindex = PlayerData.instance.GetcheckPointCount();
        int arrIndex = 0;
        if (PlayerData.instance.isClear)
            arrIndex = 1;
        Debug.Log(Countindex);
        Transform ob = CheckPoint[arrIndex].transform.GetChild(Countindex);
        ob.gameObject.SetActive(true);
    }

}
