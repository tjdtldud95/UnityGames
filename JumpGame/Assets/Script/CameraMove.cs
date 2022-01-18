using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    float CameraPosY;
    float moveSpeed;
    bool isMove;
    // Start is called before the first frame update
    void Start()
    {
        CameraPosY = 0f;
        moveSpeed = 10f;
        isMove = false;
    }

    private void LateUpdate()
    {
        if(isMove)
        {
            transform.SetPositionAndRotation(new Vector3(0, CameraPosY, -10), Quaternion.identity);
            isMove = false;
        }
    }
    public void SetCameraPosY(float PosY)
    {
        CameraPosY = PosY;
        isMove = true;
    }


}
