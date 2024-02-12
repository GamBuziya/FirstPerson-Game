using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool _isFinished = false;

    protected void FinishQuestStep()
    {
        if (!_isFinished)
        {
            _isFinished = true;
            
            // TODO - advance the quest forward mow that we have finished this step
            Destroy(this.gameObject);
        }
    }
}
