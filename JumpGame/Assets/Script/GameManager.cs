using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{
    public INGameCavas inGameCanvas;
    public GameObject endGameCanvas;
    public TilesManager tiles;
    public Player player;
    public int[] levelScore = { 10, 20, 50, 70 };
    public int level = 0;
    int Max;
    int score;
    bool end = false;
    bool levelUp = false;
    AudioSource audio;
    Color[] dieColor = new Color[2]; // 0 : player 1:tile

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.Stop();
        audio.Play();
        endGameCanvas.SetActive(false);
    }

    private void Update()
    {
        if (end ) return;

        if (level > 3)
            goto CheckEnd;
        
        //playing and LevelUP
        int playScore = player.GetScore();
        if (playScore == levelScore[level] && playScore != 0 && !levelUp)
        {
            levelUp = true;
            level++;
        }

        if (levelUp)
        {
            if(level == 3)
            {
                inGameCanvas.InGameCavesLevelUP();
            }

            levelUp = false;
            player.ReduceMaxJumpCount();
        }
      
CheckEnd:

        //end   
        if(player.GetDie() && !end)
        {
            end = true;
            SetiingEndInformation();
            audio.Stop();
            Invoke(nameof(StartEndScene), 2f);
        }
    }



    public void GameManagerReset()
    {
        player.PlayerReset();
        tiles.TilesManagerReset();
        end = false;
        audio.Play();
        tiles.gameObject.SetActive(true);
        inGameCanvas.InGameCanvasReset();
        endGameCanvas.SetActive(false);
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
        tiles.gameObject.SetActive(false);

        inGameCanvas.gameObject.SetActive(false);
        endGameCanvas.SetActive(true);
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
