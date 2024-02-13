using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Events;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class QuestPoint : MonoBehaviour
{
    [Header("Quest")] [SerializeField] private QuestInfoSO _questInfoForPoint;

    [Header("Config")] 
    [SerializeField] private bool _startPoint = true;

    [SerializeField] private bool _finishPoint = true;
    
    private bool _playerIsNear = false;
    private string _questId;
    private QuestState _currentQuestState;


    private void Awake()
    {
        _questId = _questInfoForPoint.Id;
    }

    private void OnEnable()
    {
        GameEventManager.Instance.QuestEvents.onQuestStateChange += QuestStateChange;
        GameEventManager.Instance.InputEvents.onSubmitPressed += SubmitPressed;
    }
    
    private void OnDisable()
    {
        GameEventManager.Instance.QuestEvents.onQuestStateChange -= QuestStateChange;
        GameEventManager.Instance.InputEvents.onSubmitPressed -= SubmitPressed;
    }

    private void SubmitPressed()
    {
        //ТУТ ЛОГІКА АКТИВАЦІЇ КВЕСТУ
        if (!_playerIsNear) return;
        
        Debug.Log("SubmitPressed" + _currentQuestState);
        if (_currentQuestState.Equals(QuestState.CAN_START) && _startPoint)
        {
            GameEventManager.Instance.QuestEvents.StartQuest(_questId);
        }
        else if(_currentQuestState.Equals(QuestState.CAN_FINISH) && _finishPoint)
        {
            GameEventManager.Instance.QuestEvents.FinishQuest(_questId);
        }
        
    }


    private void QuestStateChange(Quest quest)
    {
        if (quest.Info.Id.Equals(_questId))
        {
            _currentQuestState = quest.State;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsNear = false;
        }
    }
}
