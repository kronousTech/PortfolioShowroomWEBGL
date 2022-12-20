using System;
using UnityEngine;

namespace Transitions
{
    public enum TunnelDirection
    {
        Z,
        X
    }

    public enum TransitionTunnelSide
    {
        Blue,
        Red
    }
    public class TransitionTunnel : MonoBehaviour
    {
        private readonly string _playerTag = "Player";
        private Transform _player;
        [SerializeField] private TunnelDirection _direction;
        [SerializeField] private TransitionTunnelSide _closestSide;
        private TransitionTunnelSide _oldClosestSide;
        [SerializeField] private bool _isTransitioning;

        private Transform _leftSide;
        private Transform _rightSide;

        [SerializeField] private float _distanceFromLeft;
        [SerializeField] private float _distanceFromRight;
        [SerializeField] private float _effect;

        private Action<float> _onLeftEffect = new((e) => { });
        private Action _onLeftActivation = new(() => { });
        private Action _onLeftDeactivation = new(() => { });
        private Action<float> _onRightEffect = new((e) => { });
        private Action _onRightActivation = new(() => { });
        private Action _onRightDeactivation = new(() => { });

        private void Awake()
        {
            _leftSide = transform.GetChild(0);
            _rightSide = transform.GetChild(1);

            _oldClosestSide = _closestSide;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_playerTag))
            {
                _isTransitioning = true;
                _player = other.transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_playerTag))
            {
                _isTransitioning = false;
                _player = null;

                switch (_closestSide)
                {
                    case TransitionTunnelSide.Blue:
                        _onLeftEffect?.Invoke(1f);
                        _onRightEffect?.Invoke(0f);
                        break;
                    case TransitionTunnelSide.Red:
                        _onLeftEffect?.Invoke(0f);
                        _onRightEffect?.Invoke(1f);
                        break;
                }
            }
        }

        private void Update()
        {
            if (_isTransitioning)
            {
                // Get effect value
                var distance = Mathf.Abs(Vector3.Distance(_leftSide.position, _rightSide.position));
                var distanceToCenter = distance / 2.5f;
                var centerPos = _leftSide.position + 0.5f * (_rightSide.position - _leftSide.position);
                _effect = Mathf.Clamp(GetSideDistance(centerPos, _player.position), 0, distanceToCenter) / distanceToCenter;

                // Side change
                //_distanceFromLeft = 1f - (Mathf.Clamp(GetSideDistance(_leftSide.position, _player.position), 0, distanceToCenter) / distanceToCenter);
                //_distanceFromRight = 1f - (Mathf.Clamp(GetSideDistance(_rightSide.position, _player.position), 0, distanceToCenter) / distanceToCenter);

                _closestSide = GetSideDistance(_leftSide.position, _player.position) < GetSideDistance(_rightSide.position, _player.position) ?
                    TransitionTunnelSide.Blue : TransitionTunnelSide.Red;

                if(_closestSide != _oldClosestSide)
                {
                    _oldClosestSide = _closestSide;

                    switch (_closestSide)
                    {
                        case TransitionTunnelSide.Blue:
                            _onLeftEffect?.Invoke(1f);
                            _onRightEffect?.Invoke(0f);
                            _onLeftActivation?.Invoke();
                            _onRightDeactivation?.Invoke();
                            break;
                        case TransitionTunnelSide.Red:
                            _onLeftEffect?.Invoke(0f);
                            _onRightEffect?.Invoke(1f);
                            _onRightActivation?.Invoke();
                            _onLeftDeactivation?.Invoke();
                            break;
                    }
                }

                // Side effect
                switch (_closestSide)
                {
                    case TransitionTunnelSide.Blue:
                        _onLeftEffect?.Invoke(_effect);
                        break;
                    case TransitionTunnelSide.Red:
                        _onRightEffect?.Invoke(_effect);
                        break;
                }
            }
        }

        private static Vector3 GetZeroYPosition(Vector3 position)
        {
            return new Vector3(position.x, 0, position.z);
        }
        private float GetSideDistance(Vector3 side, Vector3 target)
        {
            var sidePos = new Vector3(_direction == TunnelDirection.Z ? target.x : side.x,
                0,
                _direction == TunnelDirection.X ? target.z : side.z);

            return Vector3.Distance(GetZeroYPosition(target), sidePos);
        }

        public void AddOnLeftEffect(Action<float> listener) => _onLeftEffect += listener;
        public void AddOnRightEffect(Action<float> listener) => _onRightEffect += listener;
        public void AddOnLeftActivation(Action listener) => _onLeftActivation += listener;
        public void AddOnRightActivation(Action listener) => _onRightActivation += listener;
        public void AddOnLeftDeactivation(Action listener) => _onLeftDeactivation += listener;
        public void AddOnRightDeactivation(Action listener) => _onRightDeactivation += listener;
    }
}