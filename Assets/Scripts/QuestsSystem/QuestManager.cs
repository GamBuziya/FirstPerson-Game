using System;
using System.Collections.Generic;
using DefaultNamespace.Events;
using UnityEngine;

namespace DefaultNamespace.QuestsSystem
{
    public class QuestManager : MonoBehaviour
    {
        private Dictionary<string, Quest> _questMap;

        private void Awake()
        {
            _questMap = CreateQuestMap();

            Quest quest = GetQuestById("CollectWheatQuest");
        }

        private void OnEnable()
        {
            GameEventManager.Instance.QuestEvents.onStartQuest += StartQuest;
            GameEventManager.Instance.QuestEvents.onAdvanceQuest += AdvanceQuest;
            GameEventManager.Instance.QuestEvents.onFinishQuest += FinishQuest;
        }
        
        private void OnDisable()
        {
            GameEventManager.Instance.QuestEvents.onStartQuest -= StartQuest;
            GameEventManager.Instance.QuestEvents.onAdvanceQuest -= AdvanceQuest;
            GameEventManager.Instance.QuestEvents.onFinishQuest -= FinishQuest;
        }

        private void Start()
        {
            foreach (var quest in _questMap.Values)
            {
                GameEventManager.Instance.QuestEvents.QuestStateChange(quest);
            }
        }

        private void Update()
        {
            foreach (var quest in _questMap.Values)
            {
                if (quest.State == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
                {
                    ChangeQuestState(quest.Info.Id, QuestState.CAN_START);
                }
            }
        }

        private void ChangeQuestState(string id, QuestState state)
        {
            Quest quest = GetQuestById(id);

            quest.State = state;
            GameEventManager.Instance.QuestEvents.QuestStateChange(quest);
        }
        
        
        private bool CheckRequirementsMet(Quest quest)
        {
            bool meetsRequirements = true;

            foreach (var prerequisiteQuestInfo in quest.Info.QuestsPrerequisites)
            {
                if (GetQuestById(prerequisiteQuestInfo.Id).State != QuestState.FINISHED)
                {
                    meetsRequirements = false;
                }
            }

            return meetsRequirements;
        }
        
        private void StartQuest(String id)
        {
            Quest quest = GetQuestById(id);
            quest.InstantiateCurrentQuestStep(this.transform);
            ChangeQuestState(quest.Info.Id, QuestState.IN_PROGRESS);
        }

        private void AdvanceQuest(string id)
        {
            Debug.Log("Advance Quest: " + id);
        }

        private void FinishQuest(string id)
        {
            Debug.Log("Finish Quest: " + id);
        }

        private Dictionary<string, Quest> CreateQuestMap()
        {
            // Load all Quests
            QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");

            Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
            foreach (QuestInfoSO questInfo in allQuests)
            {
                if (idToQuestMap.ContainsKey(questInfo.Id))
                {
                    Debug.LogWarning("Duplicate ID ");
                }
                idToQuestMap.Add(questInfo.Id, new Quest(questInfo));
            }

            return idToQuestMap;
        }

        private Quest GetQuestById(String id)
        {
            Quest quest = _questMap[id];
            if (quest == null)
            {
                Debug.LogWarning("error");
            }

            return quest;
        }
    }
}