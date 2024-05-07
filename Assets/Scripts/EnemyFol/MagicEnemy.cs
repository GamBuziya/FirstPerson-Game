using Abstract_classes;
using DefaultNamespace.Abstract_classes;
using GameCharacters;
using UnityEngine;

namespace DefaultNamespace.EnemyFol
{
    public class MagicEnemy : EnemyGameCharacter, IMagic
    {
        [SerializeField]
        private float _maxMagic;
        public float MaxMagic {get; set; }
        public float CurrentMagic { get; set; }
        
        protected new void Awake()
        {
            base.Awake();
            CurrentMagic = _maxMagic;
        }
    }
}