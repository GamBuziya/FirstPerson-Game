using System;
using System.Collections.Generic;
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
            Debug.Log(quest.Info.DisplayName);
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