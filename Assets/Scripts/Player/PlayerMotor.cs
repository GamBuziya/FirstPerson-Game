using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private float _jumpHeight = 1f;
    [SerializeField] private int _SpeedupStaminaCost = 20;
    
    
    private CharacterController _characterController;
    private Player _player;
    private Vector3 _velocityVector;
    private Vector3 _worldSpaceInput;
    
    private bool _isGrounded;
    
    void Awake()
    {
        _player = GetComponent<Player>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _isGrounded = _characterController.isGrounded;
    }

    public void UpdateMove(Vector2 inputVector)
    {
        Vector3 temp = Vector3.zero;
        temp.x = inputVector.x;
        temp.z = inputVector.y;

        _worldSpaceInput = transform.TransformDirection(temp);
        _characterController.Move(_worldSpaceInput * (_speed * Time.deltaTime));
        
        _velocityVector.y += _gravity * Time.deltaTime;
        
        if (_isGrounded && _velocityVector.y < 0) _velocityVector.y = -2;
        
        
        _characterController.Move(_velocityVector * Time.deltaTime);
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _velocityVector.y = Mathf.Sqrt(_jumpHeight * -3f * _gravity);
        }
    }

    public void SpeedUp()
    {
        Debug.Log("Run");
        if (_player.CurrentStamina > _SpeedupStaminaCost)
        {
            Invoke("SpeedUpReset", 0.2f);
            _speed *= 3f;
        }
        
    }

    private void SpeedUpReset()
    {
        
        _speed /= 3f;
        _player.StaminaDamage(_SpeedupStaminaCost);
    }
}
