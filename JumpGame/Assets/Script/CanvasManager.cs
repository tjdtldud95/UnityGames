using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class CanvasManager : MonoBehaviour 
{
    List<Image> clickImages;
    Canvas canvas;
    GameObject player;
    Transform buttonImage;

    private void Start()
    {
        player = GameObject.Find("Player");
        clickImages = new List<Image>();
        canvas = GetComponent<Canvas>();
        buttonImage = transform.Find("Click_Button_Color");
        foreach(Transform T in buttonImage.transform)
        {
            clickImages.Add(T.GetComponent<Image>());
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
            if(ob.color.Equals(Color.clear))
            {
                ob.color = color;
                return;
            }
        }
    }

    void ClickEnter()
    {
        //clear ClickImages
        foreach (var ob in clickImages)
        {
            if(ob.color.Equals(Color.clear))
                continue;

            player.GetComponent<SpriteRenderer>().color = ob.color;
            ob.color = Color.clear;
            
        }
    }
}
