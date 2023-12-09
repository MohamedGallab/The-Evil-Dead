using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class InventoryManagerScript : MonoBehaviour
{
    static Item[] Items = new Item[7];

    [SerializeField]
    GameObject[] Slots;

    int CurrentSlotIndex = 1; // Because slot 0 is reserved for the knife

    public void PickUpItem(Item newItem)
    {
        
        // Assign the picked up item type to the next open index
        Items[CurrentSlotIndex] = newItem;

        // Initialize the picked up item to a new slot
        InventorySlot slotScript = Slots[CurrentSlotIndex].GetComponent<InventorySlot>();
        slotScript.InitializeItem(newItem);
        
        // Increment index
        CurrentSlotIndex++;
        
    }

    public bool CanPickupItem()
    {
        if (CurrentSlotIndex >= 7)
        {
            return false;
        }

        return true;
    }
}
