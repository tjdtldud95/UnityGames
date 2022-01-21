using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EndCanavas : MonoBehaviour
{
    GameManager gm;
    Transform dieReason;
    TextMeshProUGUI scoreText;
    Color[] dieColor;
    int score;


    private void Start()
    {
        dieColor = new Color[2]; // 0 : player 1:tile

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        scoreText = transform.Find("Score_int").GetComponent<TextMeshProUGUI>();
        dieReason = transform.Find("PlayerDieReason");

        dieColor = gm.GetDieReason();
        score = gm.GetPlayerScore();

        int i = 0;
        foreach(Transform T in dieReason.transform)
        {
            T.GetComponent<Image>().color = dieColor[i];
            i++;
        }

        scoreText.text =score.ToString();

    }
    


}
