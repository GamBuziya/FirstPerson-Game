using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : Interactable
{
    private bool _isActive = true;
    protected override void Interact()
    {
        _isActive = !_isActive;
        foreach (Transform child in transform)
            child.gameObject.SetActive(_isActive);
    }
}
