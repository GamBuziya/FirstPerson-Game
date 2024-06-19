using System;
using DefaultNamespace.ScriptableObjects;
using UnityEngine;

namespace Managers
{
    public class WeaponManager : MonoBehaviour
    {
        public WeaponSO BasicWeaponData;
        

        private void Awake()
        {
            //Беремо дані про те чи прокачана зброя була до цього
        }

        public void UpdateDamage()
        {
            //DamageLevel++;
            //UpgradedWeaponData.BasicDamage = BasicWeaponData.BasicDamage + DamageLevel * 5;
        }
    }
}