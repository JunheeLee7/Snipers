using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public int counting;
    public int pisCounting;
    public int healCounting;

    public bool isItemDeleteCheck = false;

    InventoryCreator iC;
    DropDest dDest;

    private void Awake()
    {
        iC = FindObjectOfType<InventoryCreator>();
    }

    private void Start()
    {
        counting = Random.Range(4, 7);
        pisCounting = Random.Range(4, 7);
        healCounting = Random.Range(0, 2);
    }

    private void Update()
    {
        //if (isItemDeleteCheck)
        //{
        //    if (iC.transform.GetChild(4).gameObject == true)
        //    {
        //        dDest = FindObjectOfType<DropDest>();
        //        if (dDest != null)
        //        {

        //            if (dDest.transform.childCount == 0)
        //            {
        //                Destroy(gameObject);
        //            }
        //            else
        //            {

        //            }
        //        }

        //    }
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10)
        {
            isItemDeleteCheck = true;

        }
    }
}

