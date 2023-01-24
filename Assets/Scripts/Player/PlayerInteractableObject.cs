using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Player 
{
    public class PlayerInteractableObject : MonoBehaviour
    {
        private PlayerInteractableRay _ray;
        public event Action<bool> OnLookedAtInteractableObject = new((state) => { });
        private bool _isLookingAtObject;
        private bool IsLookingAtObject
        {
            get { return _isLookingAtObject; }
            set
            {
                if(_isLookingAtObject != value)
                {
                    _isLookingAtObject = value;

                    OnLookedAtInteractableObject?.Invoke(value);
                }
            }
        }


        private void Awake()
        {
            _ray = GetComponent<PlayerInteractableRay>();
            _ray.OnLookingAtInteractable += Behaviour;
        }

        private void Behaviour(GameObject interactable)
        {
            if (interactable == null)
            {
                IsLookingAtObject = false;
                return;
            }

            if (interactable.GetComponent<IInteractable>() != null)
            {
                IsLookingAtObject = true;

                if (InteractKeyIsPressed())
                    interactable.GetComponent<IInteractable>().OnInteract();
            }
            else
            {
                IsLookingAtObject = false;
            }
        }

        private bool InteractKeyIsPressed()
        {
            return Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E);
        }
    }
}