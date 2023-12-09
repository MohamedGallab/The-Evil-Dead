using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Grenade", menuName = "Create New Grenade")]
public class Grenade : Item
{

    [SerializeField]
    private string _type;

    public string Type
    {
        get { return _type; }
        set { _type = value; }
    }
}
