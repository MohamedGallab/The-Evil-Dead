using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Key", menuName = "Create New Key")]
public class Key : Item
{
    [SerializeField]
    private string _type;

    public string Type
    {
        get { return _type; }
        set { _type = value; }
    }

}
