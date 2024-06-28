using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleCountsChanger : MonoBehaviour
{
    void OnEnable()
    {
        Debug.Log("OnEnable");
        var text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = "Battle wins: " + GameStatsManager.Instance.CountOfBattles;
    }
    
}
