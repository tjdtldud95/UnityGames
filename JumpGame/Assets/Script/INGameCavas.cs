using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class INGameCavas : MonoBehaviour 
{
    public Player player;
    public Transform buttonImage;
    public GameObject activeButtonImage;
    public SpriteRenderer playerRenderer;
    public TextMeshProUGUI scoreInt;
    public TextMeshProUGUI scoreText;
    public Image enter;
    public List<Image> clickImages;
    Color enterColor;
    bool end;
    int[] clickCount = { 0, 0, 0 }; //R G B 

    private void Start()
    {
        end = false;
        enterColor = Color.clear;

        int len = buttonImage.childCount;
        for(int i=4;i<len-1;i++)
        {
            clickImages.Add(buttonImage.GetChild(i).GetComponent<Image>());
        }

        if (PlayerData.instance.goCheckPoint)
        {
            SortClickImage();
        }
    }
    private void FixedUpdate()
    {
        if (end) return;

        if(player.GetDie())
        {
            end = true;
        }
        if(player.GetScore()>=76 && scoreText.color != Color.white)
        {
            scoreText.color = Color.white;
            scoreInt.color = Color.white;
        }
        scoreInt.text = player.GetScore().ToString();
    }

    public void SortClickImage()
    {
        clickImages.Add(clickImages[0]);
        clickImages.RemoveAt(0);
    }

    public void InGameCanvasReset()
    {
        end = false;
        ClearClickImage();
        gameObject.SetActive(true);
    }

    public void InGameCavesLevelUP()
    {
        OpenLastClickImage();
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
    void OpenLastClickImage()
    {
        clickImages.Add(buttonImage.GetChild(6).GetComponent<Image>());
        activeButtonImage.SetActive(true);
    }
    void ClearClickImage()
    {
        int len = clickImages.Count;
        for(int i=0;i<len;i++)
        {
            clickImages[i].color = Color.clear;
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
        int len = clickImages.Count;
        if (clickImages[len-1].color.Equals(Color.clear) == false)
        {
            ClearClickImage();
            return;
        }

        for(int i=0;i<len;i++)
        {
            if(clickImages[i].color.Equals(Color.clear))
            {
                clickImages[i].color = color;
                break;
            }
        }
        ChangeEnterColor();
    }
    void ClickEnter()
    {
        if (clickImages[0].color.Equals(Color.clear) || clickImages[1].color.Equals(Color.clear))
            return;


        int len = clickImages.Count;
        for (int i = 0; i < len; i++)
        {
            clickImages[i].color = Color.clear;
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
        
        len = clickCount.Length;
        for(int i=0;i<len;i++)
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
