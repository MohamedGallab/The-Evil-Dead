using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;
using System.Linq;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    Item CurrentItem;

    int IndexInInventory;

    Image SlotImage;

    Text SlotText;

    // If the slot contains ammo, this field stores the total amount of ammo from different instances
    int StackCount = 0;

    TMP_Dropdown Dropdown;

    InventoryManagerScript InventoryScript;

    //Timer to update the contents of the slot's dropdown every 1 second
    private float _timer = 0f;


    void Awake()
    {
        // Get the slot image component
        GameObject slotIcon = transform.Find("Icon").gameObject;
        SlotImage = slotIcon.GetComponent<Image>();

        // Get the slot text component
        GameObject slotStack = transform.Find("Stack").gameObject;
        SlotText = slotStack.GetComponent<Text>();

        // Get the dropdown component
        Dropdown = GetComponentInChildren<TMP_Dropdown>();
        Dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged();
        });

        //Get the invenotry manager script to notify it of any changes
        GameObject InventoryManager = GameObject.Find("InventoryManager");
        InventoryScript = InventoryManager.GetComponent<InventoryManagerScript>();


        Debug.Log("In starttt");

    }

    private void Update()
    {
        _timer += Time.deltaTime;


        if (CurrentItem != null)
        {
            if (CurrentItem is Weapon)
            {
                SlotText.text = (CurrentItem as Weapon).AmmoLeft + "";
                SlotText.enabled = true;
            }
            else if (CurrentItem is Ammo)
            {
                SlotText.text = StackCount + "";
                SlotText.enabled = true;
            }
            else
            {
                SlotText.enabled = false;
            }

            if (_timer >= 1f)
            {
                //Clear Current values in dropdown
                Dropdown.ClearOptions();

                //Initialize the new options in dropdown
                List<TMP_Dropdown.OptionData> possibleActions = GetPossibleActions();
                Dropdown.AddOptions(possibleActions);
               
                Dropdown.RefreshShownValue();

                _timer = 0f;
            }
        }
    }
    public void InitializeItem(Item newItem, int indexInInventory)
    {
        CurrentItem = newItem;
        IndexInInventory = indexInInventory;

        //Initialize the item's image in the slot image
        if (CurrentItem.ItemImage != null)
        {
            SlotImage.sprite = CurrentItem.ItemImage;
            SlotImage.enabled = true;
        }

        //Initialize the item's ammo left (in case of weapon) or total ammo (in case of ammo) in the slot text
        if (CurrentItem is Weapon)
        {
            SlotText.text = (CurrentItem as Weapon).AmmoLeft + "";
            SlotText.enabled = true;
        }
        else if (CurrentItem is Ammo)
        {
            StackCount = (CurrentItem as Ammo).AmountPerPack;
            SlotText.text = StackCount+"";
            SlotText.enabled = true;
        }
        else
        {
            SlotText.enabled = false;
        }
    }

    public void StackAmmo(Ammo ammoItem)
    {
        StackCount += ammoItem.AmountPerPack;
    }

    public List<TMP_Dropdown.OptionData> GetPossibleActions()
    {
        List<TMP_Dropdown.OptionData> possibleActions = new List<TMP_Dropdown.OptionData>();

        possibleActions.Add(new TMP_Dropdown.OptionData(""));

        if (CurrentItem is Weapon || CurrentItem is Grenade)
        {
            possibleActions.Add(new TMP_Dropdown.OptionData("Equip"));
        }
        if(CurrentItem is Herb || CurrentItem is Mixture)
        {
            possibleActions.Add(new TMP_Dropdown.OptionData("Use"));
        }
        if (CurrentItem is Herb || CurrentItem is Gunpowder)
        {
            possibleActions.Add(new TMP_Dropdown.OptionData("Combine"));
        }

        if ((CurrentItem is not Weapon) && (CurrentItem is not Key))
        {
            possibleActions.Add(new TMP_Dropdown.OptionData("Discard"));
        }
        return possibleActions;
    }


    public void DropdownValueChanged()
    {
        string selectedOption = Dropdown.options[Dropdown.value].text;

        if(selectedOption.Equals("Equip"))
        {
            Equip();
        }
        else if(selectedOption.Equals("Use"))
        {
            Use();
        }
        else if(selectedOption.Equals("Combine"))
        {

        }
        else if(selectedOption.Equals("Discard"))
        {
            DiscardItem();
        }
    }


    public void DiscardItem()
    {
        //Notify inventory
        InventoryScript.ItemDropped(IndexInInventory);

        //Handle the slot's appearance
        CurrentItem = null;

        SlotImage.sprite = null;
        SlotImage.enabled = false;

        SlotText.text = null;
        SlotText.enabled = false;

        StackCount = 0;
    }

    public void Equip()
    {
        //Notify inventory
        InventoryScript.ItemEquipped(IndexInInventory);
    }

    public void Combine()
    {

    }

    public void Use()
    {
        //Notify inventory
        InventoryScript.ItemUsed(IndexInInventory);

        //Handle the slot's appearance
        CurrentItem = null;

        SlotImage.sprite = null;
        SlotImage.enabled = false;

        SlotText.text = null;
        SlotText.enabled = false;

        StackCount = 0;
    }

    public int ReloadAmmo(int ammoNeeded) //handle ammo when reloading a weapon with ammo
    {
        if(StackCount > ammoNeeded)
        {
            StackCount -= ammoNeeded;
            return ammoNeeded;
        }
        else
        {
            int tmp = StackCount;
            StackCount = 0;
            DiscardItem();
            return tmp;
        }
    }

}
