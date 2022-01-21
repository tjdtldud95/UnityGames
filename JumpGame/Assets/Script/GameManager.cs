using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    static GameManager instance = null;
    Player player;
    public int level;
    int highScore; //files read
    int score;
    bool end;
    public bool levelUp;
    Color[] dieColor = new Color[2]; // 0 : player 1:tile

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        level = 1;
        end = false;
        levelUp = false;
        string path = "Assets/Rank/test.txt";
        FileStream test = new FileStream(path, FileMode.Create);
        test.Close();
    }

    private void Update()
    {
        if (end) return;

        //playing
        int playScore = player.GetScore();
        if (playScore % 10 == 0 && playScore != 0 && !levelUp)
        {
            levelUp = true;
            level++;
        }

        if (playScore % 10 == 1 && levelUp)
            levelUp = false;


        //end
        if(player.GetDie() && !end)
        {
            end = true;
            SetiingEndInformation();
            SceneManager.LoadScene("EndScenes");
        }
    }

    void SetiingEndInformation()
    {
        dieColor[0] = player.GetPlayerColor();
        dieColor[1] = player.GetTileColor();
        score = player.GetScore();
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
