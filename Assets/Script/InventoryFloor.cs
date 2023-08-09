using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryFloor : MonoBehaviour
{
    public List<Slots> slot = new List<Slots>();
    public GameObject slotPrefab;
    public Transform slotHolders;

    public bool isHeal = false;

    private void Start()
    {
        slot.InsertRange(0,slotHolders.GetComponentsInChildren<Slots>());
    }

    public void GetItems(Item items, Item pis, Item heal)
    {
        if (slot.Count == 0)        // 3개의 슬롯 생성
        {
            for (int i = 0; i < 3; i++)
            {
                slot.Add(Instantiate(slotPrefab, slotHolders).GetComponent<Slots>());
                if (slot[i] != null)
                {
                    if (i == 0)
                    {
                        slot[i].AddItems(items);
                    }
                    else if(i == 1)
                    {
                        slot[i].AddItems(pis);
                    }
                    else if(i == 2)
                    {
                        slot[i].AddItems(heal);

                        if (slot[i].itemCount.text == "0".ToString())
                        {
                            Debug.Log("SA");
                            // 여기서 슬롯 삭제
                            Destroy(slot[i].gameObject);
                            slot.RemoveAt(i);
                        }
                    }
                    else
                    {
                        Debug.Log("ERROR");
                    }
                }
            }
        }
    }

    public void DestroyItems()
    {
        //slot.Clear();
        for (int i = 0; i < slotHolders.childCount;++i)     // 리스트에서 삭제되는 것은 2,1,0 순이며 for문에서 i++를 돌리게 되면 for문을 통해지워 0만 남았지만 2를 찾으려고하여 오류가 생겻음.
        {
            slot.RemoveAt(0);
            Destroy(slotHolders.GetChild(i).gameObject);
        }
    }
}
