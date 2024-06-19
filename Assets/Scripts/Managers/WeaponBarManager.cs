using System;
using DefaultNamespace.Abstract_classes;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class WeaponBarManager : BasicBarManager
    {

        protected new void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            Invoke("SetCurrentLevel", 0.3f);
            Invoke("UpdateVisual", 0.3f);
        }

        protected override void SetCurrentLevel()
        {
            Debug.Log(GameStatsManager.Instance.name);
            _currentUpgradeLevel = GameStatsManager.Instance.SelectedWeapon.DamageLevel;
        }

        protected override void UpdateVisual()
        {
            _currentUpgradeLevel = GameStatsManager.Instance.SelectedWeapon.DamageLevel;
            for (int i = 0; i < _currentUpgradeLevel; i++)
            {
                _levelGameObjects[i].color = Color.red;
            }
        }
    }
}