using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Character Input Values")]
    [SerializeField] private Vector2 _movementInput;
    [SerializeField] private Vector2 _mouseInput;

    [Header("Mouse Cursor Settings")]
    [SerializeField] private bool cursorLocked = true;
    [SerializeField] private bool cursorInputForLook = true;

    // ----- Mouse moving -----
    private Action _onMovement = new(() => { });
    private Action<Vector2> _onMovementValue = new((Vector2) => { });
    private Action _onMouseMoving = new(() => { });
    private Action<Vector2> _onMouseMovingValue = new((Vector2) => { });

    private const string MOVEMENT_HORIZONTAL_INPUT = "Horizontal";
    private const string MOVEMENT_VERTICAL_INPUT = "Vertical";
    private const string MOUSE_X_INPUT = "Mouse X";
    private const string MOUSE_Y_INPUT = "Mouse Y";
    

    private void Update()
    {
        SetMouseInput();
        SetMovementInput();
    }
    private void FixedUpdate()
    {
        InvokeMovementListeners();
    }
    private void LateUpdate()
    {
        InvokeMouseMovingListeners();
    }
    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    private void SetMovementInput()
    {
        var hMovement = Input.GetAxisRaw(MOVEMENT_HORIZONTAL_INPUT);
        var vMovement = Input.GetAxisRaw(MOVEMENT_VERTICAL_INPUT);

        _movementInput = new Vector2(hMovement, vMovement);
    }
    private void SetMouseInput()
    {
        var xAxis = Input.GetAxis(MOUSE_X_INPUT);
        var yAxis = Input.GetAxis(MOUSE_Y_INPUT);

        _mouseInput = new Vector2(xAxis, yAxis);
    }

    private void InvokeMovementListeners()
    {
        if (_movementInput.x != 0 || _movementInput.y != 0)
        {
            _onMovement?.Invoke();
            _onMovementValue?.Invoke(_movementInput);
        }
    }
    private void InvokeMouseMovingListeners()
    {
        // Invoke if mouse moved
        if (_mouseInput.x != 0 || _mouseInput.y != 0)
        {
            _onMouseMoving?.Invoke();
            _onMouseMovingValue?.Invoke(_mouseInput);
        }
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void AddOnMovementListener(Action listener) => _onMovement += listener;
    public void AddOnMovementListener(Action<Vector2> listener) => _onMovementValue += listener;
    public void RemoveOnMovementListener(Action listener) => _onMovement -= listener;
    public void RemoveOnMovementListener(Action<Vector2> listener) => _onMovementValue -= listener;

    public void AddOnMouseMovingListener(Action listener) => _onMouseMoving += listener;
    public void AddOnMouseMovingListener(Action<Vector2> listener) => _onMouseMovingValue += listener;
    public void RemoveOnMouseMovingListener(Action listener) => _onMouseMoving -= listener;
    public void RemoveOnMouseMovingListener(Action<Vector2> listener) => _onMouseMovingValue -= listener;
}