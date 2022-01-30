using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class INGameCavas : MonoBehaviour 
{
    List<Image> clickImages;
    Player player;
    Transform buttonImage;
    SpriteRenderer playerRenderer;
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
        playerRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
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
        if (end) return;
        
        switch (buttonColor)
        {
            case "Red" :
                clickCount[0]++;
                ChangeOrderButtonColor(MyColor.R);
                break;
            case "Green":
                clickCount[1]++;
                ChangeOrderButtonColor(MyColor.G);
                break;
            case "Blue":
                clickCount[2]++;
                ChangeOrderButtonColor(MyColor.B);
                break;

            case "Enter":
                ClickEnter();
                break;
        }
    }

    void ClearClickImage()
    {
        foreach (var ob in clickImages)
        {
            ob.color = Color.clear;
        }
        for (int i = 0; i < 3; i++)
        {
            clickCount[i] = 0;
        }
        enter.color = Color.clear;
        enterColor = Color.clear;
    }
    void ChangeOrderButtonColor(Color color)
    {
        if (clickImages[2].color.Equals(Color.clear) == false)
        {
            ClearClickImage();
            return;
        }

        foreach (var ob in clickImages)
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
                        enter.color = MyColor.R;
                        break;
                    case 1:
                        enter.color = MyColor.G;
                        break;
                    case 2:
                        enter.color = MyColor.B;
                        break;
                }
            }
        }

        playerRenderer.color = enter.color;
        enter.color = Color.clear;

        for(int i=0;i<clickCount.Length;i++)
        {
            clickCount[i] = 0;
        }
    }
    void ChangeEnterColor()
    {
        if (clickImages[0].color.Equals(Color.clear) || clickImages[1].color.Equals(Color.clear))
            return;

        int sum = clickCount[0] + clickCount[1] + clickCount[2] +1; //with White Count :1 


        for (int i = 0; i < 3; i++)
        {
            if (sum-1 == clickCount[i])
            {
                switch (i)
                {
                    case 0:
                        enter.color = MyColor.R;
                        break;
                    case 1:
                        enter.color = MyColor.G;
                        break;
                    case 2:
                        enter.color = MyColor.B;
                        break;
                }
                return;
            }
        }

        enterColor = (Color.red * clickCount[0] * ((float)1 / sum)) 
            + (MyColor.GreenNoWhite * clickCount[1] *((float)1 / sum)) 
            + (MyColor.BlueNoWhite * clickCount[2] * ((float)1 / sum))
            + (Color.white  *1* ((float)1 / sum));

        enter.color = enterColor;
        
    }


}
