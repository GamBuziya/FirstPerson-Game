using System;
using DefaultNamespace;
using DefaultNamespace.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UpgradesManager : MonoBehaviour
    {
        [Header("BaseStats")]
        [SerializeField] private BarDataManager _healthBonus;
        [SerializeField] private BarDataManager _staminaBonus;
        [SerializeField] private BarDataManager _magicBonus;
        
        [Header("Weapon")]
        [SerializeField] private BarDataManager _weaponDamage;
        [SerializeField] private BarDataManager _weaponStamina;
        
        [Header("Magic")]
        [SerializeField] private BarDataManager _magicDamage;
        [SerializeField] private BarDataManager _magicStamina;
        private void Start()
        {
            GameStatsManager.Instance.ChangeStats += UpdateData;

            ConfigureButtonActions();
            Invoke("UpdateAllData", 0.3f);
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
                    break;
                default:
                    UpdateAllData();
                    break;
            }
        }

        private void ConfigureButtonActions()
        {
            ConfigureButton(_healthBonus.GetComponentInChildren<Button>(), 
                () => GameStatsManager.Instance.LevelHealthBonus, 
                value => GameStatsManager.Instance.LevelHealthBonus = value);
            ConfigureButton(_staminaBonus.GetComponentInChildren<Button>(), 
                () => GameStatsManager.Instance.LevelStaminaBonus, 
                value => GameStatsManager.Instance.LevelStaminaBonus = value);
            
            ConfigureButton(_magicBonus.GetComponentInChildren<Button>(), 
                () => GameStatsManager.Instance.LevelMagicBonus, 
                value => GameStatsManager.Instance.LevelMagicBonus = value);
            
            ConfigureButton(_weaponDamage.GetComponentInChildren<Button>(), 
                () => GameStatsManager.Instance.SelectedWeapon.DamageLevel, 
                value => GameStatsManager.Instance.SelectedWeapon.DamageLevel = value);
            
            ConfigureButton(_weaponStamina.GetComponentInChildren<Button>(), 
                () => GameStatsManager.Instance.SelectedWeapon.StaminaLevel, 
                value => GameStatsManager.Instance.SelectedWeapon.StaminaLevel = value);
            
            ConfigureButton(_magicDamage.GetComponentInChildren<Button>(), 
                () => GameStatsManager.Instance.SelectedMagic.DamageBonus, 
                value => GameStatsManager.Instance.SelectedMagic.DamageBonus = value);
            ConfigureButton(_magicStamina.GetComponentInChildren<Button>(), 
                () => GameStatsManager.Instance.SelectedMagic.SaveMagicBonus, 
                value => GameStatsManager.Instance.SelectedMagic.SaveMagicBonus = value);
        }

        private void ConfigureButton(Button button, Func<int> getter, Action<int> setter)
        {
            button.onClick.AddListener(() => OnClickButton(getter, setter));
        }

        private void OnClickButton(Func<int> getter, Action<int> setter)
        {
            int value = getter();
            int price = value * 200 + 200;
            GameStatsManager.Instance.Coins -= price;
            setter(value + 1);
            UpdateAllData();
        }

        private void UpdateAllData()
        {
            _weaponDamage.UpdateData(GameStatsManager.Instance.SelectedWeapon.DamageLevel);
            _weaponStamina.UpdateData(GameStatsManager.Instance.SelectedWeapon.StaminaLevel);
            _magicDamage.UpdateData(GameStatsManager.Instance.SelectedMagic.DamageBonus);
            _magicStamina.UpdateData(GameStatsManager.Instance.SelectedMagic.SaveMagicBonus);
            _healthBonus.UpdateData(GameStatsManager.Instance.LevelHealthBonus);
            _staminaBonus.UpdateData(GameStatsManager.Instance.LevelStaminaBonus);
            _magicBonus.UpdateData(GameStatsManager.Instance.LevelMagicBonus);
        }
    }
}