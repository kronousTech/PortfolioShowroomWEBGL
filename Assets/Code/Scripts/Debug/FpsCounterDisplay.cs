using TMPro;
using UnityEngine;

public class FpsCounterDisplay : MonoBehaviour
{
    private void Start()
    {
        InvokeRepeating(nameof(UpdateFps), 0.5f, 0.5f);
    }

    private void UpdateFps()
    {
        GetComponent<TextMeshProUGUI>().text = "FPS: " + (int)(1f / Time.unscaledDeltaTime);
    }
}
