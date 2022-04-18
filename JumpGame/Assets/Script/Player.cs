using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public AudioClip audioJump;
    public AudioClip audioFail;
    public TilesManager tiles;
    public SpriteRenderer body;
    AudioSource audioSource;
    Rigidbody2D rb;
    PlayerAnimation playerAni;
    Vector2 nextPos;
    Vector2 curuntPos;
    Vector2 velo = Vector2.up * 5f;
    Color tileColor;
    int jumpCount = 0;
    int maxJumpCount = 4;
    int score = 0;
    public int tilesIndex = 0;
    int AnimationPlayCount = 0;
    bool isMove;
    bool die;
    bool shiledTime;
    bool randing;
    bool ishit;
    bool[] shiled = new bool[3];

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        playerAni = GetComponent<PlayerAnimation>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        SetNextPos();
    }

    private void FixedUpdate()
    {
        if (die) return;

        if (isMove)
        {
            transform.position = Vector2.Lerp(transform.position, nextPos + Vector2.up, 0.1f);
        }

        if(ishit && !die)
        {
            for (int i = 0; i < 3; i++)
            {
                if (shiled[i] == true)
                {
                    shiledTime = true;
                    shiled[i] = false;
                    break;
                }
            }

            if (shiledTime)
            {
                InvokeRepeating(nameof(Damage), 0.3f, 0.2f);
                ishit = false;
                return;
            }

            playerAni.PlayHitAnimation();
            PlaySound("Fail");
            rb.bodyType = RigidbodyType2D.Kinematic;
            Invoke(nameof(DropPlayer), 1f);

            SaveScore();
            die = true;
        }
    }

    void SetNextPos()
    { 
        score++;
        curuntPos = nextPos;
        nextPos =tiles.GetTilesPos(tilesIndex++)+ Vector2.up;
        tilesIndex %= 8;
    }
    
    public int GetScore()
    {
        return score-1;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (die) return;

        if (jumpCount >= maxJumpCount)
        {
            jumpCount = -1;
            isMove = true;

            if (shiledTime) shiledTime = false;

            if (collision.gameObject.layer.Equals(3))
            {
                collision.gameObject.SetActive(false);
            }
        }

        playerAni.PlayJumpAnimaition();
        
        PlaySound("Jump");
        jumpCount++;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            for (int i = 0; i < 3; i++)
            {
                if (shiled[i] == false)
                {
                    shiled[i] = true;
                    break;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (die ) return;

        if (isMove)
        {
            isMove = false;
            SetNextPos();
        }

        if (jumpCount == maxJumpCount) randing = true;

        if (collision.transform.CompareTag("Respawn") || shiledTime)
        {
            goto Jump;
        }
        

    Jump:
        if (randing)
        {
            playerAni.PlayRandingAnimation();
            randing = false;
        }
        else
        {
            playerAni.PlayReadyAnimation();
        }

        playerAni.AddForceJump();
    }


    void DropPlayer()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    void SaveScore()
    {
        PlayerData.instance.SetScore(score-1);
    }

    void Damage()
    {
        if (AnimationPlayCount > 3)
        {
            CancelInvoke("Damage");
            AnimationPlayCount = 0;
            return;
        }

        if (gameObject.activeSelf == true)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);

        AnimationPlayCount++;
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

    public void SetTileColor(Color color)
    {
        tileColor = color;
    }

    public void SetHit(bool value=true)
    {
        ishit = value;
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

    public (bool, int) isShild()
    {
        for(int i=2;i>=0;i--)
        {
            if(shiled[i] == true)
            {
                return (true,i);
            }
        }

        return (false,-1);
    }
    public void SetShildTime(bool value = true)
    {
        shiledTime = value;
    }

    public void SetShild(int index, bool value = true)
    {
        if (index < 0)
            return;

        shiled[index] = value;
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
        return body.color;
    }

}
