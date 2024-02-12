using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Abstract_classes;
using DefaultNamespace.Events;
using UnityEngine;

namespace DefaultNamespace.Interactable
{
    public class Seeds : global::Interactable
    {
        protected override void Interact()
        {
            GameEventManager.Instance.ItemEvent.ItemCollected("Wheat");
            Destroy(gameObject);
        }
    }
}