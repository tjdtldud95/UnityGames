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
        green
    }

    List<Transform> tilePos;
    List<SpriteRenderer> tileColor;
    Vector3 upPos;
    int level;
    bool firstRePos;
    bool start;
    // Start is called before the first frame update
    void Awake()
    {
        tilePos = new List<Transform>();
        tileColor = new List<SpriteRenderer>();
        firstRePos = false;
        start = false;
        upPos = Vector3.up * 7.5f;
        foreach (Transform T in transform)
        {
            tilePos.Add(T);
            tileColor.Add(T.GetComponent<SpriteRenderer>());
        }
    }

    private void Start()
    {
        level = 0;
        ColoringTile();
    }

    public Vector2 GetTilesPos(int tileIndex)
    {
        if(!firstRePos)
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
        return tilePos[tileIndex].position;
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
            tile = (UseColor)Random.Range(1, 4);
            switch (tile)
            {
                case UseColor.red:
                    ob.color = Color.red;
                    break;
                case UseColor.blue:
                    ob.color = Color.blue;               
                    break;
                case UseColor.green:
                    ob.color = Color.green;
                    break;
            }
            i++;
        }

    }

    void ColoringTile(int index)
    {
        UseColor tile = (UseColor)Random.Range(1, 4);
        switch (tile)
        {
            case UseColor.red:
                tileColor[index].color = Color.red;
                break;
            case UseColor.blue:
                tileColor[index].color = Color.blue ;
                break;
            case UseColor.green:
                tileColor[index].color = Color.green;
                break;
        }
    }

}
