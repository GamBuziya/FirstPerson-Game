using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Enums;
using UnityEngine;

[CreateAssetMenu]
public class MagicAttackSO : ScriptableObject
{
    public GameObject Bullet;
    public GameObject Impact;
    public TypeMagicAttack TypeMagic;
    public int Damage;
    public int MagicCost;
    public int Speed;
    
    [Header("Bonuses")]
    public int DamageBonus;
    public int SaveMagicBonus;
}
