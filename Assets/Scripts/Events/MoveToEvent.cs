using System;
using UnityEngine;

namespace DefaultNamespace.Events
{
    public class MoveToEvent
    {

        public event Action<Transform, String> onMoveToEvent;
        public void MoveEvent(Transform destination, string characterName)
        {
            if (onMoveToEvent != null)
            {
                onMoveToEvent(destination, characterName);
            }
        }
    }
}