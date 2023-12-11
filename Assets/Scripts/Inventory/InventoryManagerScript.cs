using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryManagerScript : MonoBehaviour
{
    Item[] Items = new Item[6];

    [SerializeField]
    GameObject[] Slots;

    int CurrentSlotIndex = 0;

    [SerializeField]
    GameObject EquippedWeapon;

    [SerializeField]
    GameObject EquippedGrenade;

    int equippedGrenadeIndex = -1;

    // To place the default items in the inventory at the beginning
    [SerializeField]
    Item DefaultItem1;

    [SerializeField]
    Item DefaultItem2;

    [SerializeField]
    Item DefaultItem3;

    //Player health display
    [SerializeField]
    GameObject HealthSlot;

    Text HealthText;

    //Player gold coins display
    [SerializeField]
    GameObject GoldSlot;

    Text GoldText;

    void Start()
    {
        PickUpItem(DefaultItem1);
        PickUpItem(DefaultItem2);
        PickUpItem(DefaultItem3);

        //Fetch the text of the health points
        GameObject healthStack = HealthSlot.transform.Find("Stack").gameObject;
        HealthText = healthStack.GetComponent<Text>();
        HealthText.enabled = true;

        //Fetch the text of the health points
        GameObject goldStack = GoldSlot.transform.Find("Stack").gameObject;
        GoldText = goldStack.GetComponent<Text>();
        GoldText.enabled = true;
    }

    void Update()
    {
        //Fetch the player's health and gold to update them
    }

    public void PickUpItem(Item newItem)
    {
        Debug.Log(CurrentSlotIndex);
        Debug.Log(newItem.ItemName);
        if (newItem is Ammo && SameAmmoSlot(newItem as Ammo) !=-1)
        {
            //Stack the new ammo to the current ammo
            InventorySlot slotScript = Slots[SameAmmoSlot(newItem as Ammo)].GetComponent<InventorySlot>();
            slotScript.StackAmmo(newItem as Ammo);
        }
        else
        {
            //Create a new instance of that item so that several instances of the same item can have different refernces
            newItem = CreateClone(newItem);

            // Assign the picked up item type to the next open index
            Items[CurrentSlotIndex] = newItem;

            // Initialize the picked up item to a new slot
            InventorySlot slotScript = Slots[CurrentSlotIndex].GetComponent<InventorySlot>();
            slotScript.InitializeItem(newItem, CurrentSlotIndex);

            // Increment index
            for (int i = CurrentSlotIndex + 1; i < Items.Length; i++)
            {
                if (Items[i] == null)
                {
                    CurrentSlotIndex = i;
                    break;
                }
                else
                {
                    CurrentSlotIndex = i + 1;
                }
            }
        }
    }

    public bool CanPickupItem()
    {
        if (CurrentSlotIndex >= 6)
        {
            Debug.Log("Can't pickup");
            return false;
            
        }

        return true;
    }

    public int SameAmmoSlot(Ammo ammoItem)
    {
        //Check if the same ammo type already exists in the inventory
        for(int i = 0; i < Items.Length; i++)
        {
            if (Items[i] != null && Items[i] is Ammo && (Items[i] as Ammo).Weapon.Equals(ammoItem.Weapon))
            {
                return i;
            }
        }
        return -1;
    }

    private Item CreateClone(Item item)
    {
        Item clone = Item.CreateInstance<Item>(); ;
        if(item is Ammo)
        {
            clone = Ammo.CreateInstance<Ammo>();
            (clone as Ammo).Weapon = (item as Ammo).Weapon;
            (clone as Ammo).AmountPerPack = (item as Ammo).AmountPerPack;
        }
        else if(item is Grenade)
        {
            clone = Grenade.CreateInstance<Grenade>();
            (clone as Grenade).Type = (item as Grenade).Type;
        }
        else if(item is Gunpowder)
        {
            clone = Gunpowder.CreateInstance<Gunpowder>();
            (clone as Gunpowder).Type = (item as Gunpowder).Type;
        }
        else if(item is Herb)
        {
            clone = Herb.CreateInstance<Herb>();
            (clone as Herb).Color = (item as Herb).Color;
            (clone as Herb).HealthPoints = (item as Herb).HealthPoints;
        }
        else if(item is Key)
        {
            clone = Key.CreateInstance<Key>();
            (clone as Key).Type = (item as Key).Type;
        }
        else if(item is Mixture)
        {
            clone = Mixture.CreateInstance<Mixture>();
            (clone as Mixture).FirstColor = (item as Mixture).FirstColor;
            (clone as Mixture).SecondColor = (item as Mixture).SecondColor;
            (clone as Mixture).HealthPoints = (item as Mixture).HealthPoints;
        }
        else if(item is Treasure)
        {
            clone = Treasure.CreateInstance<Treasure>();
            (clone as Treasure).Type = (item as Treasure).Type;
        }
        else if(item is Weapon)
        {
            clone = Weapon.CreateInstance<Weapon>();
            (clone as Weapon).IsTwoHanded = (item as Weapon).IsTwoHanded;
            (clone as Weapon).FiringMode = (item as Weapon).FiringMode;
            (clone as Weapon).Damage = (item as Weapon).Damage;
            (clone as Weapon).TimeBetweenShots = (item as Weapon).TimeBetweenShots;
            (clone as Weapon).Range = (item as Weapon).Range;
            (clone as Weapon).Capacity = (item as Weapon).Capacity;
            (clone as Weapon).AmmoLeft = (item as Weapon).AmmoLeft;
        }
        else if(item is Knife)
        {
            clone = Knife.CreateInstance<Knife>();
            (clone as Knife).Durability = (item as Knife).Durability;
        }

        clone.ItemImage = item.ItemImage;
        clone.ItemName = item.ItemName;
        clone.ItemDescription = item.ItemDescription;
        clone.IsSellable = item.IsSellable;
        clone.IsBuyable = item.IsBuyable;
        clone.SellPrice = item.SellPrice;
        clone.BuyPrice = item.BuyPrice;
        
        return clone;
    }
    public void ItemDropped(int slotIndex)
    {
        Item droppedItem = Items[slotIndex];

        if(droppedItem is Weapon)
        {
            InventoryEquippedWeaponSlot equippedScript = EquippedWeapon.GetComponent<InventoryEquippedWeaponSlot>();
            equippedScript.WeaponDropped(droppedItem as Weapon);
        }
        else if(droppedItem is Grenade)
        {
            InventoryEquippedGrenadeSlot equippedScript = EquippedGrenade.GetComponent<InventoryEquippedGrenadeSlot>();
            equippedScript.GrenadeDropped(droppedItem as Grenade);
        }

        Items[slotIndex] = null;
        if (slotIndex < CurrentSlotIndex)
        {
            CurrentSlotIndex = slotIndex;
        }
    }

    public void ItemEquipped(int slotIndex)
    {
        Item equippedItem = Items[slotIndex];

        if(equippedItem is Weapon)
        {
            InventoryEquippedWeaponSlot equippedScript = EquippedWeapon.GetComponent<InventoryEquippedWeaponSlot>();
            equippedScript.EquipWeapon(equippedItem as Weapon);
        }
        else if (equippedItem is Grenade)
        {
            InventoryEquippedGrenadeSlot equippedScript = EquippedGrenade.GetComponent<InventoryEquippedGrenadeSlot>();
            equippedScript.EquipGrenade(equippedItem as Grenade);
            equippedGrenadeIndex = slotIndex;
        }
    }

    public void ItemUsed(int slotIndex)
    {
        Item usedItem = Items[slotIndex];

        if (usedItem is Herb)
        {
            //Replace this next line with updating the player's health in the player script
            HealthText.text = (int.Parse(HealthText.text) + (usedItem as Herb).HealthPoints) + "";
        }
        else if (usedItem is Mixture)
        {
            //Replace this next line with updating the player's health in the player script
            HealthText.text = (int.Parse(HealthText.text) + (usedItem as Mixture).HealthPoints) + "";
        }

        Items[slotIndex] = null;
        if (slotIndex < CurrentSlotIndex)
        {
            CurrentSlotIndex = slotIndex;
        }
    }

    public void ReloadEquippedWeapon()
    {
        InventoryEquippedWeaponSlot equippedScript = EquippedWeapon.GetComponent<InventoryEquippedWeaponSlot>();
        string equippedWeaponType = equippedScript.GetEquippedWeaponType();
        int ammoNeeded = equippedScript.GetAmmoNeeded();

        //Check if there is ammo for the currently equipped weapon
        int ammoSlot = -1;
        for(int i = 0; i < Items.Length; i++)
        {
            if (Items[i] != null && Items[i] is Ammo && equippedWeaponType.Equals((Items[i] as Ammo).Weapon))
            {
                ammoSlot = i;
                break;
            }
        }

        if(ammoSlot != -1)
        {
            //Handle the ammo when reloading the weapon with ammo
            InventorySlot slotScript = Slots[ammoSlot].GetComponent<InventorySlot>();
            int ammoProvided = slotScript.ReloadAmmo(ammoNeeded);

            //handle weapon when reloading the weapon with ammo
            equippedScript.ReloadWeapon(ammoProvided);

        }
        else
        {
            //play a sound -> there's no ammo for this kind of weapon or there is no equipped weapon
        }
    }

    public void FireWeapon()
    {
        InventoryEquippedWeaponSlot equippedScript = EquippedWeapon.GetComponent<InventoryEquippedWeaponSlot>();
        equippedScript.Fire();
    }

    public void ThrowGrenade()
    {
        if(equippedGrenadeIndex != -1)
        {
            InventorySlot slotScript = Slots[equippedGrenadeIndex].GetComponent<InventorySlot>();
            slotScript.DiscardItem();
            equippedGrenadeIndex = -1;
        }
        else
        {
            //play a sound -> there is no equipped grenade
        }
    }
}
