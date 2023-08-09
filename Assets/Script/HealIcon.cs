using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealIcon : MonoBehaviour
{
    public GameObject lightEffect;
    SetWeapon set;
    HPManager manager;

    private Image fillImage;

    private void Start()
    {
        manager = FindObjectOfType<HPManager>();
        set = FindObjectOfType<SetWeapon>();
        fillImage = transform.GetChild(1).gameObject.GetComponent<Image>();
        fillImage.fillAmount = 0;
        set.healiconActivate += CoruSet;
    }

    public void CoruSet()
    {
        StartCoroutine(HealCoolTime(4));
    }

    IEnumerator HealCoolTime(float t)
    {
        if (manager.currentHP < manager.maxHP)
        {
            while (fillImage.fillAmount < 1.0f)
            {
                if (set.isHeal)
                {
                    fillImage.fillAmount += (1/t) * Time.deltaTime;
                    yield return null;
                }
            }
            GameObject obj = Instantiate(lightEffect);
            obj.transform.parent = gameObject.transform;
            set.isHeal = false;
            fillImage.fillAmount = 0.0f;
        }
    }
}
