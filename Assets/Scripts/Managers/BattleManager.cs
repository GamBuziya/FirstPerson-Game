using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Abstract_classes;
using DefaultNamespace.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleManager : MonoBehaviour
{
    
    void Start()
    {
        EventManager.Instance.onPhysicDamage += EventManagerOnPhysicDamage;
        EventManager.Instance.onMagicDamage += EventManagerOnMagicDamage;
    }

    private void EventManagerOnPhysicDamage(GameCharacter attacker, GameCharacter defender)
    {
        float modifiedDamage = 0f;
        int damageType = 0;
        if (attacker.TryGetComponent(out SwordEnemy swordEnemy))
        {
            if (swordEnemy.GetBattleController().GetCurrentMove() == SideOfMove.Up)
            {
                damageType = swordEnemy.Weapon.BasicWeaponData.PowerDamage;
            }
            else
            {
                damageType = swordEnemy.Weapon.BasicWeaponData.BasicDamage;
            }
            modifiedDamage = damageType * (1 + Random.Range(-0.25f, 0.25f));
        }
        else if (attacker.TryGetComponent(out Player player))
        {
            var bonus = GameStatsManager.Instance.SelectedWeapon.DamageLevel;
            if (player.GetBattleController().GetCurrentMove() == SideOfMove.Up)
            {
                damageType = player.Weapon.BasicWeaponData.PowerDamage + bonus * 3;
            }
            else
            {
                damageType = player.Weapon.BasicWeaponData.BasicDamage + bonus * 3;
            }
            modifiedDamage = damageType * (1 + Random.Range(-0.25f, 0.25f));
        }
        
        int damage = (int)((int)(modifiedDamage)*(1 - defender.GetAttackResist()/100));
        defender.GetHealthPoints().TakeDamage(damage);
    }
    
    private void EventManagerOnMagicDamage(GameCharacter attacker, GameCharacter defender)
    {
        var magic = attacker as IMagic;
        var bonus = 0;
        
        if (attacker.TryGetComponent(out Player player))
        {
            bonus = GameStatsManager.Instance.SelectedMagic.DamageBonus * 5;
        }
        float modifiedDamage = (magic.MagicManager.GetMagicData().Damage + bonus) * (1 + Random.Range(-0.25f, 0.25f));
        int damage = (int)((int)(modifiedDamage)*(1 - defender.GetCurrentMagicResist(magic)/100));
        defender.GetHealthPoints().TakeDamage(damage);
    }

    public void DamageWithTimer(ref float count, float timer)
    {
        StartCoroutine(Damage(count, timer, OnCountUpdated));
    }

    private IEnumerator Damage(float count, float timer, Action<float> onCountUpdated)
    {
        while (timer > 0f)
        {
            count -= 5f; // Зменшення count на 1
            yield return new WaitForSeconds(1f); // Затримка на 1 секунду
            timer -= 1f; // Зменшення часу на 1 секунду
        }
    }
    
    private void OnCountUpdated(float count)
    {
        // Оновлене значення count
        Debug.Log("Updated count: " + count);
    }
}
