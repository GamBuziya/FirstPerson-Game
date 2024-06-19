using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

namespace DefaultNamespace.Abstract_classes
{
    public abstract class BasicBarManager : MonoBehaviour
    {
        [SerializeField] protected GameObject _parentGameObjects;
    
        protected Image[] _levelGameObjects;
        protected int _currentUpgradeLevel = 0;
        protected void Awake()
        {
            _levelGameObjects = _parentGameObjects.GetComponentsInChildren<Image>();
        }

        protected abstract void SetCurrentLevel();

        protected abstract void UpdateVisual();

    }
}