using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour
{
    List<Transform> tilePos;
    Vector3 upPos;
    // Start is called before the first frame update
    void Awake()
    {
        upPos = Vector3.up * 7.5f;
        tilePos = new List<Transform>();
        foreach(Transform T in transform)
        {
            tilePos.Add(T);
        }
    }

    public Vector2 GetTilesPos(int tileIndex)
    {
        if (tileIndex % 3 == 0 && tileIndex != 0 )
        {
            RePositionTile(tileIndex - 3);
            RePositionTile(tileIndex - 2);
        }
        return tilePos[tileIndex].position;
    }

    void RePositionTile(int index)
    {
        Vector3 move = tilePos[index].position + upPos;
        tilePos[index].position = move;
    }

}
