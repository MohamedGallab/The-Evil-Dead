using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine.UI;
using UnityEngine;

public class PistolTest : MonoBehaviour, IInteractable
{
    [SerializeField]
    Item Item;

    InventoryManagerScript InventoryScript;

    void Start()
    {
        GameObject InventoryManager = GameObject.Find("InventoryManager");
        InventoryScript = InventoryManager.GetComponent<InventoryManagerScript>();
    }
    public void OnInteract()
    {
        if (InventoryScript.CanPickupItem(Item))
        {
            InventoryScript.PickUpItem(Item);
            gameObject.SetActive(false);
            //play some sound for pickup
        }
        else
        {
            //play some sound that you cannot pick up the item
        }
    }
}
