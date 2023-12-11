using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    Item CurrentItem;

    Image SlotImage;


    void Start()
    {
        GameObject slotIcon = transform.Find("Icon").gameObject;
        SlotImage = slotIcon.GetComponent<Image>();
    }

    public void InitializeItem(Item newItem)
    {
        CurrentItem = newItem;

        if (newItem.ItemImage != null)
        {
            SlotImage.sprite = newItem.ItemImage;
            SlotImage.enabled = true;
        }
    }

    public List<string> GetPossibleActions()
    {
        List<string> possibleActions = new List<string>();

        return possibleActions;
    }
}
