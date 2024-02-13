using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Events;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool _isFinished = false;
    private string _questId;
    private int _stepIndex;
    

    public void IntializeQuestStep(string questId, int stepIndex, string questStepState)
    {
        _questId = questId;
        _stepIndex = stepIndex;
        if (!string.IsNullOrEmpty(questStepState))
        {
            SetQuestStepState(questStepState);
        }
    }

    protected void FinishQuestStep()
    {
        if (!_isFinished)
        {
            _isFinished = true;
            GameEventManager.Instance.QuestEvents.AdvanceQuest(_questId);
            Destroy(this.gameObject);
        }
    }

    protected void ChangeState(string newState)
    {
        GameEventManager.Instance.QuestEvents.QuestStepStateChange(_questId, _stepIndex, new QuestStepState(newState));
    }

    protected abstract void SetQuestStepState(string state);
}
