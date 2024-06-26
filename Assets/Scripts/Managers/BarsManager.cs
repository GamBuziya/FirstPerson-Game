using System;
using DefaultNamespace;
using DefaultNamespace.Enums;
using UnityEngine;

namespace Managers
{
    public class BarsManager : MonoBehaviour
    {
        [Header("BaseStats")]
        [SerializeField] private BarDataChanger _healthBonus;
        [SerializeField] private BarDataChanger _staminaBonus;
        [SerializeField] private BarDataChanger _magicBonus;
        
        [Header("Weapon")]
        [SerializeField] private BarDataChanger _weaponDamage;
        [SerializeField] private BarDataChanger _weaponStamina;
        
        [Header("Magic")]
        [SerializeField] private BarDataChanger _magicDamage;
        [SerializeField] private BarDataChanger _magicStamina;
        private void Start()
        {
            GameStatsManager.Instance.ChangeStats += UpdateData;
            UpdateData(DataType.HealthBonus);
            UpdateData(DataType.StaminaBonus);
            UpdateData(DataType.MagicBonus);
        }

        private void UpdateData(DataType type)
        {
            switch (type)
            {
                case DataType.Weapon:
                    _weaponDamage.UpdateData(GameStatsManager.Instance.SelectedWeapon.DamageLevel);
                    _weaponStamina.UpdateData(GameStatsManager.Instance.SelectedWeapon.StaminaLevel);
                    break;
                case DataType.Magic:
                    _magicDamage.UpdateData(GameStatsManager.Instance.SelectedMagic.DamageBonus);
                    _magicStamina.UpdateData(GameStatsManager.Instance.SelectedMagic.SaveMagicBonus);
                    break;
                case DataType.HealthBonus:
                    _healthBonus.UpdateData(GameStatsManager.Instance.LevelHealthBonus);
                    break;
                case DataType.StaminaBonus:
                    _staminaBonus.UpdateData(GameStatsManager.Instance.LevelStaminaBonus);
                    break;
                case DataType.MagicBonus:
                    _magicBonus.UpdateData(GameStatsManager.Instance.LevelMagicBonus);
                    break;
                case DataType.Coins:
                    _weaponStamina.UpdateData(GameStatsManager.Instance.Coins);
                    break;
            }
        }
    }
}