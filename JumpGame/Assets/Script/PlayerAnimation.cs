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
    public float asd;
    public float aaa;
    private void Start()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void PlayReadyAnimation()
    {
        ani.SetBool("Ready", true);
        ani.SetBool("Jump", false);
        ani.SetBool("Randing", false);
        ready = true;
        motioning = true;
    }

    public void PlayJumpAnimaition()
    {
        ani.SetBool("Jump", true);
        ani.SetBool("Ready", false);
        ani.SetBool("Randing", false);
    }

    public void PlayRandingAnimation()
    {
        ani.SetBool("Randing", true);
        ani.SetBool("Ready", false);
        ani.SetBool("Jump", false);
        ready = false;
        
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
        PlayingMotion();
        PlayingJump();
    }


    void PlayingMotion()
    {
        if(ready)
        {
            Debug.Log("Playing Land");
            PlayRandingAnimation();
            return;
        }
        Debug.Log("Playing Ready");
        PlayReadyAnimation();
    }

    void PlayingJump()
    {
        StartCoroutine(nameof(Jump));
    }


    IEnumerator Jump()
    {
        if(motioning)
        {
            motioning = false;
            yield return new WaitForSeconds(0.7f);
            rb.AddForce(Vector2.up * 200f);
        }
        else
        {
            yield return new WaitForSeconds(0.7f);
            rb.AddForce(Vector2.up * 200f);
        }
      
    }
}
