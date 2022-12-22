using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorState : MonoBehaviour
{
    [Header("Mouse Cursor Settings")]
    [SerializeField] private bool _lockedOnStart;

    private void Awake()
    {
        SetCursorLockState(_lockedOnStart);
    }
    public void SetCursorLockState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}