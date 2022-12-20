using Transitions;
using UnityEngine;
using UnityEngine.Events;

public class TransitionUnityEvents : MonoBehaviour
{
    [SerializeField] private TransitionTunnelSide _defaultSide;
    [Header("Left Side")]
    [SerializeField] private UnityEvent _onLeftActivation;
    [Header("Right Side")]
    [SerializeField] private UnityEvent _onRightActivation;

    private void Awake()
    {
        GetComponent<TransitionTunnel>().AddOnLeftActivation(_onLeftActivation.Invoke);
        GetComponent<TransitionTunnel>().AddOnRightActivation(_onRightActivation.Invoke);

        switch (_defaultSide)
        {
            case TransitionTunnelSide.Blue:
                _onLeftActivation.Invoke();
                break;
            case TransitionTunnelSide.Red:
                _onRightActivation.Invoke();
                break;
            default:
                break;
        }
    }
}