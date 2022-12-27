using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private bool _isEnabled = true;
    [SerializeField] private PlayerSound _step;
    [SerializeField] private PlayerSound _jumping;
    [SerializeField] private PlayerSound _landing;

    private void Awake()
    {
        _step.Init();
        _jumping.Init();
        _landing.Init();

        if (_isEnabled)
            EnablePlayerSounds();
    }

    public void EnablePlayerSounds()
    {
        _isEnabled = true;
        FindObjectOfType<PlayerSteps>().AddListener(PlayStepsSound);
        FindObjectOfType<FirstPersonController>().OnJump += PlayJumpingSound;
        FindObjectOfType<PlayerGroundCheck>().AddOnGroundStateChangeListener(PlayLandingSound);
    }
    public void DisablePlayerSounds()
    {
        _isEnabled = false;
        FindObjectOfType<PlayerSteps>().RemoveListener(PlayStepsSound);
        FindObjectOfType<FirstPersonController>().OnJump -= PlayJumpingSound;
        FindObjectOfType<PlayerGroundCheck>().RemoveOnGroundStateChangeListener(PlayLandingSound);
    }
    public bool GetState()
    {
        return _isEnabled;
    }

    private void PlayStepsSound()
    {
        _step.Play();
    }
    private void PlayJumpingSound()
    {
        if (!_step.IsPlaying())
        {
            _jumping.Play();
            //_step.Stop();
        }
    }
    private void PlayLandingSound(bool state)
    {
        if(state)
            _landing.Play();
    }

    [Serializable]
    private class PlayerSound
    {
        public AudioClip clip;
        public AudioSource audioSource;
        public float volume;

        public void Init()
        {
            audioSource.clip = clip;
            audioSource.volume = volume;
        }
        public void Play()
        {
            var pitch = Random.Range(0.75f, 1f);
            audioSource.pitch = pitch;
            audioSource.Play();
        }
        public bool IsPlaying()
        {
            return audioSource.isPlaying;
        }
        public void Stop()
        {
            audioSource.Stop();
        }
    }
}