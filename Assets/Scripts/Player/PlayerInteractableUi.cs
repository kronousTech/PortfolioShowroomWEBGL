using Core.Player;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractableUi : MonoBehaviour
{
    private PlayerInteractableRay _ray;
    public event Action<bool> OnLookedAtInteractableUi = new((state) => { });
    private bool _isLookingAtUi;
    private bool IsLookingAtUi
    {
        get { return _isLookingAtUi; }
        set
        {
            if (_isLookingAtUi != value)
            {
                _isLookingAtUi = value;

                OnLookedAtInteractableUi?.Invoke(value);
            }
        }
    }

    [SerializeField] private Animator _selectedButton;

    private void Awake()
    {
        _ray = GetComponent<PlayerInteractableRay>();
        _ray.OnLookingAtInteractable += Behaviour;
    }

    private void Behaviour(GameObject interactable)
    {
        if (interactable == null)
        {
            if(_selectedButton != null)
                _selectedButton.SetBool("Selected", false);
            _selectedButton = null;

            IsLookingAtUi = false;
            return;
        }

        if (interactable.GetComponent<Button>())
        {
            IsLookingAtUi = true;

            if(_selectedButton != interactable.GetComponent<Animator>())
            {
                if (_selectedButton != null)
                    _selectedButton?.SetBool("Selected", false);

                _selectedButton = interactable.GetComponent<Animator>();
                _selectedButton.SetBool("Selected", true);
            }

            if (InteractKeyIsPressed())
            {
                interactable.GetComponent<Button>().onClick.Invoke();
                _selectedButton.SetTrigger("Pressed");
            }
        }
        else
        {
            _selectedButton.SetBool("Selected", false);
            _selectedButton = null;

            IsLookingAtUi = false;
        }
    }

    private bool InteractKeyIsPressed()
    {
        return Input.GetMouseButtonDown(0);
    }
}
