using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KnifeManager : MonoBehaviour
{
    [SerializeField] InventoryKnifeSlot inventoryKnifeSlot;

    [SerializeField] TextMeshProUGUI knifeText;

    void Awake()
    {
        knifeText.SetText(6.ToString());
    }

    public void RepairKnife()
    {
        inventoryKnifeSlot.Repair();
        knifeText.SetText(10.ToString());

    }


}
