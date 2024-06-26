using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Abstract_classes;
using DefaultNamespace.ScriptableObjects;
using Managers;
using UnityEngine;

public class WeaponsManager : MonoBehaviour, IDataReturner
{
    [SerializeField] private GameObject DamageIconsParent;
    [SerializeField] private GameObject StaminaIconsParent;
    
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
        WeaponSO currentWeapon = null;
        if (GameStatsManager.Instance.WeaponsSO != null)
        {
            currentWeapon = GameStatsManager.Instance.WeaponsSO.FirstOrDefault(
                weapon => weapon.Name == _currentWeapon.BasicWeaponData.Name);
        }
        
        if (currentWeapon == null)
        {
            currentWeapon = _currentWeapon.BasicWeaponData;
            GameStatsManager.Instance.WeaponsSO?.Add(currentWeapon);
        }
        
        GameStatsManager.Instance.SelectedWeapon = currentWeapon;
    }
    
    public void SetCurrent(int index)
    {
        _currentIndex = index;
        _currentWeapon = _weapons[_currentIndex];
        WeaponSO currentWeapon = null;
        if (GameStatsManager.Instance.WeaponsSO != null)
        {
            currentWeapon = GameStatsManager.Instance.WeaponsSO.FirstOrDefault(
                weapon => weapon.Name == _currentWeapon.BasicWeaponData.Name);
        }
        
        if (currentWeapon == null)
        {
            currentWeapon = _currentWeapon.BasicWeaponData;
            GameStatsManager.Instance.WeaponsSO?.Add(currentWeapon);
        }
        GameStatsManager.Instance.SelectedWeapon = currentWeapon;
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
