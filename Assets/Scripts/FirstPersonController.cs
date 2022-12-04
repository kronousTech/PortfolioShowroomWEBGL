using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private Transform _orientation;
    [SerializeField] private Transform _headPosition;

    private Rigidbody _rigidbody;
    private GameObject _mainCamera;
    private PlayerInput _playerInput;

    [Header("Player Settings")]
    [SerializeField][Range(15f, 50f)] private float _moveSpeed = 35f;
    [SerializeField][Range(20f, 100f)] private float _lookSensivity = 50f;
    [SerializeField][Range(0f, 90f)] float _xRotationLimit = 88f;

    private float _xRotation;
    private float _yRotation;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;

        // get a reference to our main camera
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        _playerInput = GetComponent<PlayerInput>();

        _playerInput.AddOnMovementListener(MovePlayer);

        _playerInput.AddOnMouseMovingListener(UdpateLookValues);
        _playerInput.AddOnMouseMovingListener(RotateHead);
        _playerInput.AddOnMouseMovingListener(RotateOrientation);
    }

    private void MovePlayer(Vector2 movementInput)
    {
        var moveDirection = _orientation.forward * movementInput.y
            + _orientation.right * movementInput.x;

        _rigidbody.AddForce(moveDirection.normalized * _moveSpeed, ForceMode.Force);
    }

    private void UdpateLookValues(Vector2 mouseInput)
    {
        var inputX = mouseInput.x * _lookSensivity * Time.deltaTime;
        var inputY = mouseInput.y * _lookSensivity * Time.deltaTime;

        _yRotation += inputX;
        _xRotation -= inputY;
        _xRotation = Mathf.Clamp(_xRotation, -_xRotationLimit, _xRotationLimit);
    }
    private void RotateHead()
    {
        _mainCamera.transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
    }

    private void RotateOrientation()
    {
        _orientation.rotation = Quaternion.Euler(0, _yRotation, 0);
    }
}