using System;
using System.Collections;
using System.Collections.Generic;
using Drakon.MovementSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private MSEntity _msEntity = null;
    private PlayerControls _playerControls = null;

    private Vector3 _moveInput = Vector3.zero;
    
    void Awake()
    {
        _playerControls = new PlayerControls();
        _playerControls.Gameplay.Move.performed += OnMove;
        _playerControls.Gameplay.Move.canceled += OnMove;
        _playerControls.Gameplay.Dash.performed += OnDash;
        _playerControls.Gameplay.Sprint.performed += OnSprint;
        _playerControls.Gameplay.Sprint.canceled += OnSprint;
        _playerControls.Gameplay.Enable();
    }

    private void Update()
    {
        _msEntity.Move(_moveInput);
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 raw = ctx.ReadValue<Vector2>();
        _moveInput = new Vector3(raw.x, 0, raw.y);
    }
    
    private void OnDash(InputAction.CallbackContext ctx)
    {
        _msEntity.Dash(_moveInput);
    }

    private void OnSprint(InputAction.CallbackContext ctx)
    {
        _msEntity.Sprinting = ctx.ReadValue<float>() >= 0.95f;
    }
}
