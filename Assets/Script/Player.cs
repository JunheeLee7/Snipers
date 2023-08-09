using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static Unity.VisualScripting.Member;

public class Player : MonoBehaviour
{
    public Action pistolShot;
    SetWeapon weapons;

    public enum PlayerState
    {
        Walk,
        Run,
        Zoom
    }
    SniperManager sni;
    public float moveSpeed = 1.0f;
    public float runSpeed = 2.0f;

    public float rotationSpeed = 360.0f;

    Vector3 inputDir = Vector3.zero;

    PlayerInputSystem playerInput;
    Rigidbody rigid;

    Animator _anim = null;

    Vector2 desireDir = Vector2.zero;
    Vector2 myDir = Vector2.zero;

    public bool setup = false;
    public bool isSetup = false;
    public GameObject sniper;

    public Action LockZoom;
    public Action ZoomFalse;

    public Camera cameras;
    public Camera cam_Pist;
    public Camera weaponCamera;

    // 인터벌
    private bool isCanShoot = false;
    public bool isRebound = false;

    // 리로드
    SniperManager sManager;
    PistolManager pistolManager;
    private bool isPistReloadCheck = false;
    public bool isZoomFalse = false;

    // 오디오
    AudioSource source;
    Coroutine cor;
    private bool isSource = false;
    private bool isRunnig = false;

    PistolAnimatorControl pAnim;

    GrenadeManager grenadeManager;
    public bool isThrowGrenade = false;

    private Animator anim
    {
        get
        {
            if (_anim == null)
            {
                _anim = GetComponent<Animator>();
                if (_anim != null)
                {
                    _anim = GetComponentInChildren<Animator>();
                }
            }
            return _anim;
        }
    }

    private void Awake()
    {
        playerInput = new PlayerInputSystem();
        rigid = GetComponent<Rigidbody>();
        sni = FindObjectOfType<SniperManager>();
        grenadeManager = FindObjectOfType<GrenadeManager>();
    }
    private void Start()
    {
        pAnim = FindObjectOfType<PistolAnimatorControl>();
        weapons = GetComponent<SetWeapon>();
        source = GetComponent<AudioSource>();
        sManager = FindAnyObjectByType<SniperManager>();
        sManager.reloaded += ReloadAnim;
        StartCoroutine(PlayerSounds());
    }

    private void Update()
    {
        // Moving();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("IsRun", true);
            isRunnig = true;
        }
        else
        {
            anim.SetBool("IsRun", false);
            isRunnig = false;
        }

        desireDir.x = Input.GetAxisRaw("Horizontal");
        desireDir.y = Input.GetAxisRaw("Vertical");

        // 여기서  관리(이동)
        myDir = Vector2.Lerp(myDir, desireDir, Time.deltaTime * 5.0f);
        anim.SetFloat("x", myDir.x);
        anim.SetFloat("y", myDir.y);

        if(rigid.velocity.magnitude >= 1.0f)
        {
            isSource = true;
        }
        else
        {
            isSource = false;
        }

        // 줌관리
        if(sni != null)
        {
            if (!sni.isZoom)
            {
                LockOn();
            }
        }

        // 피스톨 리로드
        if(pistolManager != null)
        {
            isPistReloadCheck = true;
            if(isPistReloadCheck)
            {
                pistolManager.PistAnims += PistolAnimations;
                isPistReloadCheck = false;
            }
        }
        
