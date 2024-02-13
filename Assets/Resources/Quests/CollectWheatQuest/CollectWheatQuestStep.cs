using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Events;
using UnityEngine;

public class CollectWheatQuestStep : QuestStep
{
    private int _wheatToCollect = 0;
    private int _wheatToComplete = 10;

    private void OnEnable()
    {
        GameEventManager.Instance.ItemEvent.onItemCollected += WheatCollected;
    }

    private void OnDisable()
    {
        GameEventManager.Instance.ItemEvent.onItemCollected -= WheatCollected;
    }

    private void WheatCollected(string name)
    {
        if (!name.Equals("Wheat"))
        {
            Debug.Log("ISnt wheat");
            return;
        }
        if (_wheatToCollect <= _wheatToComplete)
        {
            _wheatToCollect++;
            UpdateState();
        }
        else
        {
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string state = _wheatToCollect.ToString();
        ChangeState(state);
    }

    protected override void SetQuestStepState(string state)
    {
        this._wheatToCollect = System.Int32.Parse(state);
        UpdateState();
    }
}
