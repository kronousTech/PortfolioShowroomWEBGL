using UnityEngine;
using UnityEngine.Events;

public class OnVisibilityEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent<bool> OnVisible;

    [SerializeField] private UnityEvent OnBecameVisibleEvent;

    private void OnBecameVisible()
    {
        OnVisible?.Invoke(true);

        OnBecameVisibleEvent?.Invoke();
    }
    private void OnBecameInvisible()
    {
        OnVisible?.Invoke(false);
    }
}
