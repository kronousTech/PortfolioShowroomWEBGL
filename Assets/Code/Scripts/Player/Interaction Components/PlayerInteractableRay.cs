using System;
using UnityEngine;

namespace Core.Player
{
    public class PlayerInteractableRay : MonoBehaviour
    {
        private Transform _cameraTransform;
        [SerializeField] private float _interactDistance;
        private readonly string _interactableTag = "Interactable";

        public event Action<GameObject> OnLookingAtInteractable;

        private void Awake()
        {
            _cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out var hit, _interactDistance))
            {
                if (hit.transform.CompareTag(_interactableTag))
                {
                    OnLookingAtInteractable.Invoke(hit.transform.gameObject);
                    return;
                }
            }
            OnLookingAtInteractable.Invoke(null);
        }
    }
}