using System;
using UnityEngine;
public class PlayerSlopeCheck : MonoBehaviour
{
    [SerializeField][Range(15f, 75f)] private float _maxSlopeAngle;
    [SerializeField] private float _rayLength;
    private RaycastHit _slopeHit;
    private bool _isOnSlope = false;

    private Action _onState = new(() => { });
    private Action<bool> _onStateChange = new((b) => { });

    private void Update()
    {
        if (OnSlope() != _isOnSlope)
        {
            _isOnSlope = !_isOnSlope;

            _onStateChange?.Invoke(_isOnSlope);
        }
    }
    private void FixedUpdate()
    {
        if (_isOnSlope)
            _onState?.Invoke();
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out _slopeHit, _rayLength))
        {
            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            return angle < _maxSlopeAngle && angle != 0;
        }

        return false;
    }
    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, _slopeHit.normal).normalized;
    }

    public void AddOnStateListener(Action listener) => _onState += listener;
    public void RemoveOnStateListener(Action listener) => _onState -= listener;
    public void AddOnStateChangeListener(Action<bool> listener) => _onStateChange += listener;
    public void RemoveOnStateChangeListener(Action<bool> listener) => _onStateChange -= listener;
}