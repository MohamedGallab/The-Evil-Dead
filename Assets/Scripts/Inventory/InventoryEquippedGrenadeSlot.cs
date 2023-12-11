using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryEquippedGrenadeSlot : MonoBehaviour
{
    Grenade EquippedGrenade;

    Image SlotImage;

    Text SlotText;

    // Start is called before the first frame update
    void Start()
    {
        // Get the slot image component
        GameObject slotIcon = transform.Find("Icon").gameObject;
        SlotImage = slotIcon.GetComponent<Image>();

    }

    
    public void EquipGrenade(Grenade newGrenade)
    {
        EquippedGrenade = newGrenade;

        if (EquippedGrenade.ItemImage != null)
        {
            // Initialize the grenade's image in the slot's image
            SlotImage.sprite = EquippedGrenade.ItemImage;
            SlotImage.enabled = true;
        }
    }

    public void GrenadeDropped(Grenade droppedGrenade)
    {
        if (EquippedGrenade != null)
        {
            if (droppedGrenade.ItemName.Equals(EquippedGrenade.ItemName))
            {
                EquippedGrenade = null;

                SlotImage.sprite = null;
                SlotImage.enabled = false;
            }
        }
    }
}
