using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Abstract_classes;
using UnityEngine;

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
            Debug.Log("swordEnemy");
            modifiedDamage = swordEnemy.WeaponDamage * (1 + Random.Range(-0.25f, 0.25f));
        }
        else if (attacker.TryGetComponent(out Player player))
        {
            Debug.Log("Player");
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
}
