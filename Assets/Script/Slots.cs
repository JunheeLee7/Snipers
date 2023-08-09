using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Slots : MonoBehaviour
{
    ItemInfo info;
    InventoryFloor invenFloor;
    InventoryCreator creator;
    InvensManager invent;
    HealPackManager healPack;

    private GameObject invensSlot;
    public Image itemImages;
    public Item item;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemCount;
    Item.ItemType itemType;

    public GameObject slotPrefab;
    private Transform slotParent;

    private int nowCount;
    public int slotCount;

    private void Awake()
    {
        invent = FindObjectOfType<InvensManager>();
        healPack = FindObjectOfType<HealPackManager>();
    }

    private void Start()
    {
        creator = FindObjectOfType<InventoryCreator>();
        invensSlot = FindObjectOfType<Item_Slot_CheckFor>().gameObject;
        invenFloor = FindObjectOfType<InventoryFloor>();
        slotParent = creator.transform;
    }

    public void AddItems(Item _item)
    {
        info = FindObjectOfType<ItemInfo>();
        // 슬롯의 패런츠에 자식의 갯수를 확인해서 이 프리팹을 자식의 갯수번째로 이동 : 갯수 * 110만큼 빼준다.
        item = _item;
        itemImages.sprite = item.itemImage;
        itemName.text = item.itemName.ToString();
        itemType = item.itemType;
        if (itemType == Item.ItemType.Bullets)
        {
            if(itemName.text == ".300 Magnum".ToString())
            {
                itemCount.text = info.counting.ToString();
            }
            else
            {
                itemCount.text = info.pisCounting.ToString();
            }
        }
        else if(itemType == Item.ItemType.HealKit)
        {
            itemCount.text = info.healCounting.ToString();
        }
    }

    public void SendInventory()
    {
        Slots_Inven[] inves = invensSlot.GetComponentsInChildren<Slots_Inven>();
        Debug.Log(invensSlot.transform.childCount);
        // 아이템의 이름을 확인해서 성립하면 그 아이템의 count + 현재 인벤토리 아이템의 카운트
        for (int i = 0; i < invensSlot.transform.childCount; i ++)
        {
            if (itemName.text.ToString() == inves[i].itemName_Inven.text.ToString())            // 이름이 같을 경우
            {
                if(itemName.text == ".300 Magnum".ToString())
                {
                    nowCount = info.counting;
                    int newCount = invent.leftBulletCount;
                    invent.leftBulletCount = newCount + nowCount;      // 개수 증가
                    Debug.Log(invent.leftBulletCount);
                }
                else if(itemName.text == "9mm")
                {
                    nowCount = info.pisCounting;
                    int newCoun = creator.pistolCount;
                    invent.leftPistolBulletCount = newCoun + nowCount;      // 개수 증가
                    Debug.Log(invent.leftPistolBulletCount);
                }
                else if(itemName.text == "HEAL KIT")
                {
                    nowCount = info.healCounting;
                    int newCount = creator.healPackCount;
                    healPack.healPackCount = newCount + nowCount;      // 개수 증가
                    Debug.Log(healPack.healPackCount);
                }
                inves[i].itemCount_Inven.text  = slotCount.ToString();

                invenFloor.slot.RemoveAt(0);
                Destroy(gameObject);
            }
            else if(itemName.text.ToString() != inves[i].itemName_Inven.text.ToString())
            {
                if(itemType == Item.ItemType.Others)
                {
                    GameObject otherSlots =  Instantiate(slotPrefab, slotParent);
                    Slots_Inven invenSlots = otherSlots.GetComponent<Slots_Inven>();
                    invenSlots.itemImage_Inven.sprite = item.itemImage;
                    invenSlots.itemName_Inven.text = item.itemName.ToString();
                    int otherCount = 1;
                    invenSlots.itemCount_Inven.text = otherCount.ToString();
                }
            }
        }
    }
}
