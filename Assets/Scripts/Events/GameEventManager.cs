using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Events
{
    public class GameEventManager : MonoBehaviour
    {
        
        public static GameEventManager Instance { get; private set; }
        public QuestEvents QuestEvents;
        public InputEvents InputEvents;
        public CollectItemEvent ItemEvent;


        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("More than one Game Event Manager");
            }

            Instance = this;
            ItemEvent = new CollectItemEvent();
            InputEvents = new InputEvents();
            QuestEvents = new QuestEvents();
        }
    }
}