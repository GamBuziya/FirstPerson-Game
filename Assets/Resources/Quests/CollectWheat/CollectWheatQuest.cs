using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectWheatQuest : QuestStep
{
    private int _wheatCollected = 0;
    private int _wheatToComplete = 10;

    private void OnEnable()
    {
        //Зробити підпис на кожен раз коли ми збираємо пшеницю == WheatCollected
    }

    private void OnDisable()
    {
        //Відписатись від слідкування
    }

    private void WheatCollected()
    {
        if (_wheatCollected < _wheatToComplete)
        {
            _wheatCollected++;
        }
        else
        {
            FinishQuestStep();
        }
    }
}
