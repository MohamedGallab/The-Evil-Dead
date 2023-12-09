using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Knife", menuName = "Create New Knife")]
public class Knife : Item
{
    [SerializeField]
    private int _durability;

    public int Durability
    {
        get { return _durability; }
        set { _durability = value; }
    }

}
