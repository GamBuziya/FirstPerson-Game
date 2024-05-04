using System;
using System.Collections;
using System.Collections.Generic;
using Abstract_classes;
using DefaultNamespace.Enums;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlayerMagicManager : MonoBehaviour
{
    [SerializeField] private TypeMagicAttack _typeMagicAttack;
    [SerializeField] private MagicAttackSO[] _magicAttacksSO;
    [SerializeField] private Transform _firepoint;
    [SerializeField] private float _arcRange = 1f;
    [SerializeField] [Range(0, 1)] private float _regenerateInterval;

    private MagicGameCharacter _gameCharacter;
    
    private MagicAttackSO _currentMagicAttack;
    private Camera _camera;
    private Vector3 _destination;
    
    //Ще ні до чого не підв'язані
    private float _magicRegenBonus = 0;
    private float _magicSaveBonus = 0;
    //
    
    
    private float _timer = 0f;
    
    private void Start()
    {
        _camera = GetComponentInChildren<Camera>();
        SetCurrentMagic(_typeMagicAttack);
        _gameCharacter = GetComponent<MagicGameCharacter>();
    }

    private void Update()
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
    
    public void ShootProjectile()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            _destination = hit.point;
        }
        else
        {
            _destination = ray.GetPoint(500);
        }

        InstantiateProjectile();
    }

    private void InstantiateProjectile()
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
}
