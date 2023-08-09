using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquipCount : MonoBehaviour
{
    SniperManager sniperManager;
    PistolManager pistolManager;
    HealPackManager healPackManager;
    GrenadeManager grenadeManager;

    public TextMeshProUGUI nowCount;
    public TextMeshProUGUI maxCount;
    public TextMeshProUGUI pisNowCount;
    public TextMeshProUGUI maxNowCount;
    public TextMeshProUGUI healNowCount;
    public TextMeshProUGUI grenadeCount;

    private int bulC;
    private int maxBulC;
    private int pisC;
    private int maxPisC;
    private int healC;
    private int grenadeC;

    private int nowSni;
    private int maxNowSni;
    private int nowPis;
    private int maxNowPis;

    private void Awake()
    {
        sniperManager = FindObjectOfType<SniperManager>();
        pistolManager = FindObjectOfType<PistolManager>();
        healPackManager = FindObjectOfType<HealPackManager>();
        grenadeManager = FindObjectOfType<GrenadeManager>();
    }

    private void Update()
    {
        healC = healPackManager.healPackCount;
        grenadeC = grenadeManager.grenadeCounts;

        if(sniperManager != null)
        {
            bulC = sniperManager.bulletCount;
            maxBulC = sniperManager.maxBulletCount;
            nowSni = bulC;
            maxNowSni = maxBulC;
        }
        else if(sniperManager == null)
        {
            bulC = nowSni;
            maxBulC = maxNowSni;
        }

        if (pistolManager != null)
        {
            pisC = pistolManager.currentPistolBulletCount;
            maxPisC = pistolManager.maxPistolBulletCount;
            nowPis = pisC;
            maxNowPis = maxPisC;
            
        }
        else if(pistolManager == null)
        {
            pisC = nowPis;
            maxPisC = maxNowPis;
        }

        nowCount.text = bulC.ToString();
        maxCount.text = maxBulC.ToString();
        pisNowCount.text = pisC.ToString();
        maxNowCount.text = maxPisC.ToString();
        healNowCount.text = healC.ToString();
        grenadeCount.text = grenadeC.ToString();
    }
}
