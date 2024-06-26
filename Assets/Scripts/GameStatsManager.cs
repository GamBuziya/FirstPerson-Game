using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Enums;
using DefaultNamespace.ScriptableObjects;
using JetBrains.Annotations;
using Managers;
using UnityEngine;

public class GameStatsManager : MonoBehaviour
{
    public static GameStatsManager Instance;
    public Action<DataType> ChangeStats;
    private WeaponSO _selectedWeapon;
    private MagicAttackSO _selectedMagic;
    private int _coins;
    private int _levelHealthBonus;
    private int _levelStaminaBonus;
    private int _levelMagicBonus;

    #region Properties
    public WeaponSO SelectedWeapon
    {
        get => _selectedWeapon;
        set
        {
            if (_selectedWeapon != value)
            {
                _selectedWeapon = value;
                ChangeStats.Invoke(DataType.Weapon);
            }
        }
    }

    public MagicAttackSO SelectedMagic
    {
        get => _selectedMagic;
        set
        {
            if (_selectedMagic != value)
            {
                _selectedMagic = value;
                ChangeStats.Invoke(DataType.Magic);
            }
        }
    }
    public int Coins
    {
        get => _coins;
        set
        {
            if (_coins != value)
            {
                _coins = value;
                ChangeStats.Invoke(DataType.Coins);
            }
        }
    }

    public int LevelHealthBonus
    {
        get => _levelHealthBonus;
        set
        {
            if (_levelHealthBonus != value)
            {
                _levelHealthBonus = value;
                ChangeStats.Invoke(DataType.HealthBonus);
            }
        }
    }

    public int LevelStaminaBonus
    {
        get => _levelStaminaBonus;
        set
        {
            if (_levelStaminaBonus != value)
            {
                _levelStaminaBonus = value;
                ChangeStats.Invoke(DataType.StaminaBonus);
            }
        }
    }

    public int LevelMagicBonus
    {
        get => _levelMagicBonus;
        set
        {
            if (_levelMagicBonus != value)
            {
                _levelMagicBonus = value;
                ChangeStats.Invoke(DataType.MagicBonus);
            }
        }
    }
    #endregion
    
    [CanBeNull] public List<WeaponSO> WeaponsSO;
    [CanBeNull] public List<MagicAttackSO> MagicsAttackSO;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        
        DontDestroyOnLoad(gameObject);
    }
}
