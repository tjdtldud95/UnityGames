
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour
{
    enum UseColor
    {
        clear = 0,
        red,
        blue,
        green,
        RB,
        RG,
        GB,
        RGB,
        RRB,
        RRG,
        RGG,
        BBG,
        BGG,
        RBB
    }
    public GameManager gm;
    public Transform stars;
    public int num = 0;
    public int starCreateScore;
    Vector3 upPos = Vector3.up * 15f;
    bool firstRePos = false;
    bool isStar = false;
    List<Transform> tilePos = new List<Transform>();
    List<SpriteRenderer> tileColor = new List<SpriteRenderer>();

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Transform T in transform)
        {
            tilePos.Add(T);
            tileColor.Add(T.GetComponent<SpriteRenderer>());
        }
    }

    private void Start()
    {
        ColoringTile();
        SetStarCreateScore();
    }
    void RePositionandColoringTile(int index)
    {
        if (!firstRePos) firstRePos = true;

        if (index < 0) index += 6;

        ColoringTile(index);
        Vector3 move = tilePos[index].position + upPos;
        tilePos[index].position = move;
    }


    void ColoringTile()
    {
        UseColor tile;
        int i = 0;      
        foreach(var ob in tileColor)
        {
            tile = GetRandomlyColorByLevel();
            switch (tile)
            {
                case UseColor.red:
                    ob.color = MyColor.R;
                    break;
                case UseColor.blue:
                    ob.color = MyColor.B;               
                    break;
                case UseColor.green:
                    ob.color = MyColor.G;
                    break;
                case UseColor.RB:
                    ob.color = MyColor.RB;
                    break;
                case UseColor.RG:
                    ob.color = MyColor.RG;
                    break;
                case UseColor.GB:
                    ob.color = MyColor.GB;
                    break;
                case UseColor.RGB:
                    ob.color = MyColor.RGB;
                    break;
                case UseColor.RRB:
                    ob.color = MyColor.RRB;
                    break;
                case UseColor.RRG:
                    ob.color = MyColor.RRG;
                    break;
                case UseColor.RGG:
                    ob.color = MyColor.RGG;
                    break;
                case UseColor.BBG:
                    ob.color = MyColor.BBG;
                    break;
                case UseColor.BGG:
                    ob.color = MyColor.BGG;
                    break;
                case UseColor.RBB:
                    ob.color = MyColor.RBB;
                    break;
            }
            i++;
        }

    }

    void SetStarCreateScore()
    {
        starCreateScore = (int)Random.Range(17, 25);
    }
   
    void CreateStar(int index = -1)
    {
        Transform ob;
        try
        {
            ob = stars.transform.GetChild(0);
        }
        catch (UnityException)
        {
            return;
        }
        Vector3 move = tilePos[index].position + upPos + Vector3.up;
        ob.SetParent(null);
        ob.position = move;
        ob.gameObject.SetActive(true);
        SetStarCreateScore();
    }

    void ColoringTile(int index)
    {
        bool isStar = false;
        if (num-1 >= starCreateScore)
        {
            isStar = true;
        }

        UseColor tile = GetRandomlyColorByLevel();
        switch (tile)
        {
            case UseColor.red:
                tileColor[index].color = MyColor.R;
                break;
            case UseColor.blue:
                tileColor[index].color = MyColor.B;
                break;
            case UseColor.green:
                tileColor[index].color = MyColor.G;
                break;
            case UseColor.RB:
                tileColor[index].color = MyColor.RB;
                break;
            case UseColor.RG:
                tileColor[index].color = MyColor.RG;
                break;
            case UseColor.GB:
                tileColor[index].color = MyColor.GB;
                break;
            case UseColor.RGB:
                tileColor[index].color = MyColor.RGB;
                break;
            case UseColor.RRB:
                tileColor[index].color = MyColor.RRB;
                break;
            case UseColor.RRG:
                tileColor[index].color = MyColor.RRG;
                break;
            case UseColor.RGG:
                tileColor[index].color = MyColor.RGG;
                break;
            case UseColor.BBG:
                tileColor[index].color = MyColor.BBG;
                break;
            case UseColor.BGG:
                tileColor[index].color = MyColor.BGG;
                break;
            case UseColor.RBB:
                tileColor[index].color = MyColor.RBB;
                break;
        }


        if(isStar)
        {
            CreateStar(index);
        }
    }


    UseColor GetRandomlyColorByLevel()
    {
        int level =gm.level+1;
        UseColor value = 0;
        switch(level)
        {
            case 1:
                value = (UseColor)Random.Range(1, 4);
                break;
            case 2:
                value = (UseColor)Random.Range(4, 7);
                break;
            case 3:
                value = (UseColor)Random.Range(1, 7);
                break;
            case 4:
                value = (UseColor)Random.Range(7, 14);
                break;
            case 5:
                value = (UseColor)Random.Range(1, 14);
                break;

        }

        return value;
    }
    public Vector2 GetTilesPos(int tileIndex)
    {
        if (!firstRePos)
        {
            if (tileIndex % 2 != 0 && tileIndex != 1)
            {
                RePositionandColoringTile(tileIndex - 3);
                RePositionandColoringTile(tileIndex - 2);
            }
        }

        else
        {
            if (tileIndex % 2 != 0)
            {
                RePositionandColoringTile(tileIndex - 3);
                RePositionandColoringTile(tileIndex - 2);
            }

        }

        num++;
        num %= 30;
        return tilePos[tileIndex].position;
    }

}
