using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KnifeMenuManager : MonoBehaviour
{
    [SerializeField] InventoryManagerScript inventoryManagerScript;
    [SerializeField] InventoryKnifeSlot inventoryKnifeSlot;

    [SerializeField] TextMeshProUGUI knifeText;
    [SerializeField] GameObject progressBar;
    [SerializeField] Button repairButton;
    [SerializeField] Button repairButton2;

    void Awake()
    {
        knifeText.SetText(10.ToString());
        repairButton.onClick.AddListener(RepairKnife);
        repairButton2.onClick.AddListener(blah);
    }

    void blah()
    {
        inventoryManagerScript.KnifeTakesHit(1);
        UpdateKnife();
    }


    public void UpdateKnife()
    {
        float knifeDurability = inventoryKnifeSlot.KnifeItem.Durability;
        knifeText.SetText(knifeDurability.ToString());
        Vector3 scaleFactor = new Vector3(0.1f * knifeDurability, 1f, 1f);
        progressBar.transform.localScale = scaleFactor;
        if (inventoryKnifeSlot.KnifeItem.Durability == 10 || !inventoryManagerScript.IsEnoughGold(100))
        {
            repairButton.interactable = false;
        } else
        {
            repairButton.interactable = true;
        }
    }

    void RepairKnife()
    {
        if (inventoryManagerScript.IsEnoughGold(100))
        {
            inventoryKnifeSlot.Repair();
            inventoryManagerScript.UpdateGold(-100);
            UpdateKnife();
        }
    }


}
