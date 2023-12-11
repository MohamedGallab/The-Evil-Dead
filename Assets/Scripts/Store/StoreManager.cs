using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEditor.Timeline.Actions.MenuPriority;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    [SerializeField] InventoryManagerScript InventoryManagerScript;

    [SerializeField] GameObject StoreSlotPrefab;
    [SerializeField] GameObject StoreListContent;
    [SerializeField] GameObject InventoryListContent;
    List<StoreSlot> StoreSlots;

    void Start()
    {
        StoreSlots = new List<StoreSlot>();
        string[] storeItemsPaths = new string[] { "Weapons/AssaultRifle", "Weapons/Shotgun", "Ammo", "Grenades", "Gunpowder", "Herbs" };
        
        foreach (string path in storeItemsPaths)
        {
            var loadedobjects = Resources.LoadAll(path);
            foreach (Item loadedobject in loadedobjects)
            {
                AddItem(loadedobject as Item, "store");
            }
        }
    }

    public void AddItem(Item ItemToAdd, string type)
    {
        GameObject NewSlot = Instantiate(StoreSlotPrefab, StoreListContent.transform);
        StoreSlot SlotInstance = NewSlot.GetComponent<StoreSlot>();
        SlotInstance.InitializeItem(ItemToAdd,"blah");
        Button button = SlotInstance.GetComponent<Button>();
        if (type == "store")
        {
            button.onClick.AddListener(() => PurchaseItem(ItemToAdd));
        }
        else if (type == "inventory")
        {
            //button.onClick.AddListener(() => RemoveItem(ItemToAdd));
            Debug.Log("Inventory button");
        }
        StoreSlots.Add(SlotInstance);
    }

    public void PurchaseItem(Item ItemToPurchase)
    {
        if (InventoryManagerScript.CanPickupItem())
        {
            // Add item to inventory
            // InventoryManagerScript.PickUpItem(ItemToPurchase);

            RemoveItem(ItemToPurchase);
        }
    }

    public void RemoveItem(Item ItemToRemove)
    {
        StoreSlot slotToRemove = StoreSlots.Where(slot => slot.GetItem() == ItemToRemove).FirstOrDefault();
        StoreSlots.Remove(slotToRemove);
        Destroy(slotToRemove.GetSlotInstance());
    }
}
