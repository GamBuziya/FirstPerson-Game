using System;
using System.Collections.Generic;
using DefaultNamespace.Events;
using UnityEngine;

namespace DefaultNamespace.QuestsSystem
{
    public class QuestManager : MonoBehaviour
    {
        [Header("Config")] [SerializeField] private bool LoadQuestState = true;
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

            GameEventManager.Instance.QuestEvents.onQuestStepStateChange += QuestStepStateChange;
        }
        
        private void OnDisable()
        {
            GameEventManager.Instance.QuestEvents.onStartQuest -= StartQuest;
            GameEventManager.Instance.QuestEvents.onAdvanceQuest -= AdvanceQuest;
            GameEventManager.Instance.QuestEvents.onFinishQuest -= FinishQuest;
            
            GameEventManager.Instance.QuestEvents.onQuestStepStateChange -= QuestStepStateChange;
        }

        private void Start()
        {
            foreach (var quest in _questMap.Values)
            {
                //Initialize any loaded quest steps
                if (quest.State == QuestState.IN_PROGRESS)
                {
                    quest.InstantiateCurrentQuestStep(transform);
                }
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
            Quest quest = GetQuestById(id);
            
            quest.MoveToNextStep();

            if (quest.CurrentStepExists())
            {
                quest.InstantiateCurrentQuestStep(this.transform);
            }
            //Якщо закінчили
            else
            {
                ChangeQuestState(quest.Info.Id, QuestState.CAN_FINISH);
            }
        }

        private void FinishQuest(string id)
        {
            Quest quest = GetQuestById(id);
            ChangeQuestState(quest.Info.Id, QuestState.FINISHED);
            //Щось ще для запуску після закінчення квесту
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
                idToQuestMap.Add(questInfo.Id, LoadQuest(questInfo));
            }

            return idToQuestMap;
        }

        public Quest GetQuestById(String id)
        {
            Quest quest = _questMap[id];
            if (quest == null)
            {
                Debug.LogWarning("error");
            }

            return quest;
        }

        private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
        {
            Quest quest = GetQuestById(id);
            quest.StoreQuestStepState(questStepState, stepIndex);
            ChangeQuestState(id, quest.State);
        }

        private void OnApplicationQuit()
        {
            foreach (var quest in _questMap.Values)
            {
                SaveQuest(quest);
            }
        }

        private void SaveQuest(Quest quest)
        {
            try
            {
                QuestData questData = quest.GetQuestData();
                //Може треба JsonUtility
                string serializedData = JsonUtility.ToJson(questData);
                PlayerPrefs.SetString(quest.Info.Id, serializedData);
                
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to save quest with id " + quest.Info.Id + " " + e);
            }
        }

        private Quest LoadQuest(QuestInfoSO questInfoSo)
        {
            Quest quest = null;

            try
            {
                if (PlayerPrefs.HasKey(questInfoSo.Id) && LoadQuestState)
                {
                    string serializedData = PlayerPrefs.GetString(questInfoSo.Id);
                    QuestData questData = JsonUtility.FromJson<QuestData>(serializedData);
                    quest = new Quest(questInfoSo, questData.State, questData.QuestStepIndex,
                        questData.QuestStepStates);
                }
                else
                {
                    quest = new Quest(questInfoSo);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return quest;
        }
    }
}