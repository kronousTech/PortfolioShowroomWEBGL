using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class FirstPersonController : MonoBehaviour
{
    [SerializeField] public Transform _orientation;

    private Rigidbody _rigidbody;
    private GameObject _mainCamera;
    private PlayerInput _playerInput;
    private PlayerGroundCheck _playerGroundCheck;

    [Header("Player Settings")]
    [SerializeField][Range(100f, 400f)] private float _moveSpeed = 150f;
    [SerializeField][Range(0f, 10)] private float _groundDrag = 5f;
    [SerializeField][Range(0.05f, 5f)] private float _lookSensivity = 2f;
    [SerializeField][Range(0f, 90f)] private float _xRotationLimit = 88f;
    [SerializeField][Range(5f, 20f)] private float _jumpForce = 12f;
    [SerializeField][Range(0f, 5f)] private float _airMultiplier = 0.25f;
    [SerializeField] private bool _isOnGround = true;


    private float interpolateValue = 15f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;

        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        _playerInput = GetComponent<PlayerInput>();
        _playerInput.AddOnMovementListener(MovePlayer);
        _playerInput.AddOnMouseMovingListener(RotateView);
        _playerInput.AddOnJumpInputListener(Jump);

        _playerGroundCheck = GetComponent<PlayerGroundCheck>();
        _playerGroundCheck.AddOnGroundStateChangeListener(HandleDrag);
        _playerGroundCheck.AddOnGroundStateChangeListener(CheckIfJumpIsReady);
    }
    private void Update()
    {
        SpeedControl();
        //if (Input.GetKeyDown(KeyCode.C)) interpolateValue += 1f;
        //if (Input.GetKeyDown(KeyCode.V)) interpolateValue -= 1f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + _orientation.forward);
    }
    private void HandleDrag(bool state)
    {
        _rigidbody.drag = state ? _groundDrag : 0;
    }

    private void MovePlayer(Vector2 movementInput)
    {
        var moveDirection = _orientation.forward * movementInput.y
            + _orientation.right * movementInput.x;
        var force = (_moveSpeed * Time.maximumDeltaTime * moveDirection.normalized) * (_isOnGround ? 1:_airMultiplier);

        _rigidbody.AddForce(force, ForceMode.Force);
    }

    private void RotateView(Vector2 mouseInput)
    {
        var inputX = Mathf.Clamp(mouseInput.x, -interpolateValue, interpolateValue) * _lookSensivity * Time.maximumDeltaTime;
        var inputY = Mathf.Clamp(mouseInput.y, -interpolateValue, interpolateValue) * _lookSensivity * Time.maximumDeltaTime;

        // Interpolated way
        _mainCamera.transform.localEulerAngles -= new Vector3(Mathf.Clamp(inputY, -_xRotationLimit, _xRotationLimit), 0, 0);
        _orientation.localEulerAngles += new Vector3(0, inputX, 0);
    }

    private void SpeedControl()
    {
        var velocity = _rigidbody.velocity;
        var flatVelocity = new Vector3(velocity.x, 0, velocity.z);

        if(flatVelocity.magnitude > _moveSpeed)
        {
            var limitedVelocity = flatVelocity.normalized * _moveSpeed;
            _rigidbody.velocity = new Vector3(limitedVelocity.x, velocity.y, limitedVelocity.z);
        }
    }

    private void Jump()
    {
        if (!_isOnGround)
            return;

        var velocity = _rigidbody.velocity;
        _rigidbody.velocity = new Vector3(velocity.x, 0f, velocity.z);
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void CheckIfJumpIsReady(bool state)
    {
        _isOnGround = state;
    }
}