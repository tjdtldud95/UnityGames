using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Player player;
    public Transform tranFinalPos;
    public float moveSpeed;
    public float dis; 
    private void LateUpdate()
    {
        if (player.GetDie()) return;


        if(1000 <= player.GetScore())
        {
            Vector3 Vec3Pos = transform.position;
            if(dis < Vector3.Distance(Vec3Pos, tranFinalPos.position))
            {
                Vec3Pos.y += moveSpeed * Time.deltaTime;
                transform.position = Vec3Pos;
            }

         
            return;
        }
        transform.position = new Vector3(0, player.transform.position.y+1.5f, -10f);   
    }

}
