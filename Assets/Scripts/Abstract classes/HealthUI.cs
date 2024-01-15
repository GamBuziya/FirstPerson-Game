using DefaultNamespace;
using DefaultNamespace.Abstract_classes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;

namespace DefaultNamespace.Abstract_classes
{
    public abstract class HealthUI : MonoBehaviour
    {
        [Header("Health Bar")]
        [SerializeField] protected Image _frontHealth;
        [SerializeField] protected Image _backHealth;
        
        protected GameCharacter _hero;
        protected float _currentHealth;
        
        
        protected void UpdateHealthUI()
        {
            float fillB = _backHealth.fillAmount;
            float fillA = _frontHealth.fillAmount;

            float certainHealth = _currentHealth / _hero.GetHealthPoints().GetMaxHealth();
        
            if (certainHealth < fillB)
            {
                _backHealth.color = Color.red;
                _frontHealth.fillAmount = certainHealth;
                _backHealth.fillAmount = Mathf.Lerp(fillB, certainHealth, Time.deltaTime);
            }

            if (certainHealth > fillA)
            {
                _backHealth.color = Color.green;
                _backHealth.fillAmount = certainHealth;
                _frontHealth.fillAmount = Mathf.Lerp(fillA, certainHealth, Time.deltaTime);
            }
        }
    }
}