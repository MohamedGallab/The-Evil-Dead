using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class InventoryEquippedWeaponSlot : MonoBehaviour
{
    Weapon EquippedWeapon;

    Image SlotImage;

    Text SlotText;

    // Start is called before the first frame update
    void Start()
    {
        // Get the slot image component
        GameObject slotIcon = transform.Find("Icon").gameObject;
        SlotImage = slotIcon.GetComponent<Image>();

        // Get the slot text component
        GameObject slotStack = transform.Find("Stack").gameObject;
        SlotText = slotStack.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EquippedWeapon != null)
        {
            // Keep updating the ammo left as long as the weapon is still equipped
            SlotText.text = EquippedWeapon.AmmoLeft + "";
        }
        else
        {
            SlotText.enabled = false;
        }
    }
    public void EquipWeapon(Weapon newWeapon)
    {
        EquippedWeapon = newWeapon;

        if (EquippedWeapon.ItemImage != null)
        {
            // Initialize the weapon's image in the slot's image
            SlotImage.sprite = EquippedWeapon.ItemImage;
            SlotImage.enabled = true;

            // Initialize the weapon's ammo left in the slot's text
            SlotText.text = EquippedWeapon.AmmoLeft+"";
            SlotText.enabled = true;
        }
    }

    public void WeaponDropped(Weapon droppedWeapon)
    {
        if (EquippedWeapon != null)
        {
            if(droppedWeapon.ItemName.Equals(EquippedWeapon.ItemName))
            {
                EquippedWeapon = null;

                SlotImage.sprite = null;
                SlotImage.enabled = false;

                SlotText.text = null;
                SlotText.enabled = false;
            }
        }
    }

    public void Fire()
    {
        if(EquippedWeapon != null && EquippedWeapon.AmmoLeft > 0)
        {
            EquippedWeapon.AmmoLeft = EquippedWeapon.AmmoLeft - 1;
            //play firing sound
        }
        else
        {
            //play empty mag sound
        }
    }

    public void ReloadWeapon(int ammoProvided) //handle weapon when reloading weapon with ammo
    {
        EquippedWeapon.AmmoLeft += ammoProvided;
    }

    public string GetEquippedWeaponType()
    {
        if(EquippedWeapon != null)
        {
            return EquippedWeapon.ItemName;
        }
        else
        {
            return "None";
        }
    }
    public int GetAmmoNeeded()
    {
        if (EquippedWeapon != null)
        {
            return EquippedWeapon.Capacity - EquippedWeapon.AmmoLeft;
        }
        else
        {
            return -1;
        }
    }
}
