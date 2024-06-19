using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.ScriptableObjects;
using JetBrains.Annotations;
using Managers;
using UnityEngine;

public class GameStatsManager : MonoBehaviour
{
    public static GameStatsManager Instance;
    
    public WeaponSO SelectedWeapon;
    public MagicAttackSO SelectedMagic;
    
    
    public int Coins;
    public int LevelHealthBonus = 0;
    public int LevelStaminaBonus = 0;
    public int LevelMagicBonus = 0;
    [CanBeNull] public List<WeaponSO> WeaponsSO;
    [CanBeNull] public List<MagicAttackSO> MagicAttackSO;

    private void Awake()
    {
        //отримати дані з веапонСО якщо вони є
        
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        
        DontDestroyOnLoad(gameObject);
    }
}
