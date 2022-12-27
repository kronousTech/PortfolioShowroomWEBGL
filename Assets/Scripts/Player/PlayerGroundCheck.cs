using System;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [Header("Ground Check")]
    [SerializeField] private float _sphereRadius = 0.3f;
    [SerializeField] private float _checkOffset;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private bool _grounded;

    private event Action<bool> _onGroundStateChange = new((b) => { });

    private void Update()
    {
        if (IsOnGround() != _grounded)
        {
            _grounded = !_grounded;

            _onGroundStateChange?.Invoke(_grounded);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _grounded ? Color.green : Color.white;
        Gizmos.DrawSphere(transform.position - (Vector3.up * _checkOffset), _sphereRadius);
    }
    private bool IsOnGround()
    {
        var spherePos = new Vector3(transform.position.x, transform.position.y - _checkOffset, transform.position.z);
        return Physics.CheckSphere(spherePos, _sphereRadius, _groundLayer);
    }

    public void AddOnGroundStateChangeListener(Action<bool> listener) => _onGroundStateChange += listener;
    public void RemoveOnGroundStateChangeListener(Action<bool> listener) => _onGroundStateChange -= listener;
}
