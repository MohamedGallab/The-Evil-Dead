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
    public Item CurrentItem { get; private set; }
    string actionType;
    public int ammoStackCount { get;  set; }

    public void InitializeItem(Item newItem, string type, IStoreSlotHandler handle, int stackCount)
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
            if (newItem is Ammo)
            {
                ammoStackCount = stackCount;
                AmmoCountText.SetText(stackCount.ToString());
                AmmoCountText.enabled = true;
                AmmoGroup.SetActive(true);
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

    public void UpdateAmmoStackCount(int newStackCount)
    {
        ammoStackCount += newStackCount;
        AmmoCountText.SetText(ammoStackCount.ToString());
    }
    public GameObject GetSlotInstance() { return gameObject; }

    public void OnRectTransformRemoved() { Destroy(gameObject); }
}
