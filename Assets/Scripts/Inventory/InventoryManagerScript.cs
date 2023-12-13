using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine.UI;
using UnityEngine;

public class InventoryManagerScript : MonoBehaviour
{
    static Item[] _items = new Item[6];
    public Item[] Items
    {
        get { return _items; }

    }
    [SerializeField]
    public GameObject[] Slots;

    int CurrentSlotIndex = 0;

    [SerializeField]
    GameObject EquippedWeapon;

    [SerializeField]
    GameObject EquippedGrenade;

    int EquippedGrenadeIndex = -1;

    [SerializeField]
    GameObject KnifeSlot;


    int FirstCombineItemIndex = -1;

    // To place the default items in the inventory at the beginning
    [SerializeField]
    Item DefaultItem1;

    [SerializeField]
    Item DefaultItem2;

    [SerializeField]
    Item DefaultItem3;

    //Player health display
    [SerializeField]
    GameObject HealthFill;
    Slider HealthBar;
    [SerializeField]
    GameObject HealthTextObj;
    Text HealthText;

    //Player gold coins display
    [SerializeField]
    GameObject GoldTextObj;
    Text GoldText;

    //Results of crafting
    [SerializeField]
    Mixture RedRedMixture;
    [SerializeField]
    Mixture GreenGreenMixture;
    [SerializeField]
    Mixture GreenRedMixture;

    [SerializeField]
    Ammo NormalNormalAmmo;
    [SerializeField]
    Ammo HighGradeHighGradeAmmo;
    [SerializeField]
    Ammo NormalHighGradeAmmo;

    //Initialize gold
    float Gold = 30;

    void Start()
    {
        PickUpItem(DefaultItem1);
        PickUpItem(DefaultItem2);
        PickUpItem(DefaultItem3);

        //Fetch the text and bar of the health points
        HealthText = HealthTextObj.GetComponent<Text>();
        HealthText.text = "8"; 
        HealthText.enabled = true;

        HealthBar = HealthFill.GetComponent<Slider>();
        HealthBar.maxValue = 8;
        HealthBar.value = 8;

        //Fetch the text of the health points
        GoldText = GoldTextObj.GetComponent<Text>();
        GoldText.text = Gold+"";
        GoldText.enabled = true;
        StorageManager storageManager = FindObjectOfType<StorageManager>();
        if (storageManager != null)
        {
            storageManager.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        //Update player's gold
        GoldText.text = Gold + "";

        //Fetch the player's health to update the health text and the health bar
        
    }

    public void PickUpItem(Item newItem)
    {
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
            for (int i = CurrentSlotIndex ; i < Items.Length; i++)
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

    public bool CanPickupItem(Item newItem)
    {
        if (CurrentSlotIndex > 5)
        {
            // Check if the item being picked up is ammo that can be stacked
            if(newItem is Ammo && SameAmmoSlot(newItem as Ammo) != -1)
            {
                return true;
            }
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

    public Item CreateClone(Item item)
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
            EquippedGrenadeIndex = slotIndex;
        }
    }

    public void ItemUsed(int slotIndex)
    {
        Item usedItem = Items[slotIndex];

        if (usedItem is Herb)
        {
            //Replace this next line with updating the player's health in the player script
            HealthText.text = (int.Parse(HealthText.text) + (usedItem as Herb).HealthPoints) + "";
            HealthBar.value = int.Parse(HealthText.text);
        }
        else if (usedItem is Mixture)
        {
            //Replace this next line with updating the player's health in the player script
            HealthText.text = (int.Parse(HealthText.text) + (usedItem as Mixture).HealthPoints) + "";
            HealthBar.value = int.Parse(HealthText.text);
        }

        Items[slotIndex] = null;
        if (slotIndex < CurrentSlotIndex)
        {
            CurrentSlotIndex = slotIndex;
        }
    }

    public void SetFirstCombineItem(int slotIndex)
    {
        FirstCombineItemIndex = slotIndex;
    }

    public int isCombining() //to know if the state of the game is between the 2 clicks of combining 2 items
    {
        return FirstCombineItemIndex;
    }

    public Item GetFirstCombineItem()
    {
        if(FirstCombineItemIndex != -1)
        {
            return Items[FirstCombineItemIndex];
        }
        return null;
    }

    public void PerformCrafting(int secondCombineItemIndex)
    {
        Item firstItem = Items[FirstCombineItemIndex];
        Item secondItem = Items[secondCombineItemIndex];

        //Discard the 1st item
        InventorySlot slotScript = Slots[FirstCombineItemIndex].GetComponent<InventorySlot>();
        slotScript.DiscardItem();

        //Continue discarding the 2nd item
        Items[secondCombineItemIndex] = null;
        if (secondCombineItemIndex < CurrentSlotIndex)
        {
            CurrentSlotIndex = secondCombineItemIndex;
        }

        //Combination
        if((firstItem is Herb) && (secondItem is Herb))
        {
            if((firstItem as Herb).Color.Equals("Green") && (secondItem as Herb).Color.Equals("Green"))
            {
                PickUpItem(GreenGreenMixture);
            }
            else if ((firstItem as Herb).Color.Equals("Red") && (secondItem as Herb).Color.Equals("Red"))
            {
                PickUpItem(GreenRedMixture);
            }
            else //First is red and second is green or first is green and second is red
            {
                PickUpItem(RedRedMixture);
            }
        }
        else if ((firstItem is Gunpowder) && (secondItem is Gunpowder))
        {
            if ((firstItem as Gunpowder).Type.Equals("Normal") && (secondItem as Gunpowder).Type.Equals("Normal"))
            {
                PickUpItem(NormalNormalAmmo);
            }
            else if ((firstItem as Gunpowder).Type.Equals("HighGrade") && (secondItem as Gunpowder).Type.Equals("HighGrade"))
            {
                PickUpItem(HighGradeHighGradeAmmo);
            }
            else //First is Normal and second is HighGrade or first is HighGrade and second is Normal
            {
                PickUpItem(NormalHighGradeAmmo);
            }
        }


        //reset the state of combination/crafting
        FirstCombineItemIndex = -1;
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
        if(EquippedGrenadeIndex != -1)
        {
            InventorySlot slotScript = Slots[EquippedGrenadeIndex].GetComponent<InventorySlot>();
            slotScript.DiscardItem();
            EquippedGrenadeIndex = -1;
        }
        else
        {
            //play a sound -> there is no equipped grenade
        }
    }

    public void KnifeTakesHit(int amount)
    {
        InventoryKnifeSlot knifeScript = KnifeSlot.GetComponent<InventoryKnifeSlot>();
        knifeScript.TakeHit(amount);
    }

    public void RepaireKnife()
    {
        InventoryKnifeSlot knifeScript = KnifeSlot.GetComponent<InventoryKnifeSlot>();
        knifeScript.Repair();
    }

    //For the store
    public Item[] GetItems()
    {
        return Items;
    }

}
