using System;
using UnityEngine;

namespace Showcase.Sabseg
{
    public enum FootballTeam
    {
        Red = 0,
        Blue = 1
    }
    public class ScoreController : MonoBehaviour
    {
        public event Action<int, int> OnScore;
        private int[] _score = new int[2];

        [SerializeField] private AudioSource _winSound;

        public void AddPoint(FootballTeam team)
        {
            _score[((int)team)]++;
            _winSound.Play();
            OnScore?.Invoke(_score[0], _score[1]);
        }
    }
}