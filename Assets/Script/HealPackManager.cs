using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPackManager : MonoBehaviour
{
    HPManager manager;

    public int startHealPackCount = 3;
    public int healPackCount;
    public float healing = 2.0f;

    public bool isHealings = false;

    private void Awake()
    {
        healPackCount = startHealPackCount;
        manager = FindObjectOfType<HPManager>();
    }

    public void HealPackUse()
    {
        if(manager.currentHP < manager.maxHP)
        {
            isHealings = true;
            if (isHealings)
            {
                if (healPackCount > 0)
                {
                    StartCoroutine(healthCheck(4.0f));
                    isHealings = false;
                }
                else
                {
                    healPackCount = 0;
                    isHealings = false;
                }
            }
        }
    }

    IEnumerator healthCheck(float t)
    {
        healPackCount--;
        float targetHP = manager.currentHP + healing;
        Debug.Log(targetHP);
        if(manager.currentHP < targetHP)
        {
            while(manager.currentHP < targetHP)
            {
                manager.currentHP += healing * Time.deltaTime;
                yield return null;
            }
            manager.currentHP = targetHP;
        }
        yield return new WaitForSeconds(t);
    }
}
