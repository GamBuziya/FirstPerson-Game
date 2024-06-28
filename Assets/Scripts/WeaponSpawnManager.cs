using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class WeaponSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _weaponsObjects;
    private void Awake()
    {
        foreach (GameObject weapon in _weaponsObjects)
        {
            Debug.Log("weapon.GetComponent<WeaponManager>().BasicWeaponData.Name" + weapon.GetComponent<WeaponManager>().BasicWeaponData.Name);
            if (weapon.GetComponent<WeaponManager>().BasicWeaponData.Name == GameStatsManager.Instance.SelectedWeapon.Name)
            {
                weapon.GetComponent<WeaponManager>().BasicWeaponData = GameStatsManager.Instance.SelectedWeapon;
                Instantiate(weapon, transform, true);
            }
            
        }
    }
}
