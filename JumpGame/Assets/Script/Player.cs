using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public AudioClip audioJump;
    public AudioClip audioFail;
    public PlayerData data;
    public TilesManager tiles;
    public CameraMove cameraMove;
    AudioSource audioSource;
    Rigidbody2D rb;
    SpriteRenderer playerRenderer;
    Vector2 nextPos;
    Vector2 curuntPos;
    Vector2 velo = Vector2.up * 5f;
    Color tileColor;
    int jumpCount = 0;
    int maxJumpCount = 4;
    int score = 0;
    int tilesIndex = 0;
    float jumpPower = 200f;
    bool isMove;
    bool die;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        playerRenderer = transform.GetComponent<SpriteRenderer>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        SetNextPos();
    }

    private void FixedUpdate()
    {
        if (die) return;

        if (isMove)
        {
            transform.position = Vector2.Lerp(transform.position, nextPos + Vector2.up*0.625f, 0.1f);
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

        if(jumpCount >= maxJumpCount)
        {
            jumpCount = 0;
            isMove = true;
            if (collision.gameObject.layer.Equals(3))
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

            SaveScore();
            die = true;
            return;
        }
        
        
    Jump:
        PlaySound("Jump");
        rb.AddForce(Vector2.up * jumpPower);
    }


    void SaveScore()
    {
        PlayerData.instance.SetScore(score-1);
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
        maxJumpCount--;

        if (maxJumpCount < 0)
        {
            maxJumpCount = 0;
            return;
        }
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
