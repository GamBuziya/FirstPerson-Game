using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFireTut : MonoBehaviour
{
    [SerializeField] private GameObject _impactVFX;
    private bool collided;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Player" && !collided)
        {
            collided = true;

            var impact = Instantiate(_impactVFX, other.contacts[0].point, Quaternion.identity);
            Destroy(impact, 1);
            Destroy(gameObject);
        }
    }
}
