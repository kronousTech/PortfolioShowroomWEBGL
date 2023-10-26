using Core.Player.Interactions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGraphicButtonInteractable : MonoBehaviour
{
    private Animator _SelectedButton;
    private Animator SelectedButton
    {
        get { return _SelectedButton; }
        set 
        { 
            if( _SelectedButton != value)
            {
                _SelectedButton?.SetBool("Selected", false);

                _SelectedButton = value;

                _SelectedButton?.SetBool("Selected", true);
            }
        }
    }

    private void Awake()
    {
        GetComponent<PlayerGraphicRay>().OnElementsUpdate += CheckForButton;
    }

    private void CheckForButton(List<GameObject> elements)
    {
        foreach (var item in elements)
        {
            if (item.GetComponent<Button>())
            {
                SelectedButton = item.GetComponent<Animator>();
                return;
            }
        }

        SelectedButton = null;
    }

    private void Update()
    {
        if(SelectedButton == null)
        {
            return;
        }

        if (InteractKeyIsPressed())
        {
            SelectedButton.GetComponent<Button>().onClick.Invoke();
            SelectedButton.SetTrigger("Pressed");
        }
    }

    private bool InteractKeyIsPressed()
    {
        return Input.GetMouseButtonDown(0);
    }
}
