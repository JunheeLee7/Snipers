using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slots_Inven : MonoBehaviour
{
    InventoryCreator creator;
    public TextMeshProUGUI itemName_Inven;
    public TextMeshProUGUI itemCount_Inven;
    public Image itemImage_Inven;

    private void Start()
    {
        creator = FindObjectOfType<InventoryCreator>();
    }

    private void Update()
    {
        if (itemName_Inven.text == ".300 Magnum".ToString())
        {
            itemCount_Inven.text = creator.bulletCount.ToString();
        }
        else if (itemName_Inven.text == "9mm".ToString())
        {
            itemCount_Inven.text = creator.pistolCount.ToString();
        }
        else if (itemName_Inven.text == "HEAL KIT".ToString())
        {
            itemCount_Inven.text = creator.healPackCount.ToString();
        }
        else if(itemName_Inven.text == "Grenade".ToString())
        {
            itemCount_Inven.text = creator.grenadeCount.ToString(); 
        }
    }
}
