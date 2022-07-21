using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour{
    
    [SerializeField] float playerSpeed = 2f;
    [SerializeField] float jumpHeight = 1f;
    [SerializeField] float gravityValue = -9.81f;
    [SerializeField] float rotationSpeed = 10f;
    
    CharacterController _controller;
    PlayerInput _playerInput;
    Vector3 _playerVelocity;
    bool groundedPlayer;
    Transform _camTransform;

    InputAction _moveAction;
    InputAction _lookAction;
    InputAction _jumpAction;
    

    void Start(){
        _controller = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _camTransform = Camera.main.transform;
        _moveAction = _playerInput.actions["Move"];
        _jumpAction = _playerInput.actions["Jump"];
    }

    void Update(){
        
        // Checking if player is grounded
        groundedPlayer = _controller.isGrounded;
        if (groundedPlayer && _playerVelocity.y < 0){
            _playerVelocity.y = 0f;
        }

        Vector2 input = _moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * _camTransform.right.normalized + move.z * _camTransform.forward.normalized;
        move.y = 0f;
        _controller.Move(move * (Time.deltaTime * playerSpeed));
        

        // Changes the height position of the player
        if (_jumpAction.triggered && groundedPlayer){
            _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3f * gravityValue);
        }

        _playerVelocity.y += gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
        
        // Rotate towards camera direction.
        Quaternion targetRotation = Quaternion.Euler(0,_camTransform.eulerAngles.y,0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
