using UnityEngine;
using UnityEngine.UI;

public class ToggleFog : MonoBehaviour
{
    private Toggle _toggle;

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener((value) => RenderSettings.fog = value);
    }
    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener((value) => RenderSettings.fog = value);
    }
    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }
}
