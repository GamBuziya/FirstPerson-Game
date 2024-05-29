using DefaultNamespace.Abstract_classes;
using UnityEngine;

namespace Managers
{
    public class EnemyMagicManager : BasicMagicManager, IAtackable
    {
        [SerializeField] protected MagicAttackSO[] _magicsAttacksSO;
        private GameObject _player;
        
        protected new void Start()
        {
            base.Start();
            SetCurrentMagic();
            _player = GameObject.Find("Player");
        }
        public void Attack()
        {
            ShootProjectile();
        }

        private new void ShootProjectile()
        {
            //Тут рандом треба поставити по важкості ворога
            var randDestination = new Vector3(
                _player.transform.position.x + Random.Range(-2,2),
                _player.transform.position.y + Random.Range(-0.5f,0.5f),
                _player.transform.position.z + Random.Range(-2,2));
            _destination = randDestination;
            
            base.ShootProjectile();
        }
        
        public void SetCurrentMagic()
        {
            _currentMagicAttack = _magicsAttacksSO[Random.Range(0, _magicsAttacksSO.Length)];
        }
    }
}