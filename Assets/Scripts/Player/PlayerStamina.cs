using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


namespace DefaultNamespace 
{
    public class PlayerStamina : MonoBehaviour
    {
        public float Stamina;

        [SerializeField] private float _maxStamina;
        [SerializeField] private Image _frontStamina;
        [SerializeField] private Image _backStamina;
        

        private void Start()
        {
            Stamina = _maxStamina;
            StartCoroutine(TakeStamina());
        }

        private void Update()
        {
            Stamina = Mathf.Clamp(Stamina, 0, _maxStamina);
            UpdateStaminaUI();

            if (Input.GetKeyDown(KeyCode.A))
            {
                StaminaDamage(Random.Range(0, 20));
            }
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

        public void StaminaDamage(int damage)
        {
            Stamina -= damage;
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
    }
}