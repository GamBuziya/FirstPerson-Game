using System;
using DefaultNamespace.Enums;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class CoinsTextChanger : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Start()
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
            GameStatsManager.Instance.ChangeStats += UpdateCoinsUI;
            UpdateCoinsUI(DataType.Coins);
        }

        private void UpdateCoinsUI(DataType type)
        {
            _text.text = "Coins: " + GameStatsManager.Instance.Coins.ToString();
        }
    }
}