using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CanvasGroup))]
public class UiDisplay : MonoBehaviour
{
    [SerializeField] private KeyCode _key;
    [SerializeField] private UnityEvent _onOpen;
    [SerializeField] private UnityEvent _onClose;
    private bool _isOpen;

    private void Update()
    {
        if (Input.GetKeyDown(_key))
        {
            TogglePanel();
        }
    }

    private void TogglePanel()
    {
        if (!CanOpen())
            return;

        _isOpen = !_isOpen;

        GetComponent<Animator>().SetBool("Open", _isOpen);

        if (_isOpen)
            _onOpen?.Invoke();
        else
            _onClose?.Invoke();
    }

    private bool CanOpen()
    {
        foreach (var display in GameObject.FindObjectsOfType<UiDisplay>())
        {
            if (display == this)
                continue;
            else if (display.IsOpen())
                return false;
        }

        return true;
    }

    public bool IsOpen() => _isOpen;
}
