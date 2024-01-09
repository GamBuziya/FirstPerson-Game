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
        }
        
    }
}