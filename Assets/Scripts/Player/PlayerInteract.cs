using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Abstract_classes;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _mask;
    private Camera _cam;
    private PlayerUI _ui;
    private InputManager _inputManager;
    
    
    private void Awake()
    {
        _cam = GetComponent<PlayerLook>().GetCam();
        _ui = GetComponent<PlayerUI>();
        _inputManager = GetComponent<InputManager>();
    }

    private void Update()
    {
        _ui.updateText(String.Empty);
        Ray ray = new Ray(_cam.transform.position, _cam.transform.forward);
        
        RaycastHit hitInfo;
        
        if (Physics.Raycast(ray, out hitInfo, _distance, _mask))
        {
            
            Interactable interObject = hitInfo.collider?.GetComponent<Interactable>(); 
            if (interObject != null)
            {
                _ui.updateText(interObject.GetActionName());
                
                if (_inputManager.onFoot.Interact.triggered)
                {
                    if (interObject is ITaken takenObject)
                    {
                        if (takenObject.IsEquipped) return;
                        takenObject.Take(interObject.gameObject);
                    }
                    interObject.BaseInteract();
                }
            }
        }
    }
}
