using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyPadManager : MonoBehaviour
{
    InventoryCreator creator;
    public TextMeshProUGUI texting;

    private void Start()
    {
        creator = FindObjectOfType<InventoryCreator>();
        texting.color = new Color(texting.color.r, texting.color.g, texting.color.b, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                StopCoroutine(StartCoroutine(PressFCode(1.5f)));
                creator.transform.GetChild(6).gameObject.SetActive(true);
            }
        }
    }

    IEnumerator PressFCode(float t)
    {
        while(texting.color.a < 1)
        {
            float alpha = Time.deltaTime / t;
            texting.color = new Color(texting.color.r, texting.color.g, texting.color.b, texting.color.a + alpha);
            yield return null;
        }
        texting.color = new Color(texting.color.r, texting.color.g, texting.color.b, 1);

        while(texting.color.a > 0)
        {
            float alpha = Time.deltaTime / t;
            texting.color = new Color(texting.color.r, texting.color.g, texting.color.b, texting.color.a - alpha);
            yield return null;
        }
        texting.color = new Color(texting.color.r, texting.color.g, texting.color.b, 0);
    }
}
