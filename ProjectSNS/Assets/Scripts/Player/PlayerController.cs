using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    
    [SerializeField] float _movementSpeed;

    void Update(){
        HandleMovementInput();
        HandlerRotationInput();
    }

    void HandleMovementInput(){
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");

        Vector3 _movement = new Vector3(_horizontal, 0, _vertical);
        transform.Translate(_movement * (_movementSpeed * Time.deltaTime), Space.World);
    }

    void HandlerRotationInput(){
        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit)){
            transform.LookAt(new Vector3(_hit.point.x, transform.position.y,_hit.point.z));
        }
    }
}
