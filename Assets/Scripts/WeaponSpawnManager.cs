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
            if (weapon.GetComponent<WeaponManager>().WeaponData.Name == GameStatsManager.Instance.SelectedWeapon.Name)
            {
                weapon.GetComponent<WeaponManager>().WeaponData = GameStatsManager.Instance.SelectedWeapon;
                Instantiate(weapon, transform, true);
            }
            
        }
    }
}
