using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Create New Weapon")]
public class Weapon : Item
{
    //This is the generic weapon that can be placed in the inventory

    [SerializeField]
    private bool _isTwoHanded;

    [SerializeField]
    private bool _firingMode; //0 for single-shot, 1 for automatic

    [SerializeField]
    private int _damage;

    [SerializeField]
    private float _timeBetweenShots; //in seconds

    [SerializeField]
    private string _range;

    [SerializeField]
    private int _capacity;

    [SerializeField]
    private int _ammoLeft; //decrement with each shot

    //setters and getters
    public bool IsTwoHanded
    {
        get { return _isTwoHanded; }
        set { _isTwoHanded = value; }
    }

    public bool FiringMode
    {
        get { return _firingMode; }
        set { _firingMode = value; }
    }

    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    public float TimeBetweenShots
    {
        get { return _timeBetweenShots; }
        set { _timeBetweenShots = value; }
    }

    public string Range
    {
        get { return _range; }
        set { _range = value; }
    }

    public int Capacity
    {
        get { return _capacity; }
        set { _capacity = value; }
    }

    public int AmmoLeft
    {
        get { return _ammoLeft; }
        set { _ammoLeft = value; }
    }

    public bool CanFire()
    {
        if (_ammoLeft>0)
        {
            return true;
        }
        else
        {
            //play empty ammo sound
            return false;
        }
    }

    public void Fire()
    {
        _ammoLeft--;
        //play shot sound
    }

}
