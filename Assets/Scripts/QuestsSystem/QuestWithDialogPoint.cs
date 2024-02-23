using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.DialogSystem;
using DefaultNamespace.Events;
using DefaultNamespace.QuestsSystem;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class QuestWithDialogPoint : MonoBehaviour
{
    [Header("Quest")] [SerializeField] private QuestInfoSO _questSo;

    [Header("Config")] 
    [SerializeField] private bool _startPoint = true;

    [SerializeField] private bool _finishPoint = true;

    [SerializeField] private Transform _NPC;
    
    private bool _playerIsNear = false;
    private string _questId;
    private QuestState _currentQuestState;
    private DialogManager _dialogManager;
    private QuestManager _questManager;
    private Quest _quest;


    private void Awake()
    {
        _dialogManager = GameObject.Find("Player").GetComponent<DialogManager>();
    }

    private void Start()
    {
        _questId = _questSo.Id;
        _quest = GameObject.Find("Managers").GetComponentInChildren<QuestManager>().GetQuestById(_questId);
    }

    private void OnEnable()
    {
        GameEventManager.Instance.QuestEvents.onQuestStateChange += QuestStateChange;
        GameEventManager.Instance.InputEvents.onSubmitPressed += SubmitPressed;
        GameEventManager.Instance.InputEvents.onInteract += PlayDialog;
    }
    
    private void OnDisable()
    {
        GameEventManager.Instance.QuestEvents.onQuestStateChange -= QuestStateChange;
        GameEventManager.Instance.InputEvents.onSubmitPressed -= SubmitPressed;
        GameEventManager.Instance.InputEvents.onInteract -= PlayDialog;
    }

    private void SubmitPressed()
    {
        
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

    private void PlayDialog()
    {
        if (!_playerIsNear) return;
        var currentdialogs = _quest.GetCurrentDialogsSo();
        switch (_currentQuestState)
        {
            case QuestState.CAN_START:
                _dialogManager.DialogStart(currentdialogs.StartDialog, _NPC);
                break;
            case QuestState.IN_PROGRESS:
                _dialogManager.DialogStart(currentdialogs.RepeatQuest, _NPC);
                break;
            case QuestState.CAN_FINISH:
                _dialogManager.DialogStart(currentdialogs.EndDialog, _NPC);
                break;
        }
    }
}
