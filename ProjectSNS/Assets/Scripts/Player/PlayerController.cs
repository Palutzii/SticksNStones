using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float playerSpeed = 2f;
    [SerializeField] float jumpHeight = 1f;
    [SerializeField] float gravityValue = -9.81f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] Transform firePointParent;
    Transform _camTransform;
    CharacterController _controller;
    InputAction _jumpAction;
    InputAction _lookAction;

    InputAction _moveAction;
    PlayerInput _playerInput;

    Vector3 _playerVelocity;
    InputAction _shootAction;

    bool groundedPlayer;


    void Awake(){
        _controller = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _camTransform = Camera.main.transform;
        _moveAction = _playerInput.actions["Move"];
        _jumpAction = _playerInput.actions["Jump"];
        _shootAction = _playerInput.actions["Shoot"];
    }


    void Update(){
        // Checking if player is grounded
        groundedPlayer = _controller.isGrounded;
        if (groundedPlayer && _playerVelocity.y < 0) _playerVelocity.y = 0f;

        var input = _moveAction.ReadValue<Vector2>();
        var move = new Vector3(input.x, 0, input.y);
        move = move.x * _camTransform.right.normalized + move.z * _camTransform.forward.normalized;
        move.y = 0f;
        _controller.Move(move * (Time.deltaTime * playerSpeed));


        // Changes the height position of the player
        if (_jumpAction.triggered && groundedPlayer) _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3f * gravityValue);

        _playerVelocity.y += gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);

        // Rotate towards camera direction.
        var targetRotation = Quaternion.Euler(0, _camTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void OnEnable(){
        _shootAction.performed += _ => ShootBow();
    }

    void OnDisable(){
        _shootAction.performed -= _ => ShootBow();
    }

    void ShootBow(){
        RaycastHit hit;
        if (Physics.Raycast(_camTransform.position, _camTransform.forward, out hit, Mathf.Infinity)){
            var arrow =
                Instantiate(arrowPrefab, firePoint.position, Quaternion.identity, firePointParent);
        }
    }
}