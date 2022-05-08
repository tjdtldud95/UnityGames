using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaterChoose : MonoBehaviour
{
    RectTransform pos;
    bool isLeft;
    bool isRight;
    int distance = 0;
    float moveSpeed = 25f;

    public string[] charactornames;
    public Button select;
    public StartCanvas stc;
    
    int index = 0;
    private void Start()
    {
        pos = GetComponent<RectTransform>();
        
    }

    private void FixedUpdate()
    {
        if(isLeft)
        {
            if (pos.anchoredPosition.x == distance)
            {
                isLeft = false;
                return;
            }

            pos.localPosition += Vector3.right * moveSpeed;
        }

        if(isRight)
        {
            if (pos.anchoredPosition.x == distance)
            {
                isRight = false;
                return;
            }

            pos.localPosition += Vector3.left * moveSpeed;
          
        }
    }

    public void ClickLeftBotton()
    {
        if(distance + 900 > 0)
        {
            return;
        }

        isLeft = true;
        distance += 900;
        index--;
        select.interactable = stc.playerImage[index].isOpen;
    }

    public void ClickRightBotton()
    {
        if (distance - 900 <= -900 * transform.childCount)
        {
            return;
        }
        isRight = true;
        distance -= 900;
        index++;
        select.interactable  = stc.playerImage[index].isOpen;
    }

    public void ClickSelect()
    {
        PlayerData.instance.name = charactornames[index];
    }


}
