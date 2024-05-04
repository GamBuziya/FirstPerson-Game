using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSphereManager : MonoBehaviour
{
    [SerializeField] private MagicAttackSO _object;
    private bool collided;
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player") && !collided)
        {
            collided = true;
            SoundManager.Instance.MagicAttackSound(_object.TypeMagic, 0.05f);
            var impact = Instantiate(_object.Impact, other.contacts[0].point, Quaternion.identity);
            Destroy(impact, 1);
            Destroy(gameObject);
        }
    }
}
