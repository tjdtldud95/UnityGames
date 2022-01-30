using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{
    public string scoreTextPath = "Assets/Rank/test.txt";
    public int Max;
    public int level;
    int highScore; //files read
    int score;
    bool end;
    bool levelUp;
    bool gameStart;
    GameObject inGameCanvas;
    GameObject endGameCanvas;
    GameObject tiles;
    Player player;
    AudioSource audio;
    Color[] dieColor = new Color[2]; // 0 : player 1:tile

    private void Start()
    {  
        //if(!File.Exists(scoreTextPath))
        //{
        //    FileStream test = new FileStream(scoreTextPath, FileMode.Create);
        //    test.Close();
        //}
        //SetMaxScore();
        audio = GetComponent<AudioSource>();
        audio.Stop();
        level = 1;
        end = false;
        levelUp = false;
        audio.Play();
        player = GameObject.Find("Player").GetComponent<Player>();
        inGameCanvas = GameObject.Find("InGameCanvas");
        tiles = GameObject.Find("Tiles");

        endGameCanvas = GameObject.Find("EndGameCanvas");
        endGameCanvas.SetActive(false);
    }

    private void Update()
    {
        if (end ) return;

        //playing
        int playScore = player.GetScore();
        if (playScore % 10 == 0 && playScore != 0 && !levelUp)
        {
            levelUp = true;
            level++;
        }

        if (playScore % 10 == 1 && levelUp)
        {
            levelUp = false;
            player.ReduceMaxJumpCount();
        }
            

        //end   
        if(player.GetDie() && !end)
        {
            end = true;
            SetiingEndInformation();
            audio.Stop();

            Invoke(nameof(StartEndScene), 1.5f);
        }
    }

    void SetiingEndInformation()
    {
        dieColor[0] = player.GetPlayerColor();
        dieColor[1] = player.GetTileColor();
        score = player.GetScore();
    }

    public void StartEndScene()
    {
        player.gameObject.SetActive(false);
        tiles.SetActive(false);
     //   WriteScore();
        
        inGameCanvas.SetActive(false);
        endGameCanvas.SetActive(true);
        
    }

    void SetMaxScore()
    {
        StreamReader sr = File.OpenText(scoreTextPath);
        string maxScore = sr.ReadLine();
        sr.Close();
        if (maxScore == null)
        {
            StreamWriter sw = File.CreateText(scoreTextPath);
            sw.WriteLine("0");
            maxScore = "0";
            sw.Close();
        }

        Max = Convert.ToInt32(maxScore);
    }

    void WriteScore()
    {
        if (Max < score)
        {
            Max = score;
            StreamWriter sw = File.CreateText(scoreTextPath);
            sw.WriteLine(score.ToString());
            sw.Close();
        }
        
    }

    public Color[] GetDieReason()
    {
        return dieColor;
    }

    public int GetPlayerScore()
    {
        return score;
    }
}
