using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Player player;

    private void LateUpdate()
    {
        if (player.GetDie()) return;

        transform.position = new Vector3(0, player.transform.position.y+1.5f, -10f);   
    }

}
