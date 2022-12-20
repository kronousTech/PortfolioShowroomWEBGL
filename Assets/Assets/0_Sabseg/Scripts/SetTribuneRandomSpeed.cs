using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PortfolioProject.Sabseg
{
    [RequireComponent(typeof(Animator))]
    public class SetTribuneRandomSpeed : MonoBehaviour
    {
        private const float _maxSpeed = 0.7f;
        private const float _minSpeed = 0.1f;

        private void Awake()
        {
            var speed = Random.Range(_minSpeed, _maxSpeed);
            GetComponent<Animator>().speed = speed;
        }
    }
}