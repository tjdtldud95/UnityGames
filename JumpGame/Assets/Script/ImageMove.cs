using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageMove : MonoBehaviour
{
    public bool up;
    RectTransform movePos;
    RectTransform startPos;
    float angle = 0;
    float moveMax = 1;
    public Quaternion Qu;

    private void Start()
    {
        startPos = GetComponent<RectTransform>();
        movePos = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(up)
        {
            Vector3 pos = new Vector3(startPos.position.x, startPos.position.y + (moveMax * Mathf.Sin(angle)), startPos.position.z);
            movePos.SetPositionAndRotation(pos, Qu);
        }

        else
        {
            Vector3 pos = new Vector3(startPos.position.x + (moveMax * Mathf.Sin(angle)), startPos.position.y, startPos.position.z);
            movePos.SetPositionAndRotation(pos, Qu);
        }
        angle +=0.1f;
    }
}
