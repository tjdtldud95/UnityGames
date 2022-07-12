using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public AudioClip audioJump;
    public AudioClip audioFail;
    public TilesManager tiles;
    public SpriteRenderer body;
    public GameObject[] shiledOb;
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
    int useStar = 0;
    public int tilesIndex = 0;
    int AnimationPlayCount = 0;
    bool isMove;
    bool die;
    public bool shiledTime;
    bool randing;
    bool ishit;
    bool isReset;
    bool[] shiled = new bool[3];
    bool getStar = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        playerAni = GetComponent<PlayerAnimation>();

        SettingCheckPoint();

        rb.bodyType = RigidbodyType2D.Dynamic;
        for(int i=0;i<3;i++)
        {
            shiledOb[i].SetActive(false);
        }
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
                    shiledOb[i].SetActive(false);
                    break;
                }
            }

            if (shiledTime)
            {
                InvokeRepeating(nameof(Damage), 0.3f, 0.2f);
                getStar = true;
                useStar++; 
                ishit = false;
                return;
            }

            playerAni.PlayHitAnimation();
            PlaySound("Fail");
            rb.bodyType = RigidbodyType2D.Kinematic;
            Invoke(nameof(DropPlayer), 1f);

            SaveScore();
            PlayerData.instance.SetusingShildNum(useStar);
            if (getStar)
                PlayerData.instance.SetplayingNum();
            
            die = true;
        }
        
    }

    void SettingCheckPoint()
    {
        if (!PlayerData.instance.goCheckPoint)
            return;

        score = 50;
        Vector3 move = transform.position + (Vector3.up * 25f)*5;  //-2.5 + 20  = 18
        transform.position = move;
    }

    void SetNextPos()
    { 
        score++;
        curuntPos = nextPos;
        nextPos =tiles.GetTilesPos(tilesIndex++)+ Vector2.up;
        tilesIndex %= 10;
    }
    
    public int GetScore()
    {
        return score-1;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (die) return;

        if(isReset)
        {
            if (jumpCount >= 3)
            {
                jumpCount = -1;
                isMove = true;
                isReset = false;
                if (shiledTime) shiledTime = false;

                if (collision.gameObject.layer.Equals(3))
                {
                    collision.gameObject.SetActive(false);
                }
            }

        }
        else
        {
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
                    shiledOb[i].SetActive(true);
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

        if (isReset)
        {
            if (jumpCount >= 3)
            {
                randing = true;
            }

        }
        else
        {
            if (jumpCount == maxJumpCount)
                randing = true;
        }

        if (collision.gameObject.layer==3 || shiledTime)
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

    public void PlayerReset()
    {
        transform.position = curuntPos+Vector2.up*1f;
        gameObject.SetActive(true);
        die = false;
        shiledTime = true;
        ishit = false;
        isReset = true;
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
            shiledTime = false;
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
