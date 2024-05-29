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
        public BasicMagicManager MagicManager { get; set; }

        protected new void Awake()
        {
            base.Awake();
            MagicManager = GetComponent<BasicMagicManager>();
            CurrentMagic = _maxMagic;
            MaxMagic = _maxMagic;
        }
    }
}