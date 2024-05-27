using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Abstract_classes;
using Managers;
using UnityEngine;

public class WeaponsManager : MonoBehaviour, IDataReturner
{
    private WeaponManager[] _weapons;
    private WeaponManager _currentWeapon;
    private int _currentIndex;
    private int _maxWeapons;
    
    private void Awake()
    {
        _weapons = GetComponentsInChildren<WeaponManager>();
        _currentWeapon = _weapons[0];
        _currentIndex = 0;
        _maxWeapons = _weapons.Length;
    }

    private void Start()
    {
        GameStatsManager.Instance.SelectedWeapon = _currentWeapon.WeaponData;
    }
    
    public void SetCurrent(int index)
    {
        _currentIndex = index;
        _currentWeapon = _weapons[_currentIndex];
        GameStatsManager.Instance.SelectedWeapon = _currentWeapon.WeaponData; 
    }

    public int GetCurrent()
    {
        return _currentIndex;
    }

    public int GetMaxCount()
    {
        return _maxWeapons;
    }
}
