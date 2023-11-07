using StarterAssets;
using UnityEngine;

public class PlayerHeadBobbing : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _state;

    private FirstPersonController _controller;

    private void Awake()
    {
        _controller = GetComponent<FirstPersonController>();

        SetAnimatorOnGroundState(false);
        SetAnimatorWakingState(false);
        SetAnimatorSpeed(false);
        SetState(_state);
    }
    private void OnEnable()
    {
        FirstPersonController.OnGroundedEvent += SetAnimatorOnGroundState;
        StarterAssetsInputs.OnSprintEvent += SetAnimatorSpeed;
        StarterAssetsInputs.OnMoveEvent += SetAnimatorWakingState;
    }
    private void OnDisable()
    {
        FirstPersonController.OnGroundedEvent -= SetAnimatorOnGroundState;
        StarterAssetsInputs.OnSprintEvent -= SetAnimatorSpeed;
        StarterAssetsInputs.OnMoveEvent -= SetAnimatorWakingState;
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
       _animator.SetFloat("Speed", value ? _controller.SprintSpeed : _controller.MoveSpeed);
    }
}