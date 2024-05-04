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
                GameObject bloodEffectInstance = Instantiate(_BloodEffect, collisionPoint, Quaternion.identity);
                
                Vector3 direction = -bloodEffectInstance.transform.forward; 
                bloodEffectInstance.transform.forward = direction;
                
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