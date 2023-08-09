using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item / item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Bullets,
        HealKit,
        Grenade,
        Others
    }
    public string itemName;
    public Sprite itemImage;
    public GameObject itemPrefab;
    public int itemCount;
    public ItemType itemType;
}
