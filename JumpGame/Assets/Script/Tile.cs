using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    int AnimationPlayCount;

    public void StartDieAnimation()
    {
        InvokeRepeating("PlayingDieAnimation", 0.3f,0.3f);
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
}
