using System;
using DefaultNamespace.Abstract_classes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class BarDataManager : MonoBehaviour
    {
        [SerializeField] private GameObject _parentGameObjects;
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _text;
        
        public int Price; 
    
        private Image[] _levelGameObjects;
        
        protected void Awake()
        {
            _levelGameObjects = _parentGameObjects.GetComponentsInChildren<Image>();
        }
        
        public void UpdateData(int data)
        {
            Price = 200 + data * 200;
            _text.text = Price.ToString();

            if (Price > GameStatsManager.Instance.Coins)
            {
                _button.interactable = false;
            }
            else
            {
                _button.interactable = true;
            }
            
            for (int i = 0; i < 4; i++)
            {
                if (i < data)
                {
                    _levelGameObjects[i].color = Color.white;
                }
                else
                {
                    _levelGameObjects[i].color = Color.gray;
                }
            }
        }
    }
}