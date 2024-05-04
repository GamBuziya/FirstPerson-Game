using System;
using DefaultNamespace.Abstract_classes;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class UIGameCharacterController : MonoBehaviour
    {
        [Header("Health Bar")]
        [SerializeField] protected Image _frontHealth;
        [SerializeField] protected Image _backHealth;
        
        [Header("Stamina Bar")]
        [SerializeField] protected Image _frontStamina;
        [SerializeField] protected Image _backStamina;
        
        

        protected float _maxStamina;
        protected int _maxHealth;

        protected GameCharacter _gameCharacter;

        protected void Start()
        {
            _gameCharacter = GetComponent<GameCharacter>();
            _maxHealth = _gameCharacter.GetCurrentHealth();
            _maxStamina = _gameCharacter.GetCurrentStamina();
            
        }

        protected void Update()
        {
            _gameCharacter.SetCurrentStamina(Mathf.Clamp(_gameCharacter.GetCurrentStamina(), 0, _maxStamina));
        }

        protected void LateUpdate()
        {
            UpdateStaminaUI();
            UpdateHealthUI();
        }

        private void UpdateStaminaUI()
        {
            float fillB = _backStamina.fillAmount;

            float certainStamina = _gameCharacter.GetCurrentStamina() / _maxStamina;
        
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
        
        protected void UpdateHealthUI()
        {
            float fillB = _backHealth.fillAmount;
            float fillA = _frontHealth.fillAmount;
            
            float certainHealth = (float)_gameCharacter.GetCurrentHealth() / _maxHealth;
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