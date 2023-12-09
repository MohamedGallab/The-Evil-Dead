using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Herb", menuName = "Create New Herb")]
public class Herb : Item
{
    [SerializeField]
    private string _color;

    [SerializeField]
    private int _healthPoints;

    public string Color
    {
        get { return _color; }
        set { _color = value; }
    }

    public int HealthPoints
    {
        get { return _healthPoints; }
        set { _healthPoints = value; }
    }
}
