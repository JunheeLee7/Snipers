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
        if (slot.Count == 0)        // 3���� ���� ����
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
                            // ���⼭ ���� ����
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
        for (int i = 0; i < slotHolders.childCount;++i)     // ����Ʈ���� �����Ǵ� ���� 2,1,0 ���̸� for������ i++�� ������ �Ǹ� for���� �������� 0�� �������� 2�� ã�������Ͽ� ������ ������.
        {
            slot.RemoveAt(0);
            Destroy(slotHolders.GetChild(i).gameObject);
        }
    }
}
