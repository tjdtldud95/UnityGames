using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CanvasManager : MonoBehaviour 
{
    List<Image> clickImages;
    Player player;
    Transform buttonImage;
    SpriteRenderer playerSR;
    TextMeshProUGUI scoreText;
    Image enter;
    Color enterColor;
    bool end;
    int[] clickCount = { 0, 0, 0 }; //R G B 

    private void Start()
    {
        end = false;
        clickImages = new List<Image>();
        enterColor = Color.clear;
        playerSR = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<Player>();
        buttonImage = transform.Find("Click_Button_Color");
        scoreText = transform.Find("Score_int").GetComponent<TextMeshProUGUI>();
        enter = transform.Find("Enter").GetComponent<Image>();

        foreach (Transform T in buttonImage.transform)
        {
            clickImages.Add(T.GetComponent<Image>());
        }
    }
    private void Update()
    {
        if (end) return;
        if(player.GetDie())
        {
            end = true;
        }
        scoreText.text = player.GetScore().ToString();
        ChangeEnterColor();
    }

    public void ClickButton(string buttonColor)
    {
        switch(buttonColor)
        {
            case "Red" :
                ChangeOrderButtonColor(Color.red);
                break;
            case "Blue":
                ChangeOrderButtonColor(Color.blue);
                break;
            case "Green":
                ChangeOrderButtonColor(Color.green);
                break;
            case "Enter":
                ClickEnter();
                break;
        }
    }
    void ChangeOrderButtonColor(Color color)
    {
        foreach(var ob in clickImages)
        {
            if(ob.color.Equals(Color.clear))
            {
                ob.color = color;
                break;
            }
        }
    }
    void ClickEnter()
    {
        if (clickImages[0].color.Equals(Color.clear) || clickImages[1].color.Equals(Color.clear))
            return;


        foreach (var ob in clickImages)
        {
            ob.color = Color.clear;
        }
        
        int sum = clickCount[0] + clickCount[1]+ clickCount[2];

        for (int i = 0; i < 3; i++)
        {
            if (sum == clickCount[i])
            {
                switch (i)
                {
                    case 0:
                        enter.color = Color.red;
                        break;
                    case 1:
                        enter.color = Color.green;
                        break;
                    case 2:
                        enter.color = Color.blue;
                        break;
                }
            }
        }

        playerSR.color = enter.color;
        enterColor = Color.clear;

        for(int i=0;i<clickCount.Length;i++)
        {
            clickCount[i] = 0;
        }
    }
    void ChangeEnterColor()
    {
        if (clickImages[0].color.Equals(Color.clear) || clickImages[1].color.Equals(Color.clear))
            return;

        int sum = 0;
        //clear ClickImages
        foreach (var ob in clickImages)
        {
            if (ob.color.Equals(Color.clear))
                break;

            else if (ob.color.Equals(Color.red))
            {
                clickCount[0]++;
                sum++;
            }

            else if (ob.color.Equals(Color.green))
            {
                clickCount[1]++;
                sum++;
            }

            else if (ob.color.Equals(Color.blue))
            {
                clickCount[2]++;
                sum++;
            }
        }

        enterColor = (Color.red * clickCount[0] * 1 / sum) + (Color.green * clickCount[1] * 1 / sum) + (Color.blue * clickCount[2] * 1 / sum);
        enter.color = enterColor;
    }


}
