using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "ScriptableObject/QuestInfoSO ", order = 1)]
public class QuestInfoSO : ScriptableObject
{
    [field: SerializeField] public string Id { get; private set; }

    [Header("General")] 
    public string DisplayName;

    [Header("Requirements")] public QuestInfoSO[] QuestsPrerequisites;
    
    [Header("Steps")] public GameObject[] QuestStepPrefabs;

    private void OnValidate()
    {
        #if UNITY_EDITOR
        Id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
