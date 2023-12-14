using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine.UI;
using UnityEngine;

public class PistolTest : MonoBehaviour, IInteractable
{
    [SerializeField]
    Item Item;

    [SerializeField]
    GameObject StoreCanvas;
    InventoryManagerScript InventoryScript;

    [SerializeField]
    StoreManager storeManager;

    [SerializeField]
    KnifeMenuManager knifeMenuManager;

    [SerializeField]
    GameObject KnifeMenuCanvas;
    void Start()
    {
        GameObject InventoryManager = GameObject.Find("InventoryManager");
        InventoryScript = InventoryManager.GetComponent<InventoryManagerScript>();
    }
    public void OnInteract()
    {
        knifeMenuManager.UpdateKnife();
        KnifeMenuCanvas.SetActive(true);
        //if(InventoryScript.CanPickupItem(Item))
        //{
        //    InventoryScript.PickUpItem(Item);
        //    gameObject.SetActive(false);
        //    //play some sound for pickup
        //}
        //else
        //{
        //    //play some sound that you cannot pick up the item
        //}

    }
}

