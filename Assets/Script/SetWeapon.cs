using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SetWeapon : MonoBehaviour
{
    Player player;
    Animator anim;
    public Transform weapons;
    private GameObject snip;
    private GameObject pist;
    private GrenadeManager grenadePlace;
    public GameObject grenade;

    public int layerIndex = 0;
    [Range(0f, 1f)]
    public float targetWeight = 1.0f;
    [Range(0f, 1f)]
    public float minWeight = 0.0f;
    public float weight;
    public float weightOthers;

    public bool isPist = false;
    public bool isHeal = false;
    public bool isSni = true;

    HealPackManager healPack;
    HealIcon healIcon;

    public GameObject CheckLine;
    public GameObject PistolCheckLine;
    private bool isChecked = false;

    public Action healiconActivate;

    public bool isGred = false;
    public bool isOthers = false;

    private void Start()
    {
        grenadePlace = FindObjectOfType<GrenadeManager>();
        player = FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
        snip = weapons.GetChild(0).gameObject;
        pist = weapons.GetChild(1).gameObject;

        SniperSetting();
        //snip.SetActive(true);
        //pist.SetActive(false);
        //grenade.SetActive(false);

        weight = anim.GetLayerWeight(layerIndex);
        healIcon = FindObjectOfType<HealIcon>();
    }

    private void Update()
    {
        SelectKey();
        if(isPist)
        {
            ChangeLayer2();
        }
        else
        {
            ChangeLayer1();
        }
    }


    public void SelectKey()
    {
        if(!player.isZoomFalse && !player.setup)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SniperSetting();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                PistolSetting();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                HealpackSetting();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                GrenadeSetting();
            }
        }
        if(isChecked)   // 2¹ø
        {
            PistolCheckLine.SetActive(true);
            CheckLine.SetActive(false);
        }
        else
        {
            CheckLine.SetActive(true);
            PistolCheckLine.SetActive(false);
        }
    }

    private void ChangeLayer1()
    {
        weight = Mathf.Lerp(weight, targetWeight, Time.deltaTime * 1.0f);
        anim.SetLayerWeight(0, weight);
        weightOthers = Mathf.Lerp(weightOthers, minWeight, Time.deltaTime * 1.0f);
        anim.SetLayerWeight(1, weightOthers);
    }
    private void ChangeLayer2()
    {
        weightOthers = Mathf.Lerp(weightOthers, minWeight, Time.deltaTime * 1.0f);
        anim.SetLayerWeight(0, weightOthers);
        weight = Mathf.Lerp(weight, targetWeight, Time.deltaTime * 1.0f);
        anim.SetLayerWeight(1, weight);
    }

    public void SniperSetting()
    {
        player.isSetup = false;
        isSni = true;
        isChecked = false;
        isPist = false;
        pist.SetActive(false);
        snip.SetActive(true);
        grenade.SetActive(false);
    }

    public void PistolSetting()
    {
        isSni = false;
        isChecked = true;
        isPist = true;
        snip.SetActive(false);
        pist.SetActive(true);
        grenade.SetActive(false);
    }

    public void HealpackSetting()
    {
        // Èú
        isSni = false;
        isHeal = true;
        healPack = FindObjectOfType<HealPackManager>();
        healPack.HealPackUse();
        healiconActivate?.Invoke();
    }

    public void GrenadeSetting()
    {
        // ¼ö·ùÅº
        isSni = false;
        isOthers = true;
        pist.SetActive(false);
        snip.SetActive(false);
        if(grenadePlace.transform.childCount == 0)
        {
            GameObject obj = Instantiate(grenade,grenadePlace.transform);
            obj.transform.position = grenadePlace.transform.position;
            obj.gameObject.SetActive(true);
        }
    }
}
