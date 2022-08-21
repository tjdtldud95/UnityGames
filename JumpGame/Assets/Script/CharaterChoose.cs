using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaterChoose : MonoBehaviour
{
    enum CharaterName
    {
        TravelDust,
        PainDust,
        HeartDust,
        BabyDust,
        ChickDust
    }

    RectTransform pos;
    bool isLeft;
    bool isRight;
    int distance = 0;
    float moveSpeed = 30f;

    public string[] charactornames;
    public Button select;
    public Button leftButton;
    public Button rightButton;
    public StartCanvas stc;
    public Transform illustration_Panel;
    public GameObject illustration_Open;
    public GameObject illustration_Close;

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
                leftButton.interactable = true;
                rightButton.interactable = true;
                return;
            }
            rightButton.interactable = false;
            leftButton.interactable = false;
            pos.localPosition += Vector3.right * moveSpeed;
        }

        if(isRight)
        {
            if (pos.anchoredPosition.x == distance)
            {
                isRight = false;
                leftButton.interactable = true;
                rightButton.interactable = true;
                return;
            }
            rightButton.interactable = false;
            leftButton.interactable = false;

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
        if (stc.playerImage[index].isOpen)
        {
            illustration_Open.SetActive(true);
            illustration_Close.SetActive(false);
        }

        else
        {
            illustration_Open.SetActive(false);
            illustration_Close.SetActive(true);
        }
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

        if (stc.playerImage[index].isOpen)
        {
            illustration_Open.SetActive(true);
            illustration_Close.SetActive(false);
        }

        else
        {
            illustration_Open.SetActive(false);
            illustration_Close.SetActive(true);
        }
    }

    public void ClickSelect()
    {
        PlayerData.instance.name = charactornames[index];
    }

    public void Show_illustration()
    {
        Clear_illustration();

        illustration_Panel.gameObject.SetActive(true);
        illustration_Panel.GetChild(index).gameObject.SetActive(true);
    }

    public void Close_illustration()
    {
        Clear_illustration();
        illustration_Panel.gameObject.SetActive(false);
    }


    void Clear_illustration()
    {
        int len = illustration_Panel.childCount - 1;
        for (int i = 0; i < len; ++i)
        {
            illustration_Panel.GetChild(i).gameObject.SetActive(false);
        }

    }
}
