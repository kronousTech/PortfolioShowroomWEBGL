using System.Collections;
using System.Collections.Generic;
using Transitions;
using UnityEngine;

public class IdleSoundTransition : MonoBehaviour
{
    [SerializeField] private TransitionTunnel _tunnel;
    private AudioSource _audioSource;
    private float _maxVolume;

    private void Awake()
    {
        _tunnel.AddOnRightEffect(SetVolume);
        _audioSource = GetComponent<AudioSource>();
        _maxVolume = _audioSource.volume;
    }


    private void SetVolume(float effect)
    {
        GetComponent<AudioSource>().volume = effect * _maxVolume;
    }
}
