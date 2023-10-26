using Core.Player.Interactions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.PlayerInteraction
{
    public class PlayerGraphicToggleInteractable : MonoBehaviour
    {
        private Toggle _selectedToggle;

        private void Awake()
        {
            GetComponent<PlayerGraphicRay>().OnElementsUpdate += CheckForButton;
        }

        private void CheckForButton(List<GameObject> elements)
        {
            foreach (var item in elements)
            {
                if (item.TryGetComponent<Toggle>(out var toggle))
                {
                    _selectedToggle = toggle;

                    return;
                }
            }

            _selectedToggle = null;
        }

        private void Update()
        {
            if (_selectedToggle == null)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _selectedToggle.isOn = !_selectedToggle.isOn;
            }
        }
    }
}

