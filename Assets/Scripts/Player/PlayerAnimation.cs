using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Abstract_classes;
using DefaultNamespace.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class PlayerAnimation : AnimatorManager
    {
        
        private void Awake()
        {
            _person = GetComponent<Player>();
            
            var temp = _person.GetComponentsInChildren<Transform>(true);

            foreach (var childTransform in temp)
            {
                var childGameObject = childTransform.gameObject;

                if (childGameObject.CompareTag("Weapon"))
                {
                    WeaponAnimator = childGameObject.GetComponent<Animator>();
                    break; 
                }
            }
        }
        
    }
}