using TMPro;
using UnityEngine;

namespace Showcase.Sabseg
{
    public class ScoreDisplay : MonoBehaviour
    {
        private ScoreController _controller;
        private TextMeshProUGUI _screen;

        private void Awake()
        {
            _controller = GameObject.FindObjectOfType<ScoreController>();
            _screen = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }
        private void Start()
        {
            _controller.OnScore += SetScore;

            SetScore(0, 0);
        }

        private void SetScore(int redTeamScore, int blueTeamScore)
        {
            _screen.text = "<color=red>" + redTeamScore + "</color> - <color=blue>" + blueTeamScore;
        }
    }
}