using UnityEngine;
using UnityEngine.Events;

public class OnVisibilityEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent<bool> OnVisible;

    private void OnBecameVisible()
    {
        OnVisible?.Invoke(true);
    }
    private void OnBecameInvisible()
    {
        OnVisible?.Invoke(false);
    }
}