        // 연막탄 확인
        if (Input.GetMouseButtonUp(0))
        {
            if (isThrowGrenade)
            {
                anim.SetTrigger("Grenade");
                weapons.isGred = false;
                isThrowGrenade = false;
                StartCoroutine(WaitingFalse(2.0f));
            }
        }
    }

    public void AltStop()
    {
        rotationSpeed = 0.0f;
    }

    public void AltGo()
    {
        rotationSpeed = 360.0f;
    }

    private void AnimationProjection()
    {
        anim.SetFloat("x", Input.GetAxis("Horizontal"));
        anim.SetFloat("y", Input.GetAxis("Vertical"));
    }

    // 조준 상태 활성화 --------------------------------------------------------------------
    private void LockOn()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if (!weapons.isPist && !weapons.isOthers)
            {
                Quaternion rect = transform.rotation;
                if (setup == false)
                {
                    // 조준점 생성 (sniper)
                    cameras.gameObject.SetActive(true);
                    weaponCamera.gameObject.SetActive(false);
                    transform.rotation = rect;

                    anim.SetBool("LockOn", true);
                    setup = true;
                    LockZoom?.Invoke();
                    isZoomFalse = true;
                }
                else
                {
                    // 조준점 해제(sniper)
                    anim.SetBool("LockOn", false);
                    setup = false;
                    ZoomFalse?.Invoke();
                    cameras.gameObject.SetActive(false);
                    weaponCamera.gameObject.SetActive(true);
                    isZoomFalse = false;
                }
                isZoomFalse = false;
            }
            else if(weapons.isPist && !weapons.isOthers)
            {
                pistolManager = FindAnyObjectByType<PistolManager>();
                isZoomFalse = true;

                if (isZoomFalse)
                {
                    Quaternion rect = transform.rotation;
                    if (setup == false)
                    {
                        // 조준점 생성 (sniper)
                        cam_Pist.gameObject.SetActive(true);
                        weaponCamera.gameObject.SetActive(false);
                        transform.rotation = rect;

                        anim.SetBool("LockOn", true);
                        setup = true;
                    }
                    else
                    {
                        // 조준점 해제(sniper)
                        anim.SetBool("LockOn", false);
                        setup = false;
                        cam_Pist.gameObject.SetActive(false);
                        weaponCamera.gameObject.SetActive(true);
                        isZoomFalse = false;
                    }
                }
            }
            else if(weapons.isOthers)
            {
                ThrowThis();
            }

        }
        if(setup == true || isSetup)
        {
            ShootingBullet();
        }
    }

    private void ShootingBullet()
    {
        StartCoroutine(ShootingInterval(0));
    }

    IEnumerator DelayTime(float t)
    {
        isCanShoot = true;
        yield return new WaitForSeconds(t);
        isCanShoot = false;
    }

    public void ReloadAnim()
    {
        anim.SetTrigger("Reload");
    }

    IEnumerator ShootingInterval(float t)
    {

        //yield return new WaitForSeconds(t);
        if (Input.GetMouseButtonDown(0) && !isCanShoot)      // 쏘고 5초뒤에
        {
            if(!weapons.isPist && weapons.isSni)
            {
                anim.SetTrigger("tri");
                sni.ShootBullet();
                isRebound = true;
                StartCoroutine(DelayTime(3.0f));

                yield return null;
            }
            else if(weapons.isPist && !weapons.isSni)
            {
                pistolManager = FindAnyObjectByType<PistolManager>();
                Debug.Log("Elp");
                pistolShot?.Invoke();
                // 불렛 생성 및 딜레이 추가
                pistolManager.ShotPistolBullet();
                StartCoroutine(DelayTime(0.5f));
            }
        }
    }

    IEnumerator PlayerSounds()
    {
        while(true)
        {
            if(isSource && !isRunnig)
            {
                source.Play();
                yield return new WaitForSeconds(0.47f);
            }
            else if(isSource && isRunnig)
            {
                source.Play();
                yield return new WaitForSeconds(0.27f);
            }
            else
            {
                source.Stop();
                yield return null;
            }
        }
    }

    public void PistolAnimations()
    {
        anim.SetTrigger("PistReload");
    }

    public  void ThrowThis()
    {
        if(!weapons.isGred)
        {
            anim.SetBool("IsGrenade", true);
            weapons.isGred = true;
            isThrowGrenade = true;
        }
        else
        {
            anim.SetBool("IsGrenade", false);
            weapons.isGred = false;
            isThrowGrenade = false;
        }
    }

    IEnumerator WaitingFalse(float t)
    {
        yield return new WaitForSeconds(t);
        anim.SetBool("IsGrenade", false);
        weapons.isOthers = false;
        weapons.SniperSetting();
        yield return new WaitForSeconds(0.1f);
        if(grenadeManager.grenadeCounts > 0)
        {
            grenadeManager.grenadeCounts--;
        }
        if(grenadeManager.grenadeCounts < 0)
        {
            grenadeManager.grenadeCounts = 0;
        }
        yield return new WaitForSeconds(0.5f);
        // weapons.SniperSetting();
    }
}
