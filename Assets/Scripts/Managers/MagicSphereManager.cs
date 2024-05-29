using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Abstract_classes;
using UnityEngine;

public class MagicSphereManager : MonoBehaviour
{
    [SerializeField] private MagicAttackSO _object;
    private bool collided;
    private GameCharacter _parentCharacter;

    public void SetParent(GameCharacter parent)
    {
        _parentCharacter = parent;
    }
    

    private void OnCollisionEnter(Collision other)
    {
        if (_parentCharacter != other.gameObject.GetComponentInParent<GameCharacter>() && !collided)
        {
            collided = true;
            SoundManager.Instance.MagicAttackSound(_object.TypeMagic, transform.position);
            
            var impact = Instantiate(_object.Impact, other.contacts[0].point, Quaternion.identity);
            Destroy(impact, 1);
            Destroy(gameObject);

            if ((_parentCharacter.GetEnemyLayer() & (1 << other.gameObject.layer)) != 0)
            {
                _object.DebafFunc(other.gameObject.GetComponentInParent<GameCharacter>());
                EventManager.Instance.MagicDamage(_parentCharacter,
                    other.gameObject.GetComponentInParent<GameCharacter>());
            }
        }
    }
}
