using System;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private PlayerSound _step;
    [SerializeField] private PlayerSound _jumping;
    [SerializeField] private PlayerSound _landing;

    private void Awake()
    {
        _step.Init();
        _jumping.Init();
        _landing.Init();

        FindObjectOfType<PlayerSteps>().AddListener(PlayStepsSound);
        FindObjectOfType<FirstPersonController>().OnJump += PlayJumpingSound;
        FindObjectOfType<PlayerGroundCheck>().AddOnGroundStateChangeListener(PlayLandingSound);
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