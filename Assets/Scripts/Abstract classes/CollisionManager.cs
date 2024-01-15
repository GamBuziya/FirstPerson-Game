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
        
        protected abstract void OnCollisionEnter(Collision other);
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