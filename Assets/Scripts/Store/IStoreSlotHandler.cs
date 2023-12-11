using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStoreSlotHandler
{
    void OnClick(Item item, string actionType);
}
