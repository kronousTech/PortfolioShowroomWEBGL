using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderViewDistance : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _fieldOfViewText;
    private CinemachineVirtualCamera _camera;
    private Slider _slider;

    private void Awake()
    {
        _camera = FindObjectOfType<CinemachineVirtualCamera>();
        if (_camera == null)
        {
            Debug.LogError("Didn't found Camera on FieldOfViewSlider");
            return;
        }
        _slider = GetComponent<Slider>();   
    }
    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(UpdateText);
        _slider.onValueChanged.AddListener((value) => _camera.m_Lens.FarClipPlane = value);
    }
    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(UpdateText);
        _slider.onValueChanged.RemoveListener((value) => _camera.m_Lens.FarClipPlane = value);
    }

    private void UpdateText(float value)
    {
        _fieldOfViewText.text = ((int)value).ToString();
    }
}