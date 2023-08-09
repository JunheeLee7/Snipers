using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SniperLockOn : MonoBehaviour
{
    SniperManager sManage;
    private Transform rase;
    Player player;

    private void Start()
    {
        sManage = FindObjectOfType<SniperManager>();
        rase = transform.GetChild(0);
        player = FindObjectOfType<Player>();
        player.LockZoom += LockSet;
        player.ZoomFalse += LockSetFalse;
    }

    public void LockSet()
    {
        rase.gameObject.SetActive(true);
    }

    public void LockSetFalse()
    {
        rase.gameObject.SetActive(false);
    }

}
