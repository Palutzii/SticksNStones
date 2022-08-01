using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchCam : MonoBehaviour
{
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] int priorityAmount = 10;
    [SerializeField] Canvas thirdPersonCanvas;
    [SerializeField] Canvas aimCanvas;
    InputAction _aimAction;

    CinemachineVirtualCamera _virtualCamera;

    void Awake(){
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _aimAction = _playerInput.actions["Aim"];
    }

    void OnEnable(){
        _aimAction.performed += _ => StartAim();
        _aimAction.canceled += _ => StopAim();
    }

    void OnDisable(){
        _aimAction.performed -= _ => StartAim();
        _aimAction.canceled -= _ => StopAim();
    }

    void StartAim(){
        _virtualCamera.Priority += priorityAmount;
        aimCanvas.enabled = true;
        thirdPersonCanvas.enabled = false;
    }

    void StopAim(){
        _virtualCamera.Priority -= priorityAmount;
        aimCanvas.enabled = false;
        thirdPersonCanvas.enabled = true;
    }
}