using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mixture", menuName = "Create New Mixture")]
public class Mixture : Item
{
    [SerializeField]
    private int _healthPoints;

    [SerializeField]
    private string _firstColor;

    [SerializeField]
    private string _secondColor;

    public int HealthPoints
    {
        get { return _healthPoints; }
        set { _healthPoints = value; }
    }

    public string FirstColor
    {
        get { return _firstColor; }
        set { _firstColor = value; }
    }

    public string SecondColor
    {
        get { return _secondColor; }
        set { _secondColor = value; }
    }
}
