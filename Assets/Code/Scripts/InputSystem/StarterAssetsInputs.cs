using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace KronosTech.InputSystem
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
        public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Input Settings")]
		[SerializeField] private bool _disableMouseInput = false;
		[SerializeField] private bool _disableMoveInput = false;

		public static event Action<bool> OnSprintEvent;
        public static event Action<bool> OnMoveEvent;
        public static event Action OnJumpEvent;

        private void OnEnable()
        {
			AssetsLoader.OnBundlesDownload += () => _disableMouseInput = false;
            AssetsLoader.OnBundlesDownload += () => _disableMoveInput = false;

            GameEvents.OnPanelOpen += (state) => _disableMouseInput = state;
            GameEvents.OnPanelOpen += (state) => _disableMoveInput = state;
            GameEvents.OnPanelOpen += (state) => { if (state) StopMovement(); };
        }
        private void OnDisable()
        {
            AssetsLoader.OnBundlesDownload -= () => _disableMouseInput = false;
            AssetsLoader.OnBundlesDownload -= () => _disableMoveInput = false;

            GameEvents.OnPanelOpen -= (state) => _disableMouseInput = state;
            GameEvents.OnPanelOpen -= (state) => _disableMoveInput = state;
            GameEvents.OnPanelOpen += (state) => { if (state) StopMovement(); };
        }

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
		{
			MoveInput(_disableMoveInput ? Vector2.zero : value.Get<Vector2>());
        }
        public void OnLook(InputValue value)
		{
			LookInput(_disableMouseInput ? Vector2.zero : value.Get<Vector2>());
        }
        public void OnJump(InputValue value)
		{
            if (_disableMoveInput)
                return;

			if (value.isPressed)
				OnJumpEvent?.Invoke();

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

		private void StopMovement()
		{
			MoveInput(Vector2.zero);
			LookInput(Vector2.zero);
			JumpInput(false);
			SprintInput(false);
        }
	}
}