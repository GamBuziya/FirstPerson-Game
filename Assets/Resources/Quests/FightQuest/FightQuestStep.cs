using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Events;
using UnityEngine;

public class FightQuestStep : QuestStep
{
    private void OnEnable()
    {
        Debug.Log("OnEnable");
        GameEventManager.Instance.MoveToEvent.MoveEvent(GameObject.Find("Path").GetComponent<Path>().WayPoints[0], "Piyer");
    }
    
    protected override void SetQuestStepState(string state)
    {
        //this._wheatToCollect = System.Int32.Parse(state);
        //UpdateState();
    }
}
