using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorGuide : MonoBehaviour
{
    public Transform imageColors;
    public RawImage resultColor;
    public TextMeshProUGUI clickObjectName;
    public RawImage[] choices = new RawImage[2];
    public RawImage choice;
    int[] clickCount = { 0, 0, 0 }; //R G B 
    int index = 0;
    bool isChoice = false;
    public void ClickColorButton(string buttonColor)
    {
        if (! isChoice) return;
       
        switch (buttonColor)
        {
            case "Red":
                clickCount[0]++;
                ChangeIamgeColors(MyColor.R);
                break;
            case "Green":
                clickCount[1]++;
                ChangeIamgeColors(MyColor.G);
                break;
            case "Blue":
                clickCount[2]++;
                ChangeIamgeColors(MyColor.B);
                break;

        }     
    }

    public void ClickChageObject(string name)
    {
        ClearClickCount();
        ClearImageColors();

        if (name.Equals("Dust"))
            choice = choices[0];

        else
            choice = choices[1];

        ChangeObjectNameText(name);
        isChoice = true;
    }


    void CanvasReset()
    {
        choice.color = Color.white;
        ClearClickCount();
        ClearImageColors();
        index = 0;
    }


    void ClearClickCount()
    {
        for (int i = 0; i < 3; i++)
        {
            clickCount[i] = 0;
        }
    }
    void ClearImageColors()
    {
        for(int i=0;i<3;i++)
        {
            var ob = imageColors.GetChild(i).GetComponent<RawImage>();
            ob.color = Color.white;
        }

        resultColor.color = Color.white;
    }

    void ChangeObjectNameText(string name)
    {
        clickObjectName.text = name;
    }

    void ChangeIamgeColors(Color color)
    {

        if(index >=3)
        {
            CanvasReset();
            return;
        }

        if(index >=1)
        {
            SetResultColor();
        }

        var ob =imageColors.GetChild(index).GetComponent<RawImage>();
        ob.color = color;
        index++;
    }


    void SetResultColor()
    {
        int sum = clickCount[0] + clickCount[1] + clickCount[2] + 1; //with White Count :1 
        for (int i = 0; i < 3; i++)
        {
            if (sum - 1 == clickCount[i])
            {
                switch (i)
                {
                    case 0:
                        resultColor.color = MyColor.R;
                        break;
                    case 1:
                        resultColor.color = MyColor.G;
                        break;
                    case 2:
                        resultColor.color = MyColor.B;
                        break;
                }
                return;
            }
        }

        Color color = (Color.red * clickCount[0] * ((float)1 / sum))
            + (MyColor.GreenNoWhite * clickCount[1] * ((float)1 / sum))
            + (MyColor.BlueNoWhite * clickCount[2] * ((float)1 / sum))
            + (Color.white * 1 * ((float)1 / sum));

        resultColor.color = color;
        choice.color = color;
    }



    private void FixedUpdate()
    {
        if (choice == null) return;
        choice.color = resultColor.color;
    }
}
