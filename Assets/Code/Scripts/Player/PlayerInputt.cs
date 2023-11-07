using System;
using UnityEngine;

public class PlayerInputt : MonoBehaviour
{
    [Header("Character Input Values")]
    [SerializeField] private Vector2 _movementInput;
    [SerializeField] private Vector2 _mouseInput;

    // ----- Mouse moving -----
    private event Action _onMovement = new(() => { });
    private event Action<Vector2> _onMovementValue = new((Vector2) => { });
    private event Action<bool> _onMovementState = new((state) => { });

    private event Action _onMouseMoving = new(() => { });
    private event Action<Vector2> _onMouseMovingValue = new((Vector2) => { });

    private event Action _onJumpInput = new(() => { });

    private event Action<bool> _onSprintInput = new((state) => { });

    private const string MOVEMENT_HORIZONTAL_INPUT = "Horizontal";
    private const string MOVEMENT_VERTICAL_INPUT = "Vertical";
    private const string MOUSE_X_INPUT = "Mouse X";
    private const string MOUSE_Y_INPUT = "Mouse Y";

    private bool _isMoving = false;

    private void Update()
    {
        CheckMovementInput();
        CheckJumpInput();
        CheckSprintInput();
    }

    private void FixedUpdate()
    {
        InvokeMovementInputListeners();
    }
    private void LateUpdate()
    {
        CheckMouseInput();
        InvokeMouseInputListeners();
    }

    private void CheckMovementInput()
    {
        var hMovement = Input.GetAxisRaw(MOVEMENT_HORIZONTAL_INPUT);
        var vMovement = Input.GetAxisRaw(MOVEMENT_VERTICAL_INPUT);

        _movementInput = new Vector2(hMovement, vMovement) * Time.deltaTime;
    }
    private void CheckMouseInput()
    {
        var xAxis = Input.GetAxisRaw(MOUSE_X_INPUT);
        var yAxis = Input.GetAxisRaw(MOUSE_Y_INPUT);

        _mouseInput = new Vector2(xAxis, yAxis);
    }
    private void CheckJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _onJumpInput?.Invoke();
        }
    }
    private void CheckSprintInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _onSprintInput?.Invoke(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _onSprintInput?.Invoke(false);
        }
    }

    private void InvokeMovementInputListeners()
    {
        var hasInput = (_movementInput.x != 0 || _movementInput.y != 0);
        if (_isMoving != hasInput)
        {
            _isMoving = hasInput;
            _onMovementState?.Invoke(_isMoving);
        }

        _onMovement?.Invoke();
        _onMovementValue?.Invoke(_movementInput);
    }
    private void InvokeMouseInputListeners()
    {
        // Invoke if mouse moved
        //if (_mouseInput.x != 0 || _mouseInput.y != 0)
        {
            _onMouseMoving?.Invoke();
            _onMouseMovingValue?.Invoke(_mouseInput);
        }
    }

    public void AddOnMovementListener(Action listener) => _onMovement += listener;
    public void AddOnMovementListener(Action<bool> listener) => _onMovementState += listener;
    public void AddOnMovementListener(Action<Vector2> listener) => _onMovementValue += listener;
    public void RemoveOnMovementListener(Action listener) => _onMovement -= listener;
    public void RemoveOnMovementListener(Action<Vector2> listener) => _onMovementValue -= listener;

    public void AddOnMouseMovingListener(Action listener) => _onMouseMoving += listener;
    public void AddOnMouseMovingListener(Action<Vector2> listener) => _onMouseMovingValue += listener;
    public void RemoveOnMouseMovingListener(Action listener) => _onMouseMoving -= listener;
    public void RemoveOnMouseMovingListener(Action<Vector2> listener) => _onMouseMovingValue -= listener;

    public void AddOnJumpInputListener(Action listener) => _onJumpInput += listener;
    public void RemoveOnJumpInputListener(Action listener) => _onJumpInput -= listener;

    public void AddOnSprintInputListener(Action<bool> listener) => _onSprintInput += listener;
    public void RemoveOnSprintInputListener(Action<bool> listener) => _onSprintInput -= listener;
}