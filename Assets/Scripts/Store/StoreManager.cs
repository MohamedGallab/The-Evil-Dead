using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StoreManager : MonoBehaviour, IStoreSlotHandler
{
    [SerializeField] InventoryManagerScript InventoryManagerScript;
    [SerializeField] GameObject StoreSlotPrefab;
    [SerializeField] GameObject StoreListContent;
    [SerializeField] GameObject InventoryListContent;
    [SerializeField] TextMeshProUGUI GoldText;
    [SerializeField] bool IsShowMenu = false;
    [SerializeField] GameObject _storeCanvas;
    List<StoreSlot> InventorySlots;
    List<StoreSlot> StoreSlots;

    public void ShowMenu()
    {
        if (IsShowMenu)
        {
            UpdateInventorySlots();
            _storeCanvas.SetActive(true);
        }
        else
        {
            _storeCanvas.SetActive(false);
        }
    }

    // Handles the initialization of the store list
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
    }

    // Updates the inventory list
    public void UpdateInventorySlots()
    {
        foreach (StoreSlot loadedobject in InventorySlots)
        {
            Destroy(loadedobject.GetSlotInstance());
        }

        InventorySlots.Clear();

        foreach (GameObject loadedobject in InventoryManagerScript.Slots)
        {
            InventorySlot inventorySlot = loadedobject.GetComponent<InventorySlot>();
            if (inventorySlot.CurrentItem != null)
            {
                AddInventorySlot(inventorySlot, inventorySlot.IndexInInventory);
            }
        }
        GoldText.SetText(InventoryManagerScript.Gold.ToString());
    }

    // Adds a new Store Slot to the Store List
    void AddStoreSlot(Item itemToAdd)
    {
        GameObject NewSlot = Instantiate(StoreSlotPrefab, StoreListContent.transform);
        StoreSlot SlotInstance = NewSlot.GetComponent<StoreSlot>();
        // 
        SlotInstance.InitializeItem(itemToAdd, "Purchase", this, 0, -1);
        StoreSlots.Add(SlotInstance);
    }

    // Adds a new Inventory Slot to the Inventory List
    void AddInventorySlot(InventorySlot newInventorySlot, int indexInInventory)
    {
        GameObject NewSlot = Instantiate(StoreSlotPrefab, InventoryListContent.transform);
        StoreSlot SlotInstance = NewSlot.GetComponent<StoreSlot>();
        SlotInstance.InitializeItem(newInventorySlot.CurrentItem, "Sell", this, newInventorySlot.StackCount, indexInInventory);
        InventorySlots.Add(SlotInstance);
    }

    // Performs the purchase of an item
    public void AddToInventory(Item itemToPurchase, int stackCount)
    {
        // Checks if the item is buyable
        if (!itemToPurchase.IsBuyable)
        {
            Debug.Log("Error");
            return;
        }

        // Check if player has enough money
        if (itemToPurchase.BuyPrice > InventoryManagerScript.Gold)
        {
            Debug.Log("Not enough money");
            return;
        }

        if (!InventoryManagerScript.CanPickupItem(itemToPurchase))
        {
            Debug.Log("Not enough space");
        }
        else
        {
            InventoryManagerScript.PickUpItem(itemToPurchase);
            InventoryManagerScript.UpdateGold(-itemToPurchase.BuyPrice);
            UpdateInventorySlots();
            if (itemToPurchase is Weapon)
            {
                RemoveStoreItem(itemToPurchase);
            }
        }

        //// Check if there is enough space in the inventory
        //if (!InventoryManagerScript.CanPickupItem())
        //{
        //    // If the item is not ammo, then there is no space
        //    if (itemToPurchase is not Ammo)
        //    {
        //        Debug.Log("Not enough space");
        //        return;
        //    }

        //    // If the item is ammo, it is possible that there is ammo of the same type in the inventory
        //    int slotIndex = SameAmmoSlot(itemToPurchase as Ammo);
        //    if (slotIndex != -1)
        //    {
        //        InventorySlots[slotIndex].UpdateAmmoStackCount((itemToPurchase as Ammo).AmountPerPack);
        //        InventoryManagerScript.PickUpItem(itemToPurchase);
        //    }
        //    return;
        //}

        //// There is enough space in the inventory for either ammo or non-ammo items
        //InventoryManagerScript.PickUpItem(itemToPurchase);

        //// Add the item to the inventory list if it is not ammo
        //if (itemToPurchase is not Ammo)
        //{
        //    AddInventorySlot(itemToPurchase, 0);

        //    // Weapon can only be bought once

        //    if (itemToPurchase is Weapon)
        //    {
        //        RemoveStoreItem(itemToPurchase);
        //    }
        //}
        //else if (itemToPurchase is Ammo)
        //{
        //    // Check if there is already ammo of the same type in the inventory
        //    int slotIndex = SameAmmoSlot(itemToPurchase as Ammo);
        //    if (slotIndex == -1)
        //    {
        //        // If there is no ammo of the same type, add a new slot
        //        AddInventorySlot(itemToPurchase, (itemToPurchase as Ammo).AmountPerPack);
        //    }
        //    else
        //    {
        //        // If there is ammo of the same type, update the stack count
        //        InventorySlots[slotIndex].UpdateAmmoStackCount((itemToPurchase as Ammo).AmountPerPack);
        //    }
        //}

    }

    // Performs the selling of an item
    public void RemoveFromInventory(Item itemToSell, int indexInInventory)
    {
        InventoryManagerScript.Slots[indexInInventory].GetComponent<InventorySlot>().DiscardItem();
        InventoryManagerScript.UpdateGold(itemToSell.SellPrice);
        UpdateInventorySlots();
    }

    // Removes a store slot from the store list
    void RemoveStoreItem(Item ItemToRemove)
    {
        StoreSlot slotToRemove = StoreSlots.Where(slot => slot.CurrentItem == ItemToRemove).FirstOrDefault();
        StoreSlots.Remove(slotToRemove);
        Destroy(slotToRemove.GetSlotInstance());
    }

}
