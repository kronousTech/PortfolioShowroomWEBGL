using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class FirstPersonController : MonoBehaviour
{
    [SerializeField] public Transform _orientation;
    [SerializeField] private bool _isOnGround = true;
    [SerializeField] private bool _isOnSlope = true;

    private Rigidbody _rigidbody;
    private GameObject _mainCamera;
    private PlayerInput _playerInput;
    private PlayerGroundCheck _playerGroundCheck;
    private PlayerSlopeCheck _playerSlopeCheck;

    [Header("Player Settings")]
    private float _moveSpeed;
    [SerializeField][Range(1f, 10f)] private float _walkSpeed = 7f;
    [SerializeField][Range(1.5f, 15f)] private float _sprintSpeed = 10f;

    [Header("Jumping Handling")]
    [SerializeField][Range(0f, 10)] private float _groundDrag = 5f;
    [SerializeField][Range(5f, 20f)] private float _jumpForce = 12f;
    [SerializeField][Range(0f, 5f)] private float _airMultiplier = 0.25f;

    [Header("Slope Handling")]
    [SerializeField] private float _slopeForceMultiplier;
    [SerializeField] private float _slopeDownForceMultiplier;

    [Header("Camera Handling")]
    [SerializeField][Range(10f, 100f)] private float _lookSensivity = 2f;
    [SerializeField][Range(0f, 90f)] private float _xRotationLimit;

    private Vector3 _moveDirection;
    private readonly float _interpolateValue = 10f;
    private readonly float _forceMultiplier = 1000f;
   

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;

        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        _playerInput = GetComponent<PlayerInput>();
        _playerInput.AddOnMovementListener(MovePlayer);
        _playerInput.AddOnMouseMovingListener(RotateView);
        _playerInput.AddOnJumpInputListener(JumpListener);
        _playerInput.AddOnSprintInputListener(HandleMoveSpeedListener);

        _playerGroundCheck = GetComponent<PlayerGroundCheck>();
        _playerGroundCheck.AddOnGroundStateChangeListener(HandleDragListener);
        _playerGroundCheck.AddOnGroundStateChangeListener(HandleOnGroundStateListener);

        _playerSlopeCheck = GetComponent<PlayerSlopeCheck>();
        _playerSlopeCheck.AddOnStateListener(MovePlayerOnSlope);
        _playerSlopeCheck.AddOnStateChangeListener(HandleOnSlopeStateListener);
        _playerSlopeCheck.AddOnStateChangeListener(HandleGravityListener);

        _moveSpeed = _walkSpeed;
    }

    private void Update()
    {
        SpeedControl();
    }
    
    private void HandleDragListener(bool state)
    {
        _rigidbody.drag = state ? _groundDrag : 0;
    }
    private void HandleGravityListener(bool onSlope)
    {
        _rigidbody.useGravity = !onSlope;
    }
    private void HandleOnGroundStateListener(bool state)
    {
        _isOnGround = state;
    }
    private void HandleOnSlopeStateListener(bool state)
    {
        _isOnSlope = state;
    }
    private void HandleMoveSpeedListener(bool state)
    {
        _moveSpeed = state ? _sprintSpeed : _walkSpeed;
    }

    private void JumpListener()
    {
        if (!_isOnGround && !_isOnSlope)
            return;

        var velocity = _rigidbody.velocity;
        _rigidbody.velocity = new Vector3(velocity.x, 0f, velocity.z);
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }
    private void MovePlayer(Vector2 movementInput)
    {
        _moveDirection = _orientation.forward * movementInput.y + _orientation.right * movementInput.x;

        var force = _moveDirection.normalized* _moveSpeed * Time.deltaTime * (_isOnGround ? 1 : _airMultiplier);

        _rigidbody.AddForce(force * _forceMultiplier, ForceMode.Force);
    }
    private void MovePlayerOnSlope()
    {
        var slopeDirection = _playerSlopeCheck.GetSlopeMoveDirection(_moveDirection);
        var slopeForce = slopeDirection * _moveSpeed * Time.deltaTime;

        _rigidbody.AddForce(slopeForce * _forceMultiplier * _slopeForceMultiplier, ForceMode.Force);

        if (_rigidbody.velocity.y > 0 || _moveDirection != Vector3.zero)
            _rigidbody.AddForce(Vector3.down * _slopeDownForceMultiplier, ForceMode.Force);
    }
    private void RotateView(Vector2 mouseInput)
    {
        // Interpolated way
        var inputX = Mathf.Clamp(mouseInput.x, -_interpolateValue, _interpolateValue) * _lookSensivity * Time.deltaTime;
        var inputY = Mathf.Clamp(mouseInput.y, -_interpolateValue, _interpolateValue) * _lookSensivity * Time.deltaTime;

        var newXAngle = _mainCamera.transform.localEulerAngles.x - inputY;
        _mainCamera.transform.localEulerAngles = new Vector3(
            Mathf.Clamp((newXAngle <= 180) ? newXAngle : -(360 - newXAngle), -_xRotationLimit, _xRotationLimit),
            transform.localEulerAngles.y, 
            transform.localEulerAngles.z);

        _orientation.localEulerAngles += new Vector3(0, inputX, 0);
    }
    private void SpeedControl()
    {
        var velocity = _rigidbody.velocity;

        if (_isOnSlope)
        {
            if(velocity.magnitude > _moveSpeed)
                _rigidbody.velocity = _rigidbody.velocity.normalized * _moveSpeed;
        }
        else
        {
            var flatVelocity = new Vector3(velocity.x, 0, velocity.z);

            if (flatVelocity.magnitude > _moveSpeed)
            {
                var limitedVelocity = flatVelocity.normalized * _moveSpeed;
                _rigidbody.velocity = new Vector3(limitedVelocity.x, velocity.y, limitedVelocity.z);
            }
        }
    }
}