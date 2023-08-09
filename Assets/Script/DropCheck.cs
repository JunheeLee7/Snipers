using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DropCheck : MonoBehaviour
{
    InventoryFloor floor;
    public Transform canvases;
    private GameObject inventory;
    private GameObject minmap;
    private bool isInven = false;

    private bool isChecking = false;

    private void Start()
    {
        floor = FindObjectOfType<InventoryFloor>();
        minmap = canvases.GetChild(2).gameObject;
        inventory = canvases.GetChild(4).gameObject;

        StartCoroutine(SetInventorys(0.01f));
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(!isInven)
            {
                minmap.SetActive(false);
                inventory.SetActive(true);
                isInven = true;
            }
            else
            {
                minmap.SetActive(true);
                inventory.SetActive(false);
                isInven = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.layer == 12)
        {
            // 아이템의 정보 추가
            if(!isChecking)
            {
                floor.GetItems
                    (
                    other.transform.GetComponent<ItemInformation>().item,
                    other.transform.GetComponent<ItemInformation>().pistolitems,
                    other.transform.GetComponent<ItemInformation>().heal
                    );
                isChecking = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.gameObject.layer == 12)
        {
            isChecking = false;
            Debug.Log("EWQS");
            floor.DestroyItems();
            Destroy(other.gameObject);
        }
    }

    IEnumerator SetInventorys(float t)
    {
        yield return new WaitForSeconds(t);
        inventory.SetActive(false);
    }
}
