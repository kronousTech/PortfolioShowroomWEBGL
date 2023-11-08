using System;
using UnityEngine;

public class PlayerSteps : MonoBehaviour
{
    public static event Action OnStep;

    // Function called via animation
    protected void Step()
    {
        OnStep?.Invoke();
    }

    public void AddListener(Action listener) => OnStep += listener;
    public void RemoveListener(Action listener) => OnStep -= listener;
}
