using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Treasure", menuName = "Create New Treasure")]
public class Treasure : Item
{
    [SerializeField]
    private string _type;

    public string Type
    {
        get { return _type; }
        set { _type = value; }
    }
}
