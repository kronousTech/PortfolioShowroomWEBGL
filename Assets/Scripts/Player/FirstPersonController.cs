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
    [SerializeField][Range(0f, 90f)] float _xRotationLimit = 88f;

    private float interpolateValue = 15f;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;

        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        _playerInput = GetComponent<PlayerInput>();
        _playerInput.AddOnMovementListener(MovePlayer);
        _playerInput.AddOnMouseMovingListener(RotateView);
        //_playerInput.AddOnMouseMovingListener(RotateOrientation);
        //_playerInput.AddOnMouseMovingListener(RotateHead);

        _playerGroundCheck = GetComponent<PlayerGroundCheck>();
        _playerGroundCheck.AddOnGroundStateChangeListener(HandleDrag);
    }
    private void Update()
    {
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

        _rigidbody.AddForce(_moveSpeed * Time.maximumDeltaTime * moveDirection.normalized, ForceMode.Force);
    }

    private void RotateView(Vector2 mouseInput)
    {
        var inputX = Mathf.Clamp(mouseInput.x, -interpolateValue, interpolateValue) * _lookSensivity * Time.maximumDeltaTime;
        var inputY = Mathf.Clamp(mouseInput.y, -interpolateValue, interpolateValue) * _lookSensivity * Time.maximumDeltaTime;

        // Interpolated way
        _mainCamera.transform.localEulerAngles -= new Vector3(Mathf.Clamp(inputY, -_xRotationLimit, _xRotationLimit), 0, 0);
        _orientation.localEulerAngles += new Vector3(0, inputX, 0);
    }
}