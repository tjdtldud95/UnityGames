using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public TilesManager tiles;
    public INGameCavas inGameCanvas;
    public GameManager gm;
    public CameraMove camera;
    public Player player;

    void Start()
    {
        for(int i=0;i<transform.childCount;i++)
        {
            if(PlayerData.instance.name.Equals(transform.GetChild(i).name))
            {
                player = transform.GetChild(i).GetComponent<Player>();
                transform.GetChild(i).gameObject.SetActive(true);
                break;
            }
        }

        tiles.SetTilePlayerBody(player);
        inGameCanvas.player = player;
        inGameCanvas.playerRenderer = player.body;
        gm.player = player;
        camera.player = player;
    }
}
