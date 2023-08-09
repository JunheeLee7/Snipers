using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PistolManager : MonoBehaviour
{
    Bullet_Pistol pis;
    SetWeapon weapons;
    Player player;
    InvensManager inven;

    public Action PistAnims;

    public GameObject bulletPistolPrefab;
    public int maxPistolBulletCount = 12;
    public int reloadPistolBulletCount = 12;
    public int currentPistolBulletCount;

    public GameObject pistolShotEffect;

    public bool isReloaded = false;

    AudioSource source;

    public TextMeshProUGUI curBulPis;
    public TextMeshProUGUI maxBulPis;

    private void Start()
    {
        weapons = FindObjectOfType<SetWeapon>();
        player = FindObjectOfType<Player>();
        source = GetComponent<AudioSource>();
        inven = FindObjectOfType<InvensManager>();
    }

    private void Update()
    {
        curBulPis.text = currentPistolBulletCount.ToString();
        maxBulPis.text = maxPistolBulletCount.ToString();

        player.isSetup = true;
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isReloaded)
            {
                if (weapons.isPist)
                {
                    if(!player.isZoomFalse)
                    {
                        if (currentPistolBulletCount >= 0 && currentPistolBulletCount <= maxPistolBulletCount)
                        {
                            int leftBullet = maxPistolBulletCount - currentPistolBulletCount;
                            if (inven.leftPistolBulletCount == 0)
                            {
                                Debug.Log("NULL");
                            }
                            else if (leftBullet == 0)
                            {

                            }
                            else if (inven.leftPistolBulletCount >= leftBullet)     // 총 남은 불릿 이 충전해야할 불릿보다 크거나 같을 경우 충전 후, -leftBullet
                            {
                                PistAnims?.Invoke();
                                source.Play();
                                currentPistolBulletCount += leftBullet;
                                inven.leftPistolBulletCount -= leftBullet;
                                isReloaded = true;
                            }
                            else if (inven.leftPistolBulletCount < leftBullet) // 나머지 불릿이 충전해야할 불릿보다 작을경우 나머지만큼 충전 후 0이되도록
                            {
                                PistAnims?.Invoke();
                                source.Play();
                                currentPistolBulletCount += inven.leftPistolBulletCount;
                                inven.leftPistolBulletCount = 0;
                                isReloaded = true;
                            }
                            isReloaded = false;
                        }
                    }
                }
            }
        }
    }

    public void  ShotPistolBullet()
    {
        if(currentPistolBulletCount <=0)
        {
            Debug.Log("NULL");
        }
        else
        {
            pis = (Instantiate(bulletPistolPrefab, transform) as GameObject).GetComponent<Bullet_Pistol>();
            GameObject obj = Instantiate(pistolShotEffect);
            obj.transform.position = transform.position;
            Destroy(obj,0.1f);

            pis.OnFire_Pistol();
            if(currentPistolBulletCount > 0)
            {
                currentPistolBulletCount--;
            }
            else
            {
                currentPistolBulletCount = 0;
            }
        }
    }
}
