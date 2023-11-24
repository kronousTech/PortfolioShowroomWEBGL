using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CanvasGroup))]
public class UiDisplay : MonoBehaviour
{
    [SerializeField] private UnityEvent _onOpen;
    [SerializeField] private UnityEvent _onClose;
    [SerializeField] private InputActionReference _actionRef;

    private bool _isOpen;

    private void Awake()
    {
        _actionRef.action.performed += (a) => TogglePanel();
    }
    private void OnEnable()
    {
        _actionRef.action.Enable();
    }
    private void OnDisable()
    {
        _actionRef.action.Disable();
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

        GameEvents.OnPanelOpen?.Invoke(_isOpen);
    }


    public void ClosePanel()
    {
        if (_isOpen)
            TogglePanel();
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
