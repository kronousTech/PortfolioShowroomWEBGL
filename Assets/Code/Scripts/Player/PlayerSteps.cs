using System;
using UnityEngine;

public class PlayerSteps : MonoBehaviour
{
    private event Action _onStep;

    // Function called via animation
    protected void Step()
    {
        _onStep?.Invoke();
    }

    public void AddListener(Action listener) => _onStep += listener;
    public void RemoveListener(Action listener) => _onStep -= listener;
}
