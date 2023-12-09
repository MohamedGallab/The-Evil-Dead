using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ammo", menuName = "Create New Ammo")]
public class Ammo : Item
{
    [SerializeField]
    private string _weapon;

    [SerializeField]
    private int _amount;

    public string Weapon
    {
        get { return _weapon; }
        set { _weapon = value; }
    }

    public int Amount
    {
        get { return _amount; }
        set { _amount = value; }
    }

}
