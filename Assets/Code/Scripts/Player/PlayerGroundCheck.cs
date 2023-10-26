using System;
using UnityEngine;

public enum GroundType
{
    Wood = 0,
    Grass = 1,
    Concrete = 2
}

public class PlayerGroundCheck : MonoBehaviour
{
    [Header("Ground Type")]
    [SerializeField] private GroundType _type;

    [Header("Ground Check")]
    [SerializeField] private float _sphereRadius = 0.3f;
    [SerializeField] private float _checkOffset;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private bool _grounded;

    private event Action<bool> _onGroundStateChange = new((b) => { });
    public event Action<GroundType> OnGroundTypeChange = new((b) => { });


    private void Update()
    {
        if (IsOnGround() != _grounded)
        {
            _grounded = !_grounded;

            _onGroundStateChange?.Invoke(_grounded);
        }

        if(CheckGroundType() != _type) 
        {
            _type = CheckGroundType();

            OnGroundTypeChange?.Invoke(_type);
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

    private GroundType CheckGroundType() 
    {
        if(Physics.Raycast(new Ray(transform.position, Vector3.down), out var hit ,1f, _groundLayer))
        {
            var tag = hit.transform.tag;
            if(Enum.TryParse(typeof(GroundType), tag, false, out object result)) 
            {
                return (GroundType)result;
            }
        }
        return _type;
    }

    public void AddOnGroundStateChangeListener(Action<bool> listener) => _onGroundStateChange += listener;
    public void RemoveOnGroundStateChangeListener(Action<bool> listener) => _onGroundStateChange -= listener;
}
