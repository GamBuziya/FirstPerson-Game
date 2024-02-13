using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Events;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool _isFinished = false;
    private string _questId;

    public void IntializeQuestStep(string questId)
    {
        _questId = questId;
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
}
