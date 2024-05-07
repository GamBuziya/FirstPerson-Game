using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Abstract_classes;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    void Start()
    {
        EventManager.Instance.onPhysicDamage += EventManagerOnPhysicDamage;
        //EventManager.Instance.on
    }

    private void EventManagerOnPhysicDamage(GameCharacter attacker, GameCharacter defender)
    {
        float modifiedDamage = attacker.GetWeaponDamage() * (1 + Random.Range(-0.25f, 0.25f));
        int damage = (int)((int)(attacker.GetWeaponDamage() + modifiedDamage)*(1 - defender.GetAttackResist()/100));
        defender.GetHealthPoints().TakeDamage(damage);

    }
}
