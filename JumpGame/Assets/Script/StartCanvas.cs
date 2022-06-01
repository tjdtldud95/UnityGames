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
    public PlayerData data;
    public PlayerArray[] playerImage;
    private void Start()
    {
        string score;
        var ob = transform.Find("Score").GetComponent<TextMeshProUGUI>();
        
        score = data.GetScore().ToString();

        ob.text = score;
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

}
