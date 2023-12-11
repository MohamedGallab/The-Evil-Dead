using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Knife", menuName = "Create New Knife")]
public class Knife : Item
{
    [SerializeField]
    private float _durability;

    public float Durability
    {
        get { return _durability; }
        set { _durability = value; }
    }

    // Static method to create an instance of the Scriptable Object KNife so it can be placed in the inventory whenever the game starts
    public static Knife CreateInstance()
    {
        return CreateInstance<Knife>();
    }

}
