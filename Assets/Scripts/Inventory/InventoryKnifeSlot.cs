using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class InventoryKnifeSlot : MonoBehaviour
{
    [SerializeField]
    Knife item;

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

        // Assign the Knife image to the slot image 
        SlotImage.sprite = item.ItemImage;
        SlotImage.enabled = true;

        // Assign the Knife durability to the slot text 
        SlotText.text = item.Durability + "";
        SlotText.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the Knife durability to the slot text 
        SlotText.text = item.Durability+"";
    }

    public void TakeHit(float amount) //amount is a positive number that represents the damage bec of a hit
    {
        if(item.Durability - amount < 0)
        {
            item.Durability = 0;
        }
        else
        {
            item.Durability = item.Durability - amount;
        }
    }

    public void Repair()
    {
        item.Durability = 10;
    }
}
