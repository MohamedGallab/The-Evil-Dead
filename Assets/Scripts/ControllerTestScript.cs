using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class ControllerTestScript : MonoBehaviour
{
    [SerializeField]
    GameObject InventoryManager;

    [SerializeField]
    Canvas InventoryCanvas;

    // Start is called before the first frame update
    void Start()
    {
        UnlockCursor();
    }

    // Update is called once per frame
    void Update()
    {
        UnlockCursor();
        if (Input.GetKeyDown(KeyCode.F)) //fire equipped weapon
        {
            InventoryManagerScript inventoryScript = InventoryManager.GetComponent<InventoryManagerScript>();
            inventoryScript.FireWeapon();
        }

        if (Input.GetKeyDown(KeyCode.G)) //throw equipped grenade
        {
            InventoryManagerScript inventoryScript = InventoryManager.GetComponent<InventoryManagerScript>();
            inventoryScript.ThrowGrenade();
        }
         //reload equipped weapon
        if (Input.GetKeyDown(KeyCode.R))
        {
            InventoryManagerScript inventoryScript = InventoryManager.GetComponent<InventoryManagerScript>();
            inventoryScript.ReloadEquippedWeapon();
        }

        if (Input.GetKeyDown(KeyCode.H)) //knife takes a hit
        {
            InventoryManagerScript inventoryScript = InventoryManager.GetComponent<InventoryManagerScript>();
            inventoryScript.KnifeTakesHit(11);
        }

        if (Input.GetKeyDown(KeyCode.K)) //repair knife
        {
            InventoryManagerScript inventoryScript = InventoryManager.GetComponent<InventoryManagerScript>();
            inventoryScript.RepaireKnife();
        }

        if (Input.GetKeyDown(KeyCode.Tab)) //open/close inventory
        {
            CanvasGroup canvasGroup = InventoryCanvas.GetComponent<CanvasGroup>();
            if(canvasGroup.interactable)
            {
                //Make canvas invisible
                canvasGroup.alpha = 0f;
                //Disable interactions
                canvasGroup.interactable = false;
            }
            else
            {
                //Make canvas invisible
                canvasGroup.alpha = 1f;
                //Disable interactions
                canvasGroup.interactable = true;
            }
        }
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
