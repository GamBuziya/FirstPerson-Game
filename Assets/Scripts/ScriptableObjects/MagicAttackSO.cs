using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Abstract_classes;
using DefaultNamespace.Enums;
using UnityEngine;


public abstract class MagicAttackSO : ScriptableObject
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

    public abstract void DebafFunc(GameCharacter gameCharacter);
}



[CreateAssetMenu(menuName = "Magic Attacks/Fire Attack")]
public class FireMagicAttack : MagicAttackSO
{
    public override void DebafFunc(GameCharacter gameCharacter)
    {
        if(gameCharacter.GetCurrentStamina() != null) gameCharacter.StaminaDamage(30);
        
        var temp = gameCharacter is IMagic;
        if (temp)
        {
            IMagic magicCharacter = (IMagic)gameCharacter;
            magicCharacter.MagicManager.MagicDamage();
        }
    }
}

[CreateAssetMenu(menuName = "Magic Attacks/Venom Attack")]
public class VenomMagicAttack : MagicAttackSO
{
    public override void DebafFunc(GameCharacter gameCharacter)
    {
        if(gameCharacter.GetCurrentStamina() != null) gameCharacter.StaminaDamage(30);
        
        var temp = gameCharacter is IMagic;
        if (temp)
        {
            IMagic magicCharacter = (IMagic)gameCharacter;
            magicCharacter.MagicManager.MagicDamage();
        }
    }
}

[CreateAssetMenu(menuName = "Magic Attacks/Electricity Attack")]
public class ElectricityMagicAttack : MagicAttackSO
{
    public override void DebafFunc(GameCharacter gameCharacter)
    {
        if(gameCharacter.GetCurrentStamina() != null) gameCharacter.StaminaDamage(30);
        
        var temp = gameCharacter is IMagic;
        if (temp)
        {
            IMagic magicCharacter = (IMagic)gameCharacter;
            magicCharacter.MagicManager.MagicDamage();
        }
    }
}

[CreateAssetMenu(menuName = "Magic Attacks/Ice Attack")]
public class IceMagicAttack : MagicAttackSO
{
    public override void DebafFunc(GameCharacter gameCharacter)
    {
        if(gameCharacter.GetCurrentStamina() != null) gameCharacter.StaminaDamage(30);
        
        var temp = gameCharacter is IMagic;
        if (temp)
        {
            IMagic magicCharacter = (IMagic)gameCharacter;
            magicCharacter.MagicManager.MagicDamage();
        }
    }
}
