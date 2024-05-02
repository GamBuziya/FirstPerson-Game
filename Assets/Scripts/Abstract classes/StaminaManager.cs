namespace Abstract_classes
{
    public class StaminaManager
    {
        private float _maxStamina;
        private float _staminaRegenMultiplier;
        private float _staminaAttackSaveMultiplier;

        public StaminaManager(float maxStamina, float staminaMultiplier, float staminaAttackSaveMultiplier)
        {
            _maxStamina = maxStamina;
            _staminaRegenMultiplier = staminaMultiplier;
            _staminaAttackSaveMultiplier = staminaAttackSaveMultiplier;
        }
        
        
        public void StaminaDamage(int damage, ref float stamina)
        {
            stamina -= damage * (1 - _staminaAttackSaveMultiplier);
        }

        public void RegenerateStamina(ref float stamina)
        {
            if (stamina <= _maxStamina)
                stamina += 0.7f * _staminaRegenMultiplier;
        }
    }
}