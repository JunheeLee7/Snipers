using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SniperManager : MonoBehaviour
{
    Player player;
    SetWeapon weapons;
    Bullet bullet;
    AudioSource audios;

    InvensManager inven;

    public GameObject bulletPrefab;
    [SerializeField] private TextMeshProUGUI maxBull;
    [SerializeField] private TextMeshProUGUI curBull;


    public int bulletCount;               // 현재 총알 갯수
    public int reloadBulletCount = 12;    // 게임 시작시 탄창에 남아있는 총알 갯수
    public int maxBulletCount = 12;       // 총알의 최대갯수

    private bool isReload = false;  // 재장전확인
    public bool isZoom = false;

    public Action reloaded;


    public GameObject shotEffect;

    private void Start()
    {
        weapons = FindObjectOfType<SetWeapon>();
        audios = GetComponent<AudioSource>();
        bulletCount = reloadBulletCount;
        player = FindAnyObjectByType<Player>();
        inven = FindObjectOfType<InvensManager>();
    }

    private void Update()
    {
        OnReLoad();
        maxBull.text = maxBulletCount.ToString();
        curBull.text = bulletCount.ToString();
    }

    private void OnReLoad()     // 장전
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if (!weapons.isPist)
            {
                if(!player.isZoomFalse)
                {
                    StartCoroutine(bulletUp(3.0f));
                }
            }
        }
    }
    
    public void ShootBullet()       // 발사
    {
        if(bulletCount <= 0)
        {
            Debug.Log("NULL");
        }
        else
        {
            bullet = (Instantiate(bulletPrefab, transform) as GameObject).GetComponent<Bullet>();
            GameObject obj = Instantiate(shotEffect);
            obj.transform.position = transform.position;
            Destroy(obj, 0.1f);

            bullet.OnFire();
            bulletCount--;
            if(bulletCount <= 0)
            {
                bulletCount = 0;
            }
        }
    }

    IEnumerator bulletUp(float t)
    {
        if (bulletCount >= 0 && bulletCount < maxBulletCount)
        {
            isZoom = true;
            isReload = true;
            if (isReload)
            {
                reloaded?.Invoke();
                int left = maxBulletCount - bulletCount;        // 최대 - 현재 = 나머지
                audios.gameObject.SetActive(true);
                audios.Play();
                if (inven.leftBulletCount >= left)                    // 나머지가 총 총알 갯수보다 작거나 같으면 현재총알 12, left만큼 총에 뺴주기
                {
                    bulletCount = maxBulletCount;
                    inven.leftBulletCount -= left;
                    yield return new WaitForSeconds(t);
                }
                else
                {
                    bulletCount += inven.leftBulletCount;
                    inven.leftBulletCount = 0;
                    yield return new WaitForSeconds(t);
                }

                isReload = false;
                isZoom = false;
            }
            audios.gameObject.SetActive(false);
        }
    }
}
