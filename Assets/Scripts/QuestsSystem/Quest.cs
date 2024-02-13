using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestInfoSO Info;

    public QuestState State;

    private int _currentQuestStepIndex;

    private QuestStepState[] _questStepStates;
    public Quest(QuestInfoSO questInfo)
    {
        this.Info = questInfo;
        this.State = QuestState.REQUIREMENTS_NOT_MET;
        this._currentQuestStepIndex = 0;
        this._questStepStates = new QuestStepState[Info.QuestStepPrefabs.Length];

        for (int i = 0; i < _questStepStates.Length; i++)
        {
            _questStepStates[i] = new QuestStepState();
        }
    }

    public Quest(QuestInfoSO questInfo, QuestState questState, int currentQuestStepIndex,
        QuestStepState[] questStepStates)
    {
        Info = questInfo;
        State = questState;
        _currentQuestStepIndex = currentQuestStepIndex;
        _questStepStates = questStepStates;

        if (questStepStates.Length != Info.QuestStepPrefabs.Length)
        {
            Debug.LogWarning("questStepStates and QuestStepPrefabs are of different length");
        }
    }

    public void MoveToNextStep()
    {
        _currentQuestStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return (_currentQuestStepIndex < Info.QuestStepPrefabs.Length);
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();
        if (questStepPrefab != null)
        {
            QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parentTransform)
                .GetComponent<QuestStep>();
            questStep.IntializeQuestStep(Info.Id, _currentQuestStepIndex, _questStepStates[_currentQuestStepIndex].State);
        }
    }

    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;
        if (CurrentStepExists())
        {
            questStepPrefab = Info.QuestStepPrefabs[_currentQuestStepIndex];
        }
        else
        {
            Debug.LogWarning("Tried to get quest step prefab, but stepindex was out of range indicating that");
        }

        return questStepPrefab;
    }

    public void StoreQuestStepState(QuestStepState questStepState, int stepIndex)
    {
        if (stepIndex < _questStepStates.Length)
        {
            _questStepStates[stepIndex].State = questStepState.State;
        }
        else
        {
            Debug.LogWarning("StoreQuestStepState");
        }
    }

    public QuestData GetQuestData()
    {
        return new QuestData(State, _currentQuestStepIndex, _questStepStates);
    }
}
