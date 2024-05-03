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
    private Player _player;
    
    void Awake()
    {
        _playerInput = new PlayerInput();
        onFoot = _playerInput.OnFoot;
        
        _motor = GetComponent<PlayerMotor>();
        _look = GetComponent<PlayerLook>();
        _player = GetComponent<Player>();
        
        //Підв'язка методу до дії
        onFoot.Jump.performed += context =>  _motor.Jump();
        onFoot.SpeedUp.performed += context =>  _motor.SpeedUp();

        onFoot.Interact.performed += context => EventManager.Instance.Interacted();
        //Удари
        onFoot.PowerButton.started += context => _player.GetBattleController().ChangeForce();
        onFoot.PowerButton.canceled += context => _player.GetBattleController().ChangeForce();
        
        onFoot.LMK.performed += context => _player.GetBattleController().Attack();
        
        onFoot.RMK.started += context =>
        {
            if (_player.GetBattleController() is PlayerBattleController playerBattleController)
            {
                playerBattleController.Block();
            }
        };
        onFoot.RMK.canceled += context =>
        {
            if (_player.GetBattleController() is PlayerBattleController playerBattleController)
            {
                playerBattleController.ResetBlock();
            }
        };
        
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
        if (_player.GetBattleController() is PlayerBattleController playerBattleController)
        {
            playerBattleController.UpdateMousePosition(_playerInput.OnFoot.Look.ReadValue<Vector2>());
        }
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
