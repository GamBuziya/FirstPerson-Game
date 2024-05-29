using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.ScriptableObjects;
using Managers;
using UnityEngine;

public class GameStatsManager : MonoBehaviour
{
    public static GameStatsManager Instance;

    public int Coins;
    public WeaponSO SelectedWeapon;
    public MagicAttackSO SelectedMagic;
    public int LevelHealthBonus = 0;
    public int LevelStaminaBonus = 0;
    public int LevelMagicBonus = 0;

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
