using System;
using UnityEngine;

public enum GroundType
{
    Wood = 0,
    Grass = 1,
    Concrete = 2
}

public class PlayerGrounded : MonoBehaviour
{
    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    public static bool IsGrounded = false;
    [Tooltip("Useful for rough ground")]
    public float GroundedOffset = -0.14f;
    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    public float GroundedRadius = 0.5f;
    [Tooltip("What layers the character uses as ground")]
    public LayerMask GroundLayers;

    private GroundType _type;

    public static event Action<bool> OnGroundedEvent;
    public static event Action<GroundType> OnGroundTypeChange;

    private void Start()
    {
        GroundedCheck();
    }
    private void Update()
    {
		GroundedCheck();
    }

    private void GroundedCheck()
    {
        // set sphere position, with offset
        var spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        var currentState = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);

        if (currentState != IsGrounded)
        {
            // If it turned grounded
            if (currentState)
            {
                CheckGroundType();
            }

            IsGrounded = currentState;

            OnGroundedEvent?.Invoke(IsGrounded);
        }
    }

    private void CheckGroundType()
    {
        if (Physics.Raycast(new Ray(transform.position, Vector3.down), out var hit, 1f, GroundLayers))
        {
            if (Enum.TryParse(typeof(GroundType), hit.transform.tag, false, out object result))
            {
                if(_type != (GroundType)result)
                {
                    _type = (GroundType)result;

                    OnGroundTypeChange?.Invoke(_type);
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (IsGrounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
    }
}
