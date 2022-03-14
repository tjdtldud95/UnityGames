using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    void Start()
    {
        for(int i=0;i<transform.childCount;i++)
        {
            if(PlayerData.instance.CompareTag(transform.GetChild(i).name))
            {
                transform.GetChild(i).gameObject.SetActive(true);
                break;
            }
        }
    }
}
