using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Player player;
    public SpriteRenderer playerBody;
    SpriteRenderer TileSR;
    int AnimationPlayCount;

    private void Start()
    {
        TileSR = GetComponent<SpriteRenderer>();
    }

    public void StartDieAnimation()
    {
        InvokeRepeating(nameof(PlayingDieAnimation), 0.3f,0.3f);
    }

    void PlayingDieAnimation()
    {
        if (AnimationPlayCount >= 2)
        {
            CancelInvoke("PlayingDieAnimation");           
        }

        if (gameObject.activeSelf == true)        
            gameObject.SetActive(false);
        else        
            gameObject.SetActive(true);
        
        AnimationPlayCount++;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("ab");
        if(playerBody.color != TileSR.color)
        {
            var it = player.isShild();
            if(it.Item1)
            {
                player.SetHit(true);
                return;
            }

            StartDieAnimation();
            player.SetHit(true);
            player.SetTileColor(TileSR.color);

        }
    }
    


}
