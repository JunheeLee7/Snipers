using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    // 체력바
    public float maxHP = 10.0f;
    public float currentHP;
    public float nowHP;

    public float damage = 2.0f;

    public LayerMask masks;

    private void Start()
    {
        currentHP = maxHP;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == masks)
        {
            // 타격을 입었을 경우 현재 HP - 데미지
            StartCoroutine(HPPackage());
        }
    }

    IEnumerator HPPackage()
    {
        if (currentHP <= 0.0f)
        {
            currentHP = 0.0f;
            yield return null;
        }
        else
        {
            float targetHP = currentHP - damage;
            if(currentHP > targetHP)
            {
                while(currentHP > targetHP)
                {
                    currentHP -= damage * Time.deltaTime;
                    yield return null;
                }
                currentHP = targetHP;
            }
        }
    }
}
