using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Abstract_classes;
using DefaultNamespace.Events;
using UnityEngine;

namespace DefaultNamespace.Interactable
{
    public class TakenObject : global::Interactable
    {
        [SerializeField] private string _name;
        protected override void Interact()
        {
            Debug.Log(_name);
            GameEventManager.Instance.ItemEvent.ItemCollected(_name);
            Destroy(gameObject);
        }
    }
}