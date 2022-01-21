using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    CameraMove cameraMove;
    TilesManager tiles;
    Rigidbody2D rb;
    SpriteRenderer playerRenderer;
    Vector2 nextPos;
    Vector2 curuntPos;
    Color tileColor;
    int jumpCount;
    int maxJumpCount;
    int score;
    int tilesIndex;
    float jumpPower;
    bool isMove;
    bool die;
    bool start;
    
    private void Awake()
    {
        tilesIndex = 0;
        jumpCount = 0;
        jumpPower = 120f;
        score = 0;
        isMove = false;
        die = false;
        start = false;
        nextPos = Vector2.zero;
        curuntPos = Vector2.zero;
        tileColor = Color.clear;
        rb = GetComponent<Rigidbody2D>();
        tiles = GameObject.Find("Tiles").GetComponent<TilesManager>();
        cameraMove = Camera.main.GetComponent<CameraMove>();
        playerRenderer = transform.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SetNextPos();
    }

    private void FixedUpdate()
    {
        if (die) return;

        if (isMove)
        {
            Vector2 velo = Vector2.up*4f;
            transform.position = Vector2.SmoothDamp(transform.position,nextPos, ref velo , 0.1f);
        }

        //Debug.Log("Player Score : " + score);
    }

    void SetNextPos()
    {
        score++;
        curuntPos = nextPos;
        cameraMove.SetCameraPosY(curuntPos.y);
        nextPos =tiles.GetTilesPos(tilesIndex++)+ Vector2.up;
        tilesIndex %= 6;
    }
    
    public int GetScore()
    {
        return score-1;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isMove)
        {
            isMove = false;
            SetNextPos();
        }

        if(jumpCount==2)
        {
            jumpCount = 0;
            isMove = true;
            if(collision.transform.CompareTag("Respawn"))
            {
                collision.gameObject.SetActive(false);
            }
            return;
        }

        jumpCount++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (die ) return;


        if (collision.transform.CompareTag("Respawn"))
        {
            goto Jump;
        }

        var ob = collision.transform.GetComponent<SpriteRenderer>();
        if(!playerRenderer.color.Equals(ob.color))
        {
            tileColor = ob.color;
            die = true;
            return;
        }

Jump:
        rb.AddForce(Vector2.up * jumpPower);
    }


    public bool GetDie()
    {
        return die;
    }

    public void setStart()
    {
        start = true;
    }
    public Color GetTileColor()
    {
        return tileColor;
    }

    public Color GetPlayerColor()
    {
        return playerRenderer.color;
    }
}
