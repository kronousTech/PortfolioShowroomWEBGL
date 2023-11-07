using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Input ettings")]
		[SerializeField] private bool _disableMouseInput = false;
		[SerializeField] private bool _disableMoveInput = false;

		public static event Action<bool> OnSprintEvent;
        public static event Action<bool> OnMoveEvent;

        private void OnEnable()
        {
			GameEvents.OnPanelOpen += (state) => _disableMouseInput = state;
            GameEvents.OnPanelOpen += (state) => _disableMoveInput = state;
        }
        private void OnDisable()
        {
            GameEvents.OnPanelOpen -= (state) => _disableMouseInput = state;
            GameEvents.OnPanelOpen -= (state) => _disableMoveInput = state;
        }

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
		{
			if (_disableMoveInput)
				return;
         
			MoveInput(value.Get<Vector2>());
        }
        public void OnLook(InputValue value)
		{
			if (_disableMouseInput)
				return;
		
			LookInput(value.Get<Vector2>());
        }
        public void OnJump(InputValue value)
		{
            if (_disableMoveInput)
                return;
            
			JumpInput(value.isPressed);
		}
		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
#endif

		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;

			OnMoveEvent?.Invoke(move != Vector2.zero);
        } 
		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}
		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}
		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;

            OnSprintEvent?.Invoke(newSprintState);
        }
	}
}