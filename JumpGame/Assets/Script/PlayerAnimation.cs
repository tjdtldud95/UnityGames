using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerAnimation : MonoBehaviour
{
    Animator ani;
    Rigidbody2D rb;
    public bool ready = false;
    public bool motioning = false;
    public bool jump;
    public int size;
    public int a = 0;
    public bool hit;
    public GameObject asd;
    private void Start()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(a>=10)
        {
            asd.SetActive(false);
            ani.SetBool("Hit", true);
        }
    }

    public void PlayReadyAnimation()
    {
        ani.SetBool("Ready", true);
        ani.SetBool("Jump", false);
        ani.SetBool("Randing", false);
        ready = true;
       
    }

    public void PlayJumpAnimaition()
    {
        ani.SetBool("Jump", true);
        ani.SetBool("Ready", false);
        ani.SetBool("Randing", false);

        if(jump)
            ani.SetBool("Randing", true);
    }

    public void PlayRandingAnimation()
    {
        ani.SetBool("Randing", true);
        ani.SetBool("Ready", false);
        ani.SetBool("Jump", false);
        ready = false;
        motioning = true;
    }

    public void PlayHitAnimation()
    {
        ani.SetBool("Hit", true);
        ani.SetBool("Ready", false);
        ani.SetBool("Jump", false);
        ani.SetBool("Randing", false);
    }
    


    public void ResetAnimation()
    {
        ani.SetBool("Hit", false);
        ani.SetBool("Ready", false);
        ani.SetBool("Jump", false);
        ani.SetBool("Randing", false);
    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        PlayJumpAnimaition();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(size >=4)
        {
            PlayRandingAnimation();
            PlayingJump();
            size = 0;
            return;
        }

        PlayReadyAnimation();
        PlayingJump();
        size++;
        a++;
    }


    void PlayingJump()
    {
        StartCoroutine(nameof(Jump));
    }

    IEnumerator Jump()
    {
        if (motioning)
        {
            motioning = false;
            yield return new WaitForSeconds(0.45f);
            rb.AddForce(Vector2.up * 400f);
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
            rb.AddForce(Vector2.up * 200f);
        }

    }

}
