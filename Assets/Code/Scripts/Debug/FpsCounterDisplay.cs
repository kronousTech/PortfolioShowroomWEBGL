using TMPro;
using UnityEngine;

public class FpsCounterDisplay : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private const float INTERVAL = 0.5f;

    private float accum = 0.0f;
    private int frames = 0;
    private float timeleft;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        frames++;

        // Interval ended - update GUI text and start new interval
        if (timeleft <= 0.0)
        {
            _text.text = "FPS: " + (int)(accum / frames);

            // Reset variables
            timeleft = INTERVAL;
            accum = 0.0f;
            frames = 0;
        }
    }
}