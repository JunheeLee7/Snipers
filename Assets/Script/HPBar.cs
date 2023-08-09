using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    Slider slider;
    HPManager manager;

    private void Start()
    {
        slider = GetComponent<Slider>();
        manager = FindObjectOfType<HPManager>();
    }

    private void Update()
    {
        // currentHP = nowHP - damage가 될 때까지 감소
        slider.value = manager.currentHP / manager.maxHP;
    }
}
