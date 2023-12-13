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

    IStoreSlotHandler slotHandler;
    public Item CurrentItem { get; private set; }
    string actionType;
    int ammoStackCount;

    // Initializes the slot with the given item and type
    public void InitializeItem(Item newItem, string type, IStoreSlotHandler handle, int stackCount)
    {
        CurrentItem = newItem;
        actionType = type;
        slotHandler = handle;

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
        if (actionType == "Purchase")
        {
            ItemPriceText.SetText(CurrentItem.BuyPrice.ToString());

            // Show the ammo count if the item is ammo
            if (CurrentItem is Ammo)
            {
                ammoStackCount = stackCount;
                AmmoCountText.SetText((CurrentItem as Ammo).AmountPerPack.ToString());
                AmmoGroup.SetActive(true);
            }
        }
        else if (actionType == "Sell")
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
                ammoStackCount = stackCount;
                AmmoCountText.SetText(stackCount.ToString());
                AmmoGroup.SetActive(true);
            }
        }
    }

    // Handles the button click event by invoking the 'OnClick' method of the assigned slot handler,
    void HandleButtonClick()
    {
        if (slotHandler != null)
        {
            slotHandler.OnClick(CurrentItem, actionType);
        }
    }

    // Updates the ammo stack count
    public void UpdateAmmoStackCount(int newStackCount)
    {
        ammoStackCount += newStackCount;
        AmmoCountText.SetText(ammoStackCount.ToString());
    }

    public GameObject GetSlotInstance() { return gameObject; }

    public void OnRectTransformRemoved() { Destroy(gameObject); }
}
