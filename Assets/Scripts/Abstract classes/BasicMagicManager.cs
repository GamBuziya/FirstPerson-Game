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
        protected GameCharacter _gameCharacter;
        protected IMagic _magicGameCharacter;
        protected MagicAttackSO _currentMagicAttack;
        
        protected Vector3 _destination;
        
        //Ще ні до чого не підв'язані
        private float _magicRegenBonus = 0;
        private float _magicSaveBonus = 0;
        //
        
        private float _timer = 0f;
    
        protected void Start()
        {
            SetCurrentMagic(_typeMagicAttack);
            _gameCharacter = GetComponent<GameCharacter>();
            _magicGameCharacter = (IMagic)_gameCharacter;
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
        
        public void MagicDamage()
        {
            _magicGameCharacter.CurrentMagic = _magicGameCharacter.CurrentMagic - (_currentMagicAttack.MagicCost - _magicSaveBonus);
        }
    
        public void RegenerateMagic()
        {
            if (_magicGameCharacter.CurrentMagic <= _magicGameCharacter.MaxMagic)
            {
                _magicGameCharacter.CurrentMagic = _magicGameCharacter.CurrentMagic + 0.7f + _magicRegenBonus;
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
            if (_magicGameCharacter.CurrentMagic > _currentMagicAttack.MagicCost)
            {
                MagicDamage();
                SoundManager.Instance.MagicAttackSound(_currentMagicAttack.TypeMagic, transform.position);
                var projectileObj = Instantiate(
                    _currentMagicAttack.Bullet, 
                    _firepoint.position, 
                    Quaternion.identity); 
                projectileObj.GetComponent<MagicSphereManager>().SetParent(_gameCharacter);
                
                projectileObj.GetComponent<Rigidbody>().velocity = 
                    (_destination - _firepoint.position).normalized * _currentMagicAttack.Speed;
        
                iTween.PunchPosition(projectileObj, new Vector3(
                        Random.Range(-_arcRange, _arcRange), 
                        Random.Range(-_arcRange, _arcRange), 0), 
                    Random.Range(0.5f, 2));
            }
        
        }

        protected void ShootProjectile()
        {
            InstantiateProjectile();
        }
        
        public MagicAttackSO GetMagicData() => _currentMagicAttack;
    }
}