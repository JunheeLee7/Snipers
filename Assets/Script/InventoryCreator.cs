using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCreator : MonoBehaviour
{
    InvensManager invents;
    SniperManager sniperManager;
    PistolManager pistolManager;
    HealPackManager healPack;
    GrenadeManager grenadeManager;

    public int bulletCount;
    public int pistolCount;
    public int healPackCount;
    public int grenadeCount;

    private void Awake()
    {
        invents = FindObjectOfType<InvensManager>();
        sniperManager = FindObjectOfType<SniperManager>();
        pistolManager = FindObjectOfType<PistolManager>();
        healPack = FindObjectOfType<HealPackManager>();
        grenadeManager = FindObjectOfType<GrenadeManager>();
        bulletCount = invents.leftBulletCount;
        pistolCount = invents.leftPistolBulletCount;
        healPackCount = healPack.startHealPackCount;
        grenadeCount = grenadeManager.grenadeCounts;
    }

    private void Update()
    {
        bulletCount = invents.leftBulletCount;
        pistolCount = invents.leftPistolBulletCount;
        healPackCount = healPack.healPackCount;
        grenadeCount = grenadeManager.grenadeCounts;
    }
}
