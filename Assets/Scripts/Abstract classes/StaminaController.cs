using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace DefaultNamespace.Abstract_classes
{
    public abstract class StaminaController : MonoBehaviour
    {
        [SerializeField] private float _maxStamina;
        [SerializeField] private Image _frontStamina;
        [SerializeField] private Image _backStamina;
        
        public float Stamina;

        private void Start()
        {
            StartCoroutine(TakeStamina());
        }

        private void Update()
        {
            Stamina = Mathf.Clamp(Stamina, 0, _maxStamina);
            UpdateStaminaUI();
        }
        
        private void UpdateStaminaUI()
        {
            float fillB = _backStamina.fillAmount;

            float certainStamina = Stamina / _maxStamina;
        
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
        
        private IEnumerator TakeStamina()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                if (Stamina <= _maxStamina)
                    Stamina += 0.5f;
            }
        }
        
        public void StaminaDamage(int damage)
        {
            Stamina -= damage;
        }
    }
}