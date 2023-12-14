using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreSlot : MonoBehaviour
{
    // Prefab requirements
    [SerializeField] TextMeshProUGUI ItemNameDisplay;
    [SerializeField] Image ItemImageDisplay;
    [SerializeField] TextMeshProUGUI ItemPriceText;
    [SerializeField] TextMeshProUGUI ItemDescriptionText;
    [SerializeField] GameObject AmmoGroup;
    [SerializeField] TextMeshProUGUI AmmoCountText;

    IStoreSlotHandler _slotHandler;
    public Item CurrentItem { get; private set; }
    string _actionType;
    public int _ammoStackCount;
    int _inventorySlotIndex;

    // Initializes the slot with the given item and type
    public void InitializeItem(Item newItem, string type, IStoreSlotHandler handle, int stackCount, int inventorySlotIndex)
    {
        CurrentItem = newItem;
        _actionType = type;
        _slotHandler = handle;
        _inventorySlotIndex = inventorySlotIndex;

        // Initialize the item's name, description and image
        ItemNameDisplay.SetText(CurrentItem.ItemName);
        ItemDescriptionText.SetText(CurrentItem.ItemDescription);
        if (CurrentItem.ItemImage != null)
        {
            ItemImageDisplay.sprite = CurrentItem.ItemImage;
            ItemImageDisplay.enabled = true;
        }

        // Create a button onClick listener
        Button button = GetComponent<Button>();
        button.onClick.AddListener(HandleButtonClick);

        // Initialize the item's price
        if (_actionType == "Purchase")
        {
            ItemPriceText.SetText(CurrentItem.BuyPrice.ToString());

            // Show the ammo count if the item is ammo
            if (CurrentItem is Ammo)
            {
                _ammoStackCount = stackCount;
                AmmoCountText.SetText((CurrentItem as Ammo).AmountPerPack.ToString());
                AmmoGroup.SetActive(true);
            }
        }
        else if (_actionType == "Sell")
        {
            ItemPriceText.SetText(CurrentItem.SellPrice.ToString());
            // If the item is not sellable, disable the button
            if (!CurrentItem.IsSellable)
            {
                button.interactable = false;
            }

            // If the item is ammo, display the ammo count
            if (CurrentItem is Ammo)
            {
                _ammoStackCount = stackCount;
                AmmoCountText.SetText(_ammoStackCount.ToString());
                AmmoGroup.SetActive(true);
            }
        } else if (_actionType == "Store" || _actionType == "Unstore")
        {
            ItemPriceText.SetText(CurrentItem.SellPrice.ToString());

            // If the item is ammo, display the ammo count
            if (CurrentItem is Ammo)
            {
                _ammoStackCount = stackCount;
                AmmoCountText.SetText(_ammoStackCount.ToString());
                AmmoGroup.SetActive(true);
            }
        }
    }

    // Handles the button click event by invoking the 'OnClick' method of the assigned slot handler,
    void HandleButtonClick()
    {
        if (_slotHandler != null)
        {
            if (_actionType == "Purchase" || _actionType == "Unstore")
            {
                _slotHandler.AddToInventory(CurrentItem, _ammoStackCount);
            }
            else if (_actionType == "Sell" || _actionType == "Store")
            {
                _slotHandler.RemoveFromInventory(CurrentItem, _inventorySlotIndex);
            }
        }
    }

    public void StackAmmo(Ammo ammoItem)
    {
        _ammoStackCount += ammoItem.AmountPerPack;
        AmmoCountText.SetText(_ammoStackCount.ToString());
    }

    public GameObject GetSlotInstance() { return gameObject; }

    public void OnRectTransformRemoved() { Destroy(gameObject); }
}
