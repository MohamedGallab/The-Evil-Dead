using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeItem : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        gameObject.SetActive(false);
    }
}
