using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Enums;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class MagicManager : MonoBehaviour
{
    [SerializeField] private TypeMagicAttack _typeMagicAttack;
    
    [SerializeField] private MagicAttackSO[] _magicAttacksSO;
    [SerializeField] private Transform _firepoint;
    [SerializeField] private float _arcRange = 1f;
    
    private MagicAttackSO _currentMagicAttack;
    private Camera _camera;
    private Vector3 _destination;
    
    private void Start()
    {
        _camera = GetComponentInChildren<Camera>();
        SetCurrentMagic(_typeMagicAttack);
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
            _destination = ray.GetPoint(1000);
        }

        InstantiateProjectile();
    }

    private void InstantiateProjectile()
    {
        var projectileObj = Instantiate(_currentMagicAttack.Bullet, _firepoint.position, Quaternion.identity);
        projectileObj.GetComponent<Rigidbody>().velocity = 
            (_destination - _firepoint.position).normalized * _currentMagicAttack.Speed;
        
        iTween.PunchPosition(projectileObj, new Vector3(
                Random.Range(-_arcRange, _arcRange), 
                Random.Range(-_arcRange, _arcRange), 0), 
            Random.Range(0.5f, 2));
    }
}
