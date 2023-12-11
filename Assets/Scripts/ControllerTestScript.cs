using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class ControllerTestScript : MonoBehaviour
{
    [SerializeField]
    GameObject EquippedWeapon;

    [SerializeField]
    GameObject KnifeSlot;

    [SerializeField]
    GameObject InventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        UnlockCursor();
    }

    // Update is called once per frame
    void Update()
    {
        UnlockCursor();
        if (Input.GetKeyDown(KeyCode.F))
        {
            InventoryEquippedWeaponSlot equippedScript = EquippedWeapon.GetComponent<InventoryEquippedWeaponSlot>();
            equippedScript.Fire();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            InventoryManagerScript equippedScript = InventoryManager.GetComponent<InventoryManagerScript>();
            equippedScript.ThrowGrenade();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            InventoryManagerScript equippedScript = InventoryManager.GetComponent<InventoryManagerScript>();
            equippedScript.ReloadEquippedWeapon();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            InventoryKnifeSlot equippedScript = KnifeSlot.GetComponent<InventoryKnifeSlot>();
            equippedScript.TakeHit(11);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            InventoryKnifeSlot equippedScript = KnifeSlot.GetComponent<InventoryKnifeSlot>();
            equippedScript.Repair();
        }
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
