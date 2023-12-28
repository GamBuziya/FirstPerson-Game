using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;

    public PlayerInput.OnFootActions onFoot;
    private PlayerMotor _motor;
    private PlayerLook _look;
    private PlayerAnimation _playerAnimation;
    
    void Awake()
    {
        _playerInput = new PlayerInput();
        onFoot = _playerInput.OnFoot;
        
        _motor = GetComponent<PlayerMotor>();
        _look = GetComponent<PlayerLook>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        
        //Підв'язка методу до дії
        onFoot.Jump.performed += context =>  _motor.Jump();
        onFoot.SpeedUp.performed += context =>  _motor.SpeedUp();
        
        
        //Удари
        onFoot.PowerButton.started += context => _playerAnimation.ChangePower();
        onFoot.PowerButton.canceled += context => _playerAnimation.ChangePower();
        
        onFoot.LMK.performed += context => _playerAnimation.Attack();
        
        onFoot.RMK.performed += context => _playerAnimation.Block();
        
    }

    private void FixedUpdate()
    {
        _motor.UpdateMove(_playerInput.OnFoot.Movement.ReadValue<Vector2>());
    }
    
    private void LateUpdate()
    {
        _look.UpdateLook(_playerInput.OnFoot.Look.ReadValue<Vector2>());
    }

    private void Update()
    {
        _playerAnimation.UpdateSide(_playerInput.OnFoot.Look.ReadValue<Vector2>());
    }


    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
