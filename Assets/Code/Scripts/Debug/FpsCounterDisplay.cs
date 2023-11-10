using TMPro;
using UnityEngine;

public class FpsCounterDisplay : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        InvokeRepeating(nameof(UpdateFps), 0.5f, 0.5f);
    }

    private void UpdateFps()
    {
        _text.text = "FPS: " + (int)(1f / Time.unscaledDeltaTime);
    }
}
