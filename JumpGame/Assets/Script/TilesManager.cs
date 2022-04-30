
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
    public Sprite[] tileSprite = new Sprite[3];
    Vector3 upPos = Vector3.up * 20f;
    bool firstRePos = false;
    bool isStar = false;
    List<Transform> tilePos = new List<Transform>();
    List<SpriteRenderer> tileColor = new List<SpriteRenderer>();
    List<Tile> tiles = new List<Tile>();

    // Start is called before the first frame update
    void Awake()
    {
        int len = transform.childCount;
        for(int i=0;i<len;i++)
        {
            tilePos.Add(transform.GetChild(i));
            tileColor.Add(transform.GetChild(i).GetComponent<SpriteRenderer>());
            tiles.Add(transform.GetChild(i).GetComponent<Tile>());
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

        if (index < 0) index += 8;

        ColoringTile(index);
      //  Vector3 move = tilePos[index].position + upPos;
        tilePos[index].position += upPos;
        SetTileSprite(index);
    }


    void ColoringTile()
    {
        int len = tileColor.Count;
        for(int i=0;i<len;i++)
        {
            UseColor tile = GetRandomlyColorByLevel();
            switch (tile)
            {
                case UseColor.red:
                    tileColor[i].color = MyColor.R;
                    break;
                case UseColor.blue:
                    tileColor[i].color = MyColor.B;
                    break;
                case UseColor.green:
                    tileColor[i].color = MyColor.G;
                    break;
                case UseColor.RB:
                    tileColor[i].color = MyColor.RB;
                    break;
                case UseColor.RG:
                    tileColor[i].color = MyColor.RG;
                    break;
                case UseColor.GB:
                    tileColor[i].color = MyColor.GB;
                    break;
                case UseColor.RGB:
                    tileColor[i].color = MyColor.RGB;
                    break;
                case UseColor.RRB:
                    tileColor[i].color = MyColor.RRB;
                    break;
                case UseColor.RRG:
                    tileColor[i].color = MyColor.RRG;
                    break;
                case UseColor.RGG:
                    tileColor[i].color = MyColor.RGG;
                    break;
                case UseColor.BBG:
                    tileColor[i].color = MyColor.BBG;
                    break;
                case UseColor.BGG:
                    tileColor[i].color = MyColor.BGG;
                    break;
                case UseColor.RBB:
                    tileColor[i].color = MyColor.RBB;
                    break;
            }
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

    void SetTileSprite(int index)
    {
        if(tilePos[index].position.y <96f)
        {
            tileColor[index].sprite = tileSprite[0];
        }    
        else if(tilePos[index].position.y >=96f && tilePos[index].position.y <196f)
        {
            tileColor[index].sprite = tileSprite[1];
        }

        else
        {
            tileColor[index].sprite = tileSprite[2];
        }
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
            num = 0;
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
        return tilePos[tileIndex].position;
    }



    public void SetTilePlayerBody(Player py)
    {
        for(int i=0;i<tiles.Count;i++)
        {
            tiles[i].player = py;
            tiles[i].playerBody = py.body;
        }
    }


    
}
