using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.Abstract_classes
{
    public abstract class CollisionManager : MonoBehaviour
    {
        [SerializeField] protected UnityEvent _IsDamaged;
        [SerializeField] protected UnityEvent _IsBlocked;
        [SerializeField] protected ParticleSystem _particleEffect;
        
        protected GameCharacter _hero;
        protected BlockChecker _checker;
        protected LayerMask _enemyLayer;

        protected void OnCollisionEnter(Collision other)
        {
            Debug.Log("A");
            if (other.gameObject.CompareTag("Weapon") && (_enemyLayer & (1 << other.gameObject.layer)) != 0)
            {
                Debug.Log("b");
                var isBlock = _checker.IsBlock(other.gameObject);
                if (!isBlock)
                {
                    Debug.Log("c");
                    _IsDamaged.Invoke();
                }
                else
                {
                    PlayParticleEffect(other.contacts[0].point);
                    var enemyCharacter = other.gameObject.GetComponentInParent<GameCharacter>();
                    enemyCharacter.SetStun(true);
                    _IsBlocked.Invoke();
                    enemyCharacter.GetAnimatorManager().EnemyParriedEffect();
                }
            
            }
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