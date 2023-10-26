using UnityEngine;

public class PlayerHeadBobbing : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private float _maxSpeed;
    private float _minSpeed;
    [SerializeField] private bool _state;

    private void Awake()
    {
        _minSpeed = GetComponent<FirstPersonController>().GetWalkSpeed();
        _maxSpeed = GetComponent<FirstPersonController>().GetSprintSpeed();

        GetComponent<PlayerGroundCheck>().AddOnGroundStateChangeListener(SetAnimatorOnGroundState);
        GetComponent<PlayerInput>().AddOnMovementListener(SetAnimatorWakingState);
        GetComponent<PlayerInput>().AddOnSprintInputListener(SetAnimatorSpeed);

        SetAnimatorOnGroundState(false);
        SetAnimatorWakingState(false);
        SetAnimatorSpeed(false);
        SetState(_state);
    }

    public void SetState(bool value)
    {
        _state = value;
        _animator.SetBool("Enabled", value);
    }
    public bool GetState()
    {
        return _state;
    }

    private void SetAnimatorOnGroundState(bool value)
    {
        _animator.SetBool("OnGround", value);
    }
    private void SetAnimatorWakingState(bool value)
    {
        _animator.SetBool("Moving", value);
    }
    private void SetAnimatorSpeed(bool value)
    {
        _animator.SetFloat("Speed", value ? _maxSpeed : _minSpeed);
    }
}