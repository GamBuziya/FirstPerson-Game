using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Events;
using UnityEngine;

public class FindSticksQuestStep : QuestStep
{
    private int _stickToCollect = 0;
    private int _stickToComplete = 3;
    private bool _potionIsFind = false;

    private void OnEnable()
    {
        GameEventManager.Instance.ItemEvent.onItemCollected += StickCollected;
    }

    private void OnDisable()
    {
        GameEventManager.Instance.ItemEvent.onItemCollected -= StickCollected;
    }

    private void StickCollected(string name)
    {
        if (name.Equals("Stick"))
        {
            if (_stickToCollect <= _stickToComplete)
            {
                _stickToCollect++;
                UpdateState();
            }
        }
        else if(name.Equals("Potion"))
        {
            _potionIsFind = true;
        }
        
        if(_potionIsFind && _stickToCollect >= _stickToComplete) FinishQuestStep();
        
    }

    private void UpdateState()
    {
        string state = _stickToCollect.ToString();
        ChangeState(state);
    }

    protected override void SetQuestStepState(string state)
    {
        this._stickToCollect = System.Int32.Parse(state);
        UpdateState();
    }
}
