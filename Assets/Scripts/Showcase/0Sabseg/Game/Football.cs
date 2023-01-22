using System;
using UnityEngine;

namespace Showcase.Sabseg
{
    public class Football : MonoBehaviour, IInteractable
    {
        private GameObject _playerOrientation;
        private Rigidbody _rigidbody;
        private ScoreController _scoreController;

        private Vector3 _startingPosition;

        [SerializeField] private float _sideDistance;
        [SerializeField] private float forwardKickForce;
        [SerializeField] private float upKickForce;

        private void Awake()
        {
            _playerOrientation = GameObject.Find("Orientation");
            _rigidbody = GetComponent<Rigidbody>();
            _scoreController = FindObjectOfType<ScoreController>();
            _startingPosition = transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            // Contact with one of the gates
            if(other.name == "Red" || other.name == "Blue")
            {
                _scoreController.AddPoint((FootballTeam)Enum.Parse(typeof(FootballTeam), other.name));
                StopBallAt(_startingPosition);
            }
            // Sontact with one of the sides
            else if (other.name == "Side")
            {
                var newPosition = other.ClosestPoint(transform.position) + (other.transform.forward * _sideDistance);
                StopBallAt(newPosition);
            }
            else if(other.name == "Gate Sides")
            {
                var newPosition = other.transform.GetChild(0).position;
                StopBallAt(newPosition);
            }
        }

        public void OnInteract()
        {
            var kickDirection = _playerOrientation.transform.forward;

            GetComponent<AudioSource>().Play();

            _rigidbody.AddForce(kickDirection.normalized * forwardKickForce, ForceMode.Impulse);
            _rigidbody.AddForce(Vector3.up * upKickForce, ForceMode.Impulse);
        }

        private void StopBallAt(Vector3 position)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            transform.position = position;
        }
    }
}