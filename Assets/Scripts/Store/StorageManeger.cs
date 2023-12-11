using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StorageManeger : MonoBehaviour, IStoreSlotHandler
{
    [SerializeField] InventoryManagerScript InventoryManagerScript;

    [SerializeField] GameObject StoreSlotPrefab;
    [SerializeField] GameObject StoreListContent;
    [SerializeField] GameObject InventoryListContent;
    List<StoreSlot> InventorySlots;
    List<StoreSlot> StorageSlots;

    void Start()
    {
        StorageSlots = new List<StoreSlot>();
        foreach (Item item in InventoryManagerScript.Items)
        {
            GameObject NewSlot = Instantiate(StoreSlotPrefab, StoreListContent.transform);
            StoreSlot SlotInstance = NewSlot.GetComponent<StoreSlot>();
            SlotInstance.InitializeItem(item, "Store");
            InventorySlots.Add(SlotInstance);
        }
    }

    public void OnClick(Item item, string actionType)
    {
        if (actionType == "Store")
        {
            StoreItem(item);
            Debug.Log("Store item");
        }
        else if (actionType == "Unstore")
        {
            // Unstore item
            UnstoreItem(item);
            Debug.Log("Unstore item");

        }
    }

    void StoreItem(Item ItemToStore)
    {
        GameObject NewSlot = Instantiate(StoreSlotPrefab, StoreListContent.transform);
        StoreSlot SlotInstance = NewSlot.GetComponent<StoreSlot>();
        SlotInstance.InitializeItem(ItemToStore, "Unstore");
        StorageSlots.Add(SlotInstance);
        // Remove item from inventory
    }

    void UnstoreItem(Item ItemToUnStore)
    {
        GameObject NewSlot = Instantiate(StoreSlotPrefab, InventoryListContent.transform);
        StoreSlot SlotInstance = NewSlot.GetComponent<StoreSlot>();
        SlotInstance.InitializeItem(ItemToUnStore, "Store");
        Button button = SlotInstance.GetComponent<Button>();
        button.onClick.AddListener(() => StoreItem(ItemToUnStore));
        RemoveItem(ItemToUnStore);
        // Add item to inventory
    }


    void RemoveItem(Item ItemToRemove)
    {
        StoreSlot slotToRemove = StorageSlots.Where(slot => slot.GetItem() == ItemToRemove).FirstOrDefault();
        StorageSlots.Remove(slotToRemove);
        Destroy(slotToRemove.GetSlotInstance());
    }

}
