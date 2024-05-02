using System;
using DefaultNamespace.Abstract_classes;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class UIGameCharacterController : MonoBehaviour
    {
        /*[Header("Health Bar")]
        [SerializeField] protected Image _frontHealth;
        [SerializeField] protected Image _backHealth;*/
        
        [Header("Stamina Bar")]
        [SerializeField] private Image _frontStamina;
        [SerializeField] private Image _backStamina;

        private float _maxStamina;
        private float _currentStamina;

        private int _maxHealth;
        private int _currentHealth;

        private void Start()
        {
            _maxHealth = GetComponent<GameCharacter>().CurrentHealth;
            _maxStamina = GetComponent<GameCharacter>().CurrentStamina;
        }

        private void Update()
        {
            _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);
        }

        private void LateUpdate()
        {
            UpdateStaminaUI();
            //UpdateHealthUI();
        }

        private void UpdateStaminaUI()
        {
            float fillB = _backStamina.fillAmount;

            float certainStamina = _currentStamina / _maxStamina;
        
            if (certainStamina < fillB)
            {
                _backStamina.color = Color.red;
                _frontStamina.fillAmount = certainStamina;
                _backStamina.fillAmount = Mathf.Lerp(fillB, certainStamina, Time.deltaTime);
                return;
            }
            
            _frontStamina.fillAmount = Mathf.Lerp(_frontStamina.fillAmount, certainStamina, Time.deltaTime*5);
            _backStamina.fillAmount = Mathf.Lerp(fillB, certainStamina, Time.deltaTime*5);
        }
        
        /*protected void UpdateHealthUI()
        {
            float fillB = _backHealth.fillAmount;
            float fillA = _frontHealth.fillAmount;

            float certainHealth = _currentHealth / _maxHealth;
        
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
        }*/
    }
}