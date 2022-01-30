using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public AudioClip audioJump;
    public AudioClip audioFail;
    AudioSource audioSource;
    CameraMove cameraMove;
    TilesManager tiles;
    Rigidbody2D rb;
    SpriteRenderer playerRenderer;
    public Vector2 nextPos;
    Vector2 curuntPos;
    Color tileColor;
    int jumpCount;
    int maxJumpCount;
    int score;
    public int tilesIndex;
    float jumpPower;
    bool isMove;
    bool die;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        tiles = GameObject.Find("Tiles").GetComponent<TilesManager>();
        cameraMove = Camera.main.GetComponent<CameraMove>();
        playerRenderer = transform.GetComponent<SpriteRenderer>();
        rb.bodyType = RigidbodyType2D.Dynamic;
    }


    private void Start()
    {
        tilesIndex = 0;
        jumpCount = 0;
        jumpPower = 200f;
        score = 0;
        isMove = false;
        die = false;
        nextPos = Vector2.zero;
        curuntPos = Vector2.zero;
        tileColor = Color.clear;
        maxJumpCount = 4;
        SetNextPos();
    }

    private void FixedUpdate()
    {
        if (die) return;

        if (isMove)
        {
            Vector2 velo = Vector2.up*5f;
            if (nextPos.x < 0) velo += Vector2.left*0.7f;
            else velo += Vector2.right * 0.7f;

            transform.position = Vector2.SmoothDamp(transform.position,nextPos, ref velo , 0.1f);
     
        }
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

        if(jumpCount== maxJumpCount)
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
        if(playerRenderer.color != (ob.color))
        {  
            PlaySound("Fail");
            ob.transform.GetComponent<Tile>().StartDieAnimation();
            rb.bodyType = RigidbodyType2D.Kinematic;
            tileColor = ob.color;
            die = true;
            return;
        }
        

    Jump:
        PlaySound("Jump");
        rb.AddForce(Vector2.up * jumpPower);
    }


    void PlaySound(string action)
    {
        switch(action)
        {
            case "Jump":
                audioSource.clip = audioJump;
                break;
            case "Fail":
                audioSource.clip = audioFail;
                break;
        }
        audioSource.Play();
    }

    public void ReduceMaxJumpCount()
    {
        if (maxJumpCount <= 0)
        {
            maxJumpCount = 1;
            return;
        }

        maxJumpCount--;
    }

    public bool GetDie()
    {
        return die;
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
