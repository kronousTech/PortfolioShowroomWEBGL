using UnityEngine;

public class Football : MonoBehaviour, IInteractable
{
    private GameObject _playerOrientation;
    private Rigidbody _rigidbody;
    [SerializeField] private float forwardKickForce;
    [SerializeField] private float upKickForce;

    private void Awake()
    {
        _playerOrientation = GameObject.Find("Orientation");
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnInteract()
    {
        var kickDirection = _playerOrientation.transform.forward;

        _rigidbody.AddForce(kickDirection.normalized * forwardKickForce, ForceMode.Impulse);
        _rigidbody.AddForce(Vector3.up * upKickForce, ForceMode.Impulse);
    }
}