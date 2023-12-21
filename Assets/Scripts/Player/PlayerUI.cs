using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _promptText;

    [Header("Health Bar")]
    [SerializeField] private Image _frontHealth;
    [SerializeField] private Image _backHealth;

    [Header("Damaged Effect")] 
    [SerializeField] private Image _hitEffect;
    [SerializeField] private float _fullADuration;
    [SerializeField] private float _recoverSpeed;
    
    public float _durationTimer = 0;

    private PlayerHealthController _player;
    private float _currentHealth;


    private void Start()
    {
        _player = GetComponent<PlayerHealthController>();
        _currentHealth = _player._playerHealth.Health;
        _hitEffect.color = new Color(_hitEffect.color.r, _hitEffect.color.g, _hitEffect.color.b, 0);
    }
    
    private void Update()
    {
        _currentHealth = _player._playerHealth.Health;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _player._playerHealth.MaxHealth);
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
            
        }
    }
    
    private void UpdateHealthUI()
    {
        float fillB = _backHealth.fillAmount;
        float fillA = _frontHealth.fillAmount;

        float certainHealth = _currentHealth / _player._playerHealth.MaxHealth;
        
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
