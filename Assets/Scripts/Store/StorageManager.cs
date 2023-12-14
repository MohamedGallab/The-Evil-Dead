using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class StorageManager : MonoBehaviour, IStoreSlotHandler
{
    [SerializeField] InventoryManagerScript InventoryManagerScript;

    [SerializeField] GameObject StoreSlotPrefab;
    [SerializeField] GameObject StorageListContent;
    [SerializeField] GameObject InventoryListContent;
    List<StoreSlot> InventorySlots;
    List<StoreSlot> StorageSlots;


    [SerializeField] Item ammo;

    void Awake()
    {
        StorageSlots = new List<StoreSlot>();
        InventorySlots = new List<StoreSlot>();
        AddStoreSlot(ammo, 12);
    }

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
    }

    void AddStoreSlot(Item itemToAdd, int stackCount)
    {
        if (itemToAdd is Ammo && SameAmmoSlot(itemToAdd as Ammo) != -1)
        {
            StorageSlots[SameAmmoSlot(itemToAdd as Ammo)].StackAmmo((itemToAdd as Ammo));
            return;
        }
        GameObject NewSlot = Instantiate(StoreSlotPrefab, StorageListContent.transform);
        StoreSlot SlotInstance = NewSlot.GetComponent<StoreSlot>();
        SlotInstance.InitializeItem(itemToAdd, "Unstore", this, stackCount, -1);
        StorageSlots.Add(SlotInstance);
    }
    public int SameAmmoSlot(Ammo ammoItem)
    {
        for (int i = 0; i < StorageSlots.Count; i++)
        {
            if (StorageSlots[i] != null && StorageSlots[i].CurrentItem is Ammo && (StorageSlots[i].CurrentItem as Ammo).Weapon.Equals(ammoItem.Weapon))
            {
                return i;
            }
        }
        return -1;
    }

    // Adds a new Inventory Slot to the Inventory List
    void AddInventorySlot(InventorySlot newInventorySlot, int indexInInventory)
    {
        GameObject NewSlot = Instantiate(StoreSlotPrefab, InventoryListContent.transform);
        StoreSlot SlotInstance = NewSlot.GetComponent<StoreSlot>();
        SlotInstance.InitializeItem(newInventorySlot.CurrentItem, "Store", this, newInventorySlot.StackCount, indexInInventory);
        InventorySlots.Add(SlotInstance);
    }

    public void AddToInventory(Item itemToUnstore, int stackCount)
    {
        if (InventoryManagerScript.CanPickupItem(itemToUnstore))
        {
            InventoryManagerScript.PickUpItem(itemToUnstore);
            if (itemToUnstore is Ammo)
            {
                int index = InventoryManagerScript.SameAmmoSlot(itemToUnstore as Ammo);
                if (index != -1)
                {
                    InventoryManagerScript.Slots[index].GetComponent<InventorySlot>().StackCount = stackCount;
                }
            }
            RemoveItem(itemToUnstore);
            UpdateInventorySlots();
        }
    }

    public void RemoveFromInventory(Item itemToStore, int indexInInventory)
    {
        AddStoreSlot(itemToStore, InventoryManagerScript.Slots[indexInInventory].GetComponent<InventorySlot>().StackCount);
        InventoryManagerScript.Slots[indexInInventory].GetComponent<InventorySlot>().DiscardItem();
        UpdateInventorySlots();
    }

    void RemoveItem(Item ItemToRemove)
    {
        StoreSlot slotToRemove = StorageSlots.Where(slot => slot.CurrentItem == ItemToRemove).FirstOrDefault();
        StorageSlots.Remove(slotToRemove);
        Destroy(slotToRemove.GetSlotInstance());
    }
}
