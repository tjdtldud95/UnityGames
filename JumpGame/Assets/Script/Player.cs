using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CameraMove cameraMove;
    TilesManager tiles;
    Rigidbody2D rb;
    Vector2 nextPos;
    Vector2 curuntPos;
    int jumpCount;
    float jumpPower;
    bool isMove;
    int tilesIndex;

    private void Awake()
    {
        tilesIndex = 0;
        jumpCount = 0;
        jumpPower = 120f;
        isMove = false;
        rb = GetComponent<Rigidbody2D>();
        nextPos = Vector2.zero;
        curuntPos = Vector2.zero;
        tiles = GameObject.Find("Tiles").GetComponent<TilesManager>();
        cameraMove = Camera.main.GetComponent<CameraMove>();
        
    }
    private void Start()
    {
        
        SetNextPos();
    }

    private void FixedUpdate()
    {
        if (isMove)
        {
            Vector2 velo = Vector2.up*8f;
            transform.position = Vector2.SmoothDamp(transform.position,nextPos, ref velo , 0.1f);
        }
    }

    void SetNextPos()
    {
        curuntPos = nextPos;
        cameraMove.SetCameraPosY(curuntPos.y);
        nextPos =tiles.GetTilesPos(tilesIndex++)+ Vector2.up;
        tilesIndex %= 6;
    }

    Vector2 GetCurrntPos()
    {
        return curuntPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isMove)
        {
            isMove = false;
            SetNextPos();
        }

        if(jumpCount==10)
        {
            jumpCount = 0;
            isMove = true;
            return;
        }

        rb.AddForce(Vector2.up * jumpPower);

        jumpCount++;
    }
 
}
