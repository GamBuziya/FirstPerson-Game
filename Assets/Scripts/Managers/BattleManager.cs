using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Abstract_classes;
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
        if (attacker.TryGetComponent(out SwordEnemy swordEnemy))
        { 
            modifiedDamage = swordEnemy.WeaponDamage * (1 + Random.Range(-0.25f, 0.25f));
        }
        else if (attacker.TryGetComponent(out Player player))
        {
            modifiedDamage = player.WeaponDamage * (1 + Random.Range(-0.25f, 0.25f));
        }
        
        
        int damage = (int)((int)(modifiedDamage)*(1 - defender.GetAttackResist()/100));
        defender.GetHealthPoints().TakeDamage(damage);
    }
    
    private void EventManagerOnMagicDamage(GameCharacter attacker, GameCharacter defender)
    {
        var magic = attacker as IMagic;
        
        float modifiedDamage = (magic.MagicManager.GetMagicData().Damage + magic.MagicManager.GetMagicData().DamageBonus) * (1 + Random.Range(-0.25f, 0.25f));
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
