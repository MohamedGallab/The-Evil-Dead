using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class InventoryKnifeSlot : MonoBehaviour
{
    [SerializeField]
    Knife item;

    public Knife KnifeItem { get; private set; }

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

        //Ceate an instance of the scriptable object
        GameObject InventoryManager = GameObject.Find("InventoryManager");
        InventoryManagerScript InventoryScript = InventoryManager.GetComponent<InventoryManagerScript>();
        KnifeItem = (Knife) InventoryScript.CreateClone(item);

        // Assign the Knife image to the slot image 
        SlotImage.sprite = KnifeItem.ItemImage;
        SlotImage.enabled = true;

        // Assign the Knife durability to the slot text 
        SlotText.text = KnifeItem.Durability + "";
        SlotText.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        // Update the Knife durability to the slot text 
        SlotText.text = KnifeItem.Durability+"";
    }

    public void TakeHit(float amount) //amount is a positive number that represents the damage bec of a hit
    {
        if(KnifeItem.Durability - amount < 0)
        {
            KnifeItem.Durability = 0;
        }
        else
        {
            KnifeItem.Durability = KnifeItem.Durability - amount;
        }
    }

    public void Repair()
    {
        KnifeItem.Durability = 10;
    }
}
