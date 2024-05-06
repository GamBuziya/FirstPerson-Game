using Abstract_classes;
using GameCharacters;
using UnityEngine;

namespace DefaultNamespace.EnemyFol
{
    public class MagicEnemy : EnemyGameCharacter
    {
        [Header("Magic")] 
        [SerializeField] protected float _maxMagic;
        public float GetMaxMagic() => _maxMagic;

        
        private float _curentMagicCount;
        public float GetCurrentMagic() => _curentMagicCount;
        public void SetCurrentMagic(float magic) => _curentMagicCount = magic;
        
        protected new void Awake()
        {
            base.Awake();
            _curentMagicCount = _maxMagic;
        }
    }
}