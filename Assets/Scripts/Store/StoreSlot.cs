using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;

public class StoreSlot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ItemNameDisplay;
    [SerializeField] Image ItemImageDisplay;
    [SerializeField] TextMeshProUGUI ItemPriceText;
    [SerializeField] TextMeshProUGUI ItemDescriptionText;
    [SerializeField] GameObject AmmoGroup;
    [SerializeField] TextMeshProUGUI AmmoCountText;

    IStoreSlotHandler slotHandler;
    Item CurrentItem;
    string actionType;

    public void InitializeItem(InventorySlot newSlot, string type, IStoreSlotHandler handle)
    {
        CurrentItem = newSlot.CurrentItem;
        actionType = type;
        slotHandler = handle;
        ItemNameDisplay.SetText(CurrentItem.ItemName);
        ItemDescriptionText.SetText(CurrentItem.ItemDescription);

        if (CurrentItem.ItemImage != null)
        {
            ItemImageDisplay.sprite = CurrentItem.ItemImage;
            ItemImageDisplay.enabled = true;
        }

        Button button = GetComponent<Button>();
        button.onClick.AddListener(HandleButtonClick);
        if (actionType == "Purchase")
        {
            ItemPriceText.SetText(CurrentItem.BuyPrice.ToString());
        }
        else if (actionType == "Sell")
        {
            ItemPriceText.SetText(CurrentItem.SellPrice.ToString());
            if (!CurrentItem.IsSellable)
            {
                button.interactable = false;
            }
            if (CurrentItem is Ammo)
            {
                AmmoCountText.SetText(newSlot.StackCount.ToString());
            }
            else
            {
                AmmoCountText.enabled = false;
            }
        }
    }

    public void InitializeItem(Item newItem, string type, IStoreSlotHandler handle)
    {
        CurrentItem = newItem;
        actionType = type;
        slotHandler = handle;
        ItemNameDisplay.SetText(CurrentItem.ItemName);
        ItemDescriptionText.SetText(CurrentItem.ItemDescription);

        if (CurrentItem.ItemImage != null)
        {
            ItemImageDisplay.sprite = CurrentItem.ItemImage;
            ItemImageDisplay.enabled = true;
        }

        Button button = GetComponent<Button>();
        button.onClick.AddListener(HandleButtonClick);
        if (actionType == "Purchase")
        {
            ItemPriceText.SetText(CurrentItem.BuyPrice.ToString());
        }
        else if (actionType == "Sell")
        {
            ItemPriceText.SetText(CurrentItem.SellPrice.ToString());
            if (!CurrentItem.IsSellable)
            {
                button.interactable = false;
            }
            if (CurrentItem is Ammo)
            {
                AmmoCountText.SetText((CurrentItem as Ammo).stal.ToString());
            }
            else
            {
                AmmoCountText.enabled = false;
            }
        }
    }

    void HandleButtonClick()
    {
        if (slotHandler != null)
        {
            slotHandler.OnClick(CurrentItem, actionType);
        }
    }

    public GameObject GetSlotInstance() { return gameObject; }

    public Item GetItem() { return CurrentItem; }

    public void OnRectTransformRemoved() { Destroy(gameObject); }
}
