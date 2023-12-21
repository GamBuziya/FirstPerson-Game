using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Abstract_classes;
using UnityEngine;

public class Sword : Interactable, ITaken
{
    private Vector3 _localPosition = new Vector3(0.96f, -0.71f, 1.11f);
    private Quaternion _localQuanterion = Quaternion.Euler(0f, 85.46f, 0f);
    private Vector3 _localScale = new Vector3(1, 1.7f, 1);
    
    [field: SerializeField] public bool IsEquipped { get; set; }

    private void Start()
    {
        if(IsEquipped) BasicCondition = "";
    }

    public void Take(GameObject weaponObject)
    {
        IsEquipped = true;
        BasicCondition = "";
        weaponObject.transform.SetParent(GameObject.Find("WeaponSlot").transform);
        weaponObject.GetComponent<Animator>().enabled = true;
        
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        weaponObject.transform.localPosition = _localPosition;
        weaponObject.transform.localRotation = _localQuanterion;
        weaponObject.transform.localScale = _localScale;
    }

}
