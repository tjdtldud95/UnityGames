using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class EndCanavas : MonoBehaviour
{
    Transform playerColor;
    TextMeshProUGUI scoreText;
    Color[] dieColor;
    int score;
    List<Image> answers;
    Image reason;
    private void Awake()
    {
        answers = new List<Image>();        
    }
    private void Start()
    {
        dieColor = new Color[2]; // 0 : player 1:tile

        scoreText = transform.Find("Score_int").GetComponent<TextMeshProUGUI>();
        playerColor = transform.Find("PlayerDieReason");
        reason = transform.Find("Reason").GetComponent<Image>();

        dieColor = GameObject.Find("GameManager").GetComponent<GameManager>().GetDieReason();
        score = GameObject.Find("GameManager").GetComponent<GameManager>().GetPlayerScore();
         
        int i = 0;
        foreach(Transform T in playerColor.transform)
        {
            T.GetComponent<Image>().color = dieColor[i];
            i++;
        }

        var obs = transform.Find("Answer");
        foreach(Transform T in obs.transform)
        {
            answers.Add(T.GetComponent<Image>());
        }

        reason.color = dieColor[1];
        scoreText.text =score.ToString();
        Answer();
    }


    void Answer()
    {
        if(reason.color == MyColor.R)
        {
            answers[0].color = MyColor.R;
            answers[1].color = MyColor.R;
            answers[2].gameObject.SetActive(false);
        }
        else if(reason.color ==MyColor.G)
        {
            answers[0].color = MyColor.G;
            answers[1].color = MyColor.G;
            answers[2].gameObject.SetActive(false);
        }
        else if (reason.color == MyColor.B)
        {
            answers[0].color = MyColor.B;
            answers[1].color = MyColor.B;
            answers[2].gameObject.SetActive(false);
        }
        else if (reason.color == MyColor.RB)
        {
            answers[0].color = MyColor.R;
            answers[1].color = MyColor.B;
            answers[2].gameObject.SetActive(false);
        }
        else if (reason.color == MyColor.RG)
        {
            answers[0].color = MyColor.R;
            answers[1].color = MyColor.G;
            answers[2].gameObject.SetActive(false);
        }
        else if (reason.color == MyColor.GB)
        {
            answers[0].color = MyColor.G;
            answers[1].color = MyColor.B;
            answers[2].gameObject.SetActive(false);
        }
        else if (reason.color == MyColor.RGB)
        {
            answers[0].color = MyColor.R;
            answers[1].color = MyColor.G;
            answers[2].color = MyColor.B;
        }
        else if (reason.color == MyColor.RRB)
        {
            answers[0].color = MyColor.R;
            answers[1].color = MyColor.R;
            answers[2].color = MyColor.B;
        }
        else if (reason.color == MyColor.RRG)
        {
            answers[0].color = MyColor.R;
            answers[1].color = MyColor.R;
            answers[2].color = MyColor.G;
        }
        else if (reason.color == MyColor.RGG)
        {
            answers[0].color = MyColor.R;
            answers[1].color = MyColor.G;
            answers[2].color = MyColor.G;
        }
        else if (reason.color == MyColor.BBG)
        {
            answers[0].color = MyColor.B;
            answers[1].color = MyColor.B;
            answers[2].color = MyColor.G;
        }
        else if (reason.color == MyColor.BGG)
        {
            answers[0].color = MyColor.B;
            answers[1].color = MyColor.G;
            answers[2].color = MyColor.G;
        }
        else if (reason.color == MyColor.RBB)
        {
            answers[0].color = MyColor.R;
            answers[1].color = MyColor.B;
            answers[2].color = MyColor.B;
        }
    }
    

    public void ReStartGame()
    {
        SceneManager.LoadScene("OutGame");
    }
}
