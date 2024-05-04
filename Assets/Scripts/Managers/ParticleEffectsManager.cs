using System;
using UnityEngine;

namespace Managers
{
    public class ParticleEffectsManager : MonoBehaviour
    {
        public static ParticleEffectsManager Instance;
        
        [SerializeField] private GameObject _BloodEffect;

        private void Awake()
        {
            Instance = this;
        }

        public void CreateBloodEffect(Vector3 collisionPoint)
        {
            if (_BloodEffect != null)
            {
                // Створюємо інстанс ефекту крові в місці зіткнення
                GameObject bloodEffectInstance = Instantiate(_BloodEffect, collisionPoint, Quaternion.identity);

                // Налаштовуємо напрямок ефекту крові від точки зіткнення
                Vector3 direction = -bloodEffectInstance.transform.forward; // Напрямок вздовж власної осі Z ефекту
                bloodEffectInstance.transform.forward = direction; // Встановлюємо напрямок ефекту

                // Опціонально: можна додати інші налаштування частинок крові тут

                // Активуємо ефект крові
                bloodEffectInstance.SetActive(true);
                Destroy(bloodEffectInstance, 3f);
            }
            else
            {
                Debug.LogWarning("Blood effect prefab is not assigned to ParticleEffectsManager.");
            }
        } 
    }
}