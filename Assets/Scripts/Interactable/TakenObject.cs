using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Abstract_classes;
using UnityEngine;

namespace DefaultNamespace.Interactable
{
    public class TakenObject : global::Interactable
    {
        [SerializeField] private string _name;
        protected override void Interact()
        {
            Debug.Log(_name);
            EventManager.Instance.ItemCollected(_name);
            Destroy(gameObject);
        }
    }
}