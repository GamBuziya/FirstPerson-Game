using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Abstract_classes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _promptText;
    
    [Header("Damaged Effect")] 
    [SerializeField] private Image _hitEffect;
    [SerializeField] private float _fullADuration;
    [SerializeField] private float _recoverSpeed;
    
    public float _durationTimer = 0;

    private void Start()
    {
        //_hero = GetComponent<Player>();
        //_currentHealth = _hero.GetCurrentHealth();
        _hitEffect.color = new Color(_hitEffect.color.r, _hitEffect.color.g, _hitEffect.color.b, 0);
    }
    
    private void Update()
    {
        /*_currentHealth = _hero.GetCurrentHealth();
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _hero.GetMaxHealth());
        UpdateHealthUI();
        
        if (_hitEffect.color.a > 0)
        {
            if(_currentHealth < 30) return;
            _durationTimer += Time.deltaTime;
            if (_durationTimer > _fullADuration)
            {
                var temp = _hitEffect.color.a;
                temp -= Time.deltaTime * _recoverSpeed;
                _hitEffect.color = new Color(_hitEffect.color.r, _hitEffect.color.g, _hitEffect.color.b, temp);
            }
            
        }*/
    }

    public void TakeDamageEffect()
    {
        _durationTimer = 0;
        _hitEffect.color = new Color(_hitEffect.color.r, _hitEffect.color.g, _hitEffect.color.b, 0.2f);
    }
    
    public void updateText(string promptText)
    {
        _promptText.text = promptText;
    }
}
