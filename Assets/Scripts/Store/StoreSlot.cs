using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class StoreSlot : MonoBehaviour
{
    [SerializeField] StoreManager StoreManager;
    [SerializeField] TextMeshProUGUI ItemNameDisplay;
    [SerializeField] Image ItemImageDisplay;

    IStoreSlotHandler slotHandler;
    Item CurrentItem;
    string actionType;

    public void InitializeItem(Item newItem, string type)
    {
        CurrentItem = newItem;
        actionType = type;
        ItemNameDisplay.text = CurrentItem.ItemName;

        if (CurrentItem.ItemImage != null)
        {
            ItemImageDisplay.sprite = CurrentItem.ItemImage;
            ItemImageDisplay.enabled = true;
        }

        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => HandleButtonClick(newItem));
    }

    void HandleButtonClick(Item item)
    {
        if (slotHandler != null)
        {
            slotHandler.OnClick(item, actionType);
        }
    }

    public GameObject GetSlotInstance() { return gameObject; }

    public Item GetItem() { return CurrentItem; }

    public void OnRectTransformRemoved() { Destroy(gameObject); }
}
