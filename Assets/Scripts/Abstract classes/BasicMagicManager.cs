using Abstract_classes;
using DefaultNamespace.Enums;
using UnityEngine;

namespace DefaultNamespace.Abstract_classes
{
    public abstract class BasicMagicManager : MonoBehaviour
    {
        [SerializeField] protected TypeMagicAttack _typeMagicAttack;
        [SerializeField] protected MagicAttackSO[] _magicAttacksSO;
        
        [SerializeField] protected Transform _firepoint;
        [SerializeField] protected float _arcRange = 1f;
        
        [SerializeField] [Range(0, 1)] protected float _regenerateInterval;
        protected MagicGameCharacter _gameCharacter;
        protected MagicAttackSO _currentMagicAttack;
        
        protected Vector3 _destination;
        
        //Ще ні до чого не підв'язані
        private float _magicRegenBonus = 0;
        private float _magicSaveBonus = 0;
        //
        
        public void MagicDamage()
        {
            _gameCharacter.SetCurrentMagic(
                _gameCharacter.GetCurrentMagic() - (_currentMagicAttack.MagicCost - _magicSaveBonus));
        }
    
        public void RegenerateMagic()
        {
            if (_gameCharacter.GetCurrentMagic() <= _gameCharacter.GetMaxMagic())
            {
                _gameCharacter.SetCurrentMagic(_gameCharacter.GetCurrentMagic() + 0.7f + _magicRegenBonus);
            }
        }
        
        private float _timer = 0f;
    
        protected void Start()
        {
            SetCurrentMagic(_typeMagicAttack);
            _gameCharacter = GetComponent<MagicGameCharacter>();
        }

        protected void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= _regenerateInterval)
            {
                RegenerateMagic();
                _timer = 0f;
            }
        }
        
        public void SetCurrentMagic(TypeMagicAttack magicAttack)
        {
            foreach (var attack in _magicAttacksSO)
            {
                if (attack.TypeMagic == magicAttack)
                {
                    _currentMagicAttack = attack;
                    break;
                }
            }
        }
        protected void InstantiateProjectile()
        {
            if (_gameCharacter.GetCurrentMagic() > _currentMagicAttack.MagicCost)
            {
                MagicDamage();
                SoundManager.Instance.MagicAttackSound(_currentMagicAttack.TypeMagic);
                var projectileObj = Instantiate(_currentMagicAttack.Bullet, _firepoint.position, Quaternion.identity);
                projectileObj.GetComponent<Rigidbody>().velocity = 
                    (_destination - _firepoint.position).normalized * _currentMagicAttack.Speed;
        
                iTween.PunchPosition(projectileObj, new Vector3(
                        Random.Range(-_arcRange, _arcRange), 
                        Random.Range(-_arcRange, _arcRange), 0), 
                    Random.Range(0.5f, 2));
            }
        
        }

        public abstract void ShootProjectile();
    }
}