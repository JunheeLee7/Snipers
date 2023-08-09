using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyPadCheck : MonoBehaviour
{
    DoorOpen open;

    string code = "0781";
    string now_Texts;
    int index = 0;
    public TextMeshProUGUI text;
    public GameObject doors;
    public GameObject target;

    private void Start()
    {
        open = FindObjectOfType<DoorOpen>();
        gameObject.SetActive(false);
    }

    public void CountUP(string num)
    {
        index++;
        now_Texts += num;
        text.text = now_Texts;
    }

    public void GreenButtonUp()
    {
        if(now_Texts == code)
        {
            Debug.Log("Start");
            open.isOpen = true;
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("WRONG");
            RedButtonUp();
        }
    }

    public void RedButtonUp()
    {
        index++;
        now_Texts = null;
        text.text = now_Texts;
    }
}
