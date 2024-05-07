using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Enums;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.Abstract_classes
{
    public abstract class CollisionManager : MonoBehaviour
    {
        [SerializeField] protected UnityEvent _IsDamaged;
        [SerializeField] protected UnityEvent _IsBlocked;
        [SerializeField] protected ParticleSystem _particleEffect;
        
        protected GameCharacter _gameCharacter;
        protected BlockChecker _checker;
        protected LayerMask _enemyLayer;

        protected bool _onCollision = false;

        protected void OnCollisionEnter(Collision other)
        {
            if (_onCollision == true) return;
            
            
            if (other.gameObject.CompareTag("Weapon") 
                && (_enemyLayer & (1 << other.gameObject.layer)) != 0 
                && other.gameObject.GetComponentInParent<GameCharacter>().GetBattleController().GetCurrentTypeOfMove() == TypeOfMove.IsAttack)
            {
                _onCollision = true;
                var isBlock = _checker.IsBlock(other.gameObject, _gameCharacter.gameObject);
                if (!isBlock)
                {
                    EventManager.Instance.PhysicDamage(other.gameObject.GetComponentInParent<GameCharacter>(), GetComponent<GameCharacter>());
                    ParticleEffectsManager.Instance.CreateBloodEffect(other.contacts[0].point);
                    SoundManager.Instance.HitSound(gameObject);
                }
                else
                {
                    SoundManager.Instance.BlockSound(gameObject);
                    PlayParticleEffect(other.contacts[0].point);
                    var enemyCharacter = other.gameObject.GetComponentInParent<GameCharacter>();
                    enemyCharacter.SetStun(true);
                    _IsBlocked.Invoke();
                    enemyCharacter.GetAnimatorManager().EnemyParriedEffect();
                }
                StartCoroutine(DelayedCollisionHandling());
            
            }
        }


        private IEnumerator DelayedCollisionHandling()
        {
            yield return new WaitForSeconds(0.3f);
            _onCollision = false;
        }
        protected void SubscribeToAttack(UnityAction action)
        {
            _IsDamaged.AddListener(action);
        }
        
        protected void PlayParticleEffect(Vector3 position)
        {
            if (_particleEffect != null)
            {
                ParticleSystem particleInstance = Instantiate(_particleEffect, position, Quaternion.identity);
                particleInstance.Play();
                Destroy(particleInstance.gameObject, particleInstance.main.duration);
            }
        }
    }
}