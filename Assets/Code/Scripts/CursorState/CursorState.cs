using UnityEngine;

public class CursorState : MonoBehaviour
{
    [Header("Mouse Cursor Settings")]
    [SerializeField] private bool _lockedOnStart;

    private CursorLockMode _lockMode;

    private void OnEnable()
    {
        GameEvents.OnPanelOpen += (open) => SetCursorLockState(!open);
    }
    private void OnDisable()
    {
        GameEvents.OnPanelOpen -= (open) => SetCursorLockState(!open);
    }
    private void Awake()
    {
        SetCursorLockState(_lockedOnStart);
    }
    private void OnApplicationFocus(bool hasFocus)
    {
        Cursor.lockState = _lockMode;
    }

    public void SetCursorLockState(bool locked)
    {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;

        _lockMode = Cursor.lockState;
    }
}