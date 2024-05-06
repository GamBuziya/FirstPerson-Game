﻿using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace Abstract_classes
{
    public class UiMagicCharacterController : UIGameCharacterController
    {
        [Header("Magic Bar")]
        [SerializeField] protected Image _frontMagic;
        [SerializeField] protected Image _backMagic;
        
        protected int _maxMagic;

        private MagicGameCharacter _magicCharacter;
        
        protected new void Start()
        {
            base.Start();
            if (TryGetComponent(out MagicGameCharacter magicCharacter))
            {
                _magicCharacter = magicCharacter;
            }
            else
            {
                Debug.LogWarning("MagicGameCharacter doesn`t exist");
            }
        }

        protected new void LateUpdate()
        {
            base.LateUpdate();
            UpdateMagicUI();
        }
        
        protected void UpdateMagicUI()
        {
            float fillB = _backMagic.fillAmount;
            float certainMagic = _magicCharacter.GetCurrentMagic() / _magicCharacter.GetMaxMagic();
        
            if (certainMagic < fillB)
            {
                _backMagic.color = Color.red;
                _frontMagic.fillAmount = certainMagic;
                _backMagic.fillAmount = Mathf.Lerp(fillB, certainMagic, Time.deltaTime);
                return;
            }
            
            _frontMagic.fillAmount = Mathf.Lerp(_frontMagic.fillAmount, certainMagic, Time.deltaTime*5);
            _backMagic.fillAmount = Mathf.Lerp(fillB, certainMagic, Time.deltaTime*5);
        }
    }
}