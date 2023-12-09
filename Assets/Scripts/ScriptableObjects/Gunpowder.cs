using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gunpowder", menuName = "Create New Gunpowder")]
public class Gunpowder : Item
{
    [SerializeField]
    private string _type;

    public string Type
    {
        get { return _type; }
        set { _type = value; }
    }

}
