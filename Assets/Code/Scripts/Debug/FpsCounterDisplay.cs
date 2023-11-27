using TMPro;
using UnityEngine;

public class FpsCounterDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Color _low;
    [SerializeField] private Color _medium;
    [SerializeField] private Color _high;

    private const float INTERVAL = 0.5f;

    private float accum = 0.0f;
    private int frames = 0;
    private float timeleft;

    private void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        frames++;

        // Interval ended - update GUI text and start new interval
        if (timeleft <= 0.0)
        {
            var value = (int)(accum / frames);
            
            if (value <= 15)
            {
                _text.color = _low;
            }
            else if(value <= 45)
            {
                _text.color = _medium;
            }
            else
            {
                _text.color = _high;
            }

            _text.text = value.ToString();

            // Reset variables
            timeleft = INTERVAL;
            accum = 0.0f;
            frames = 0;
        }
    }
}