using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    private bool _isOpened = false;
    [SerializeField] private GameObject _object;
    protected override void Interact()
    {
        _isOpened = !_isOpened;
        _object.GetComponent<Animator>().SetBool("IsOpened", _isOpened);
    }
}
