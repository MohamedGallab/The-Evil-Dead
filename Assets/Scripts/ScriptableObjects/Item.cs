using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Create New Item")]
public class Item : ScriptableObject
{
    //This is the generic item that can be placed in the inventory


    [SerializeField]
    private Sprite _itemImage;

    [SerializeField]
    private string _itemName;

    [SerializeField]
    private string _itemDescription;

    [SerializeField]
    private bool _isSellable;

    [SerializeField]
    private bool _isBuyable;

    [SerializeField]
    private int _buyPrice;

    [SerializeField]
    private int _sellPrice;

    //setters and getters
    public Sprite ItemImage
    {
        get { return _itemImage; }
        set { _itemImage = value; }
    }

    public string ItemName
    {
        get { return _itemName; }
        set { _itemName = value; }
    }

    public string ItemDescription
    {
        get { return _itemDescription; }
        set { _itemDescription = value; }
    }

    public bool IsSellable
    {
        get { return _isSellable; }
        set { _isSellable = value; }
    }

    public bool IsBuyable
    {
        get { return _isBuyable; }
        set { _isBuyable = value; }
    }

    public int BuyPrice
    {
        get { return _buyPrice; }
        set { _buyPrice = value; }
    }

    public int SellPrice
    {
        get { return _sellPrice; }
        set { _sellPrice = value; }
    }

}
