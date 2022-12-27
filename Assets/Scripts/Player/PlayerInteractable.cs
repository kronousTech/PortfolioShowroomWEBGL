using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    private Transform _cameraTransform;
    [SerializeField] private float _interactDistance;

    private readonly string _interactableTag = "Interactable";

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }
    private void Update()
    {
        if(Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out var hit, _interactDistance))
        {
            if (hit.transform.CompareTag(_interactableTag))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.E))
                {
                    try
                    {
                        Debug.Log("Click");
                        hit.transform.GetComponent<IInteractable>().OnInteract();
                    }
                    catch
                    {
                        Debug.LogWarning("Interactable doesnt have interact script");
                    }
                }
            }
        }
    }
}