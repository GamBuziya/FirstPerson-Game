using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.DialogSystem;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private List<DialogString> _dialogStrings;
    [SerializeField] private Transform _npcTransform;

    private bool _hasSpoken = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_hasSpoken)
        {
            other.gameObject.GetComponent<DialogManager>().DialogStart(_dialogStrings, _npcTransform);
            _hasSpoken = true;
        }
    }
}
