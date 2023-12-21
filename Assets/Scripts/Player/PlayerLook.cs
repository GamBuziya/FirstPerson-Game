using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] public Camera _camera;
    [SerializeField] private float _sensivity;
    
    private float _xRotation = 0f;
    private float _mouseX;
    private float _mouseY;
    
    public void UpdateLook(Vector2 input)
    {
        
        _mouseX = input.x;
        _mouseY = input.y;
        
        _xRotation -= (_mouseY * Time.deltaTime) * _sensivity;
        _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);
        
        _camera.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        
        transform.Rotate(Vector3.up * (_mouseX * Time.deltaTime * _sensivity));

    }
    

    public Camera GetCam()
    {
        return _camera;
    }
}
