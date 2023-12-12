using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEditor.Timeline.Actions.MenuPriority;
using UnityEngine.UI;
using UnityEditorInternal.Profiling.Memory.Experimental;

public class StoreManager : MonoBehaviour, IStoreSlotHandler
{
    [SerializeField] InventoryManagerScript InventoryManagerScript;

    [SerializeField] GameObject StoreSlotPrefab;
    [SerializeField] GameObject StoreListContent;
    [SerializeField] GameObject InventoryListContent;
    List<StoreSlot> InventorySlots;
    List<StoreSlot> StoreSlots;

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

        foreach (Item loadedobject in InventoryManagerScript.Items)
        {
            if (loadedobject != null && loadedobject.IsSellable)
            {
                AddInventorySlot(loadedobject as Item);
            }
        }
    }

    public void AddStoreSlot(Item itemToAdd)
    {
        GameObject NewSlot;
        NewSlot = Instantiate(StoreSlotPrefab, StoreListContent.transform);
        StoreSlot SlotInstance = NewSlot.GetComponent<StoreSlot>();
        SlotInstance.InitializeItem(itemToAdd, "Purchase", this);
        StoreSlots.Add(SlotInstance);
    }

    public void AddInventorySlot(Item itemToAdd)
    {
        GameObject NewSlot;
        NewSlot = Instantiate(StoreSlotPrefab, InventoryListContent.transform);
        StoreSlot SlotInstance = NewSlot.GetComponent<StoreSlot>();
        SlotInstance.InitializeItem(itemToAdd, "Sell", this);
        InventorySlots.Add(SlotInstance);
    }

    public void PurchaseItem(Item itemToPurchase)
    {
        float PlayerMoney = 1000f;

        if (!itemToPurchase.IsBuyable)
        {
            Debug.Log("Error");
            return;
        }
        else if (itemToPurchase.BuyPrice > PlayerMoney)
        {
            // Check if player has enough money
            Debug.Log("Not enough money");
            return;
        }
        else if (InventoryManagerScript.CanPickupItem())
        {
            InventoryManagerScript.PickUpItem(itemToPurchase);
            if (itemToPurchase is Ammo && SameAmmoSlot(itemToPurchase as Ammo) == -1)
            {
                AddInventorySlot(itemToPurchase);
            }
            else if (itemToPurchase is not Ammo)
            {
                AddInventorySlot(itemToPurchase);

            }
            if (itemToPurchase is Weapon)
            {
                RemoveStoreItem(itemToPurchase);
            }
        }
        else if (itemToPurchase is Ammo && InventoryManagerScript.SameAmmoSlot(itemToPurchase as Ammo) != -1)
        {
            InventoryManagerScript.PickUpItem(itemToPurchase);
        }
        else
        {
            Debug.Log("Not enough space");
            return;
        }
    }

    public int SameAmmoSlot(Ammo ammoItem)
    {
        //Check if the same ammo type already exists in the inventory
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            if (InventorySlots[i] != null && InventorySlots[i].GetItem() is Ammo && (InventorySlots[i].GetItem() as Ammo).Weapon.Equals(ammoItem.Weapon))
            {
                return i;
            }
        }
        return -1;
    }

    public void RemoveStoreItem(Item ItemToRemove)
    {
        StoreSlot slotToRemove = StoreSlots.Where(slot => slot.GetItem() == ItemToRemove).FirstOrDefault();
        StoreSlots.Remove(slotToRemove);
        Destroy(slotToRemove.GetSlotInstance());
    }

    public void RemoveInventoryItem(Item ItemToRemove)
    {
        StoreSlot slotToRemove = InventorySlots.Where(slot => slot.GetItem() == ItemToRemove).FirstOrDefault();
        InventorySlots.Remove(slotToRemove);
        Destroy(slotToRemove.GetSlotInstance());
    }

    public void OnClick(Item item, string actionType)
    {
        if (actionType == "Purchase")
        {
            PurchaseItem(item);
        }
        else if (actionType == "Sell")
        {

            RemoveInventoryItem(item);
            if (item is Weapon)
            {
                AddStoreSlot(item);
            }
        }
    }
}
