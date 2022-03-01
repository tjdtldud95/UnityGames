using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerAnimation : MonoBehaviour
{
    Animator ani;
    Rigidbody2D rb;
    bool motioning = false;
    bool jump;

    private void Start()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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


    public void PlayReadyAnimation()
    {
        ani.SetBool("Ready", true);
        ani.SetBool("Jump", false);
        ani.SetBool("Randing", false);

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
        motioning = true;
    }

    public void AddForceJump()
    {
        StartCoroutine(nameof(Jump));
    }

    public void SetJump(bool value = true)
    {
        jump = value;
    }

    IEnumerator Jump()
    {
        if (motioning)
        {
            motioning = false;
            yield return new WaitForSeconds(0.45f);
            rb.AddForce(Vector2.up * 200f);
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
            rb.AddForce(Vector2.up * 200f);
        }
    }

}
