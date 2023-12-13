using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoreManager : MonoBehaviour, IStoreSlotHandler
{
    [SerializeField] InventoryManagerScript InventoryManagerScript;
    [SerializeField] GameObject StoreSlotPrefab;
    [SerializeField] GameObject StoreListContent;
    [SerializeField] GameObject InventoryListContent;

    List<StoreSlot> InventorySlots;
    List<StoreSlot> StoreSlots;

    // Handles the initialization of the store and inventory lists
    void Awake()
    {
        StoreSlots = new List<StoreSlot>();
        InventorySlots = new List<StoreSlot>();
        string[] storeItemsPaths = new string[] { "Weapons", "Ammo", "Grenades", "Gunpowder", "Herbs" };

        foreach (string path in storeItemsPaths)
        {
            var loadedobjects = Resources.LoadAll(path);
            foreach (Item loadedobject in loadedobjects)
            {
                if (loadedobject.IsBuyable)
                {
                    AddStoreSlot(loadedobject as Item);
                }
            }
        }

        foreach (GameObject loadedobject in InventoryManagerScript.Slots)
        {
            InventorySlot inventorySlot = loadedobject.GetComponent<InventorySlot>();
            if (inventorySlot.CurrentItem != null && inventorySlot.CurrentItem.IsSellable)
            {
                AddInventorySlot(inventorySlot.CurrentItem, inventorySlot.StackCount);
            }
        }
    }

    // Adds a new Store Slot to the Store List
    void AddStoreSlot(Item itemToAdd)
    {
        GameObject NewSlot = Instantiate(StoreSlotPrefab, StoreListContent.transform);
        StoreSlot SlotInstance = NewSlot.GetComponent<StoreSlot>();
        // 
        SlotInstance.InitializeItem(itemToAdd, "Purchase", this, 0);
        StoreSlots.Add(SlotInstance);
    }

    // Adds a new Inventory Slot to the Inventory List
    void AddInventorySlot(Item itemToAdd, int stackCount)
    {
        GameObject NewSlot = Instantiate(StoreSlotPrefab, InventoryListContent.transform);
        StoreSlot SlotInstance = NewSlot.GetComponent<StoreSlot>();
        SlotInstance.InitializeItem(itemToAdd, "Sell", this, stackCount);
        InventorySlots.Add(SlotInstance);
    }

    // TO BE IMPLMENTED TO ADD ITEM TO INVENTORY AND DEDUCT MONEY FROM PLAYER
    void PerformPurchaseTransaction()
    {

    }

    // Performs the purchase of an item
    void PurchaseItem(Item itemToPurchase)
    {
        // TO BE REPLED WITH ACTUAL PLAYER MONEY
        int PlayerMoney = 1000;

        // Checks if the item is buyable
        if (!itemToPurchase.IsBuyable)
        {
            Debug.Log("Error");
            return;
        }

        // Check if player has enough money
        if (itemToPurchase.BuyPrice > PlayerMoney)
        {
            Debug.Log("Not enough money");
            return;
        }

        // Check if there is enough space in the inventory
        if (!InventoryManagerScript.CanPickupItem())
        {
            // If the item is not ammo, then there is no space
            if (itemToPurchase is not Ammo)
            {
                Debug.Log("Not enough space");
                return;
            }

            // If the item is ammo, it is possible that there is ammo of the same type in the inventory
            int slotIndex = SameAmmoSlot(itemToPurchase as Ammo);
            if (slotIndex != -1)
            {
                InventorySlots[slotIndex].UpdateAmmoStackCount((itemToPurchase as Ammo).AmountPerPack);
                InventoryManagerScript.PickUpItem(itemToPurchase);
            }
            return;
        }

        // There is enough space in the inventory for either ammo or non-ammo items
        InventoryManagerScript.PickUpItem(itemToPurchase);

        // Add the item to the inventory list if it is not ammo
        if (itemToPurchase is not Ammo)
        {
            AddInventorySlot(itemToPurchase, 0);

            // Weapon can only be bought once

            if (itemToPurchase is Weapon)
            {
                RemoveStoreItem(itemToPurchase);
            }
        }
        else if (itemToPurchase is Ammo)
        {
            // Check if there is already ammo of the same type in the inventory
            int slotIndex = SameAmmoSlot(itemToPurchase as Ammo);
            if (slotIndex == -1)
            {
                // If there is no ammo of the same type, add a new slot
                AddInventorySlot(itemToPurchase, (itemToPurchase as Ammo).AmountPerPack);
            }
            else
            {
                // If there is ammo of the same type, update the stack count
                InventorySlots[slotIndex].UpdateAmmoStackCount((itemToPurchase as Ammo).AmountPerPack);
            }
        }

    }

    // Performs the sale of an item
    void SellItem(Item itemToSell)
    {
        RemoveInventoryItem(itemToSell);
        // Deduct money from player
    }

    // Checks if there is already ammo of the same type in the inventory slots
    int SameAmmoSlot(Ammo ammoItem)
    {
        //Check if the same ammo type already exists in the inventory
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            if (InventorySlots[i] != null && InventorySlots[i].CurrentItem is Ammo && (InventorySlots[i].CurrentItem as Ammo).Weapon.Equals(ammoItem.Weapon))
            {
                return i;
            }
        }
        return -1;
    }

    // Removes a store slot from the store list
    void RemoveStoreItem(Item ItemToRemove)
    {
        StoreSlot slotToRemove = StoreSlots.Where(slot => slot.CurrentItem == ItemToRemove).FirstOrDefault();
        StoreSlots.Remove(slotToRemove);
        Destroy(slotToRemove.GetSlotInstance());
    }

    // Removes a store slot from the inventory list
    void RemoveInventoryItem(Item ItemToRemove)
    {
        StoreSlot slotToRemove = InventorySlots.Where(slot => slot.CurrentItem == ItemToRemove).FirstOrDefault();
        InventorySlots.Remove(slotToRemove);
        Destroy(slotToRemove.GetSlotInstance());
    }

    // Handles the click of a store slot using the IStoreSlotHandler interface
    public void OnClick(Item item, string actionType)
    {
        if (actionType == "Purchase")
        {
            PurchaseItem(item);
        }
        else if (actionType == "Sell")
        {
            SellItem(item);
        }
    }
}
