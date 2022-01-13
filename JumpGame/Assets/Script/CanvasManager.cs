using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
[RequireComponent(typeof(Canvas))]
public class CanvasManager : MonoBehaviour 
{
    List<Image> clickImages;
    Canvas canvas;

    private void Start()
    {
        clickImages = new List<Image>();
        canvas = GetComponent<Canvas>();

        var ob = transform.Find("Click_Button_Color");
        for(var i=0;i<ob.childCount;i++)
        {
            clickImages.Add(ob.GetChild(i).GetComponent<Image>());
        }
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
            case "Yellow":
                ChangeOrderButtonColor(Color.yellow);
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
            if(ob.color == Color.clear)
            {
                ob.color = color;
                return;
            }
        }
    }

    void ClickEnter()
    {
        foreach (var ob in clickImages)
        {
            ob.color = Color.clear;
        }
    }
}
