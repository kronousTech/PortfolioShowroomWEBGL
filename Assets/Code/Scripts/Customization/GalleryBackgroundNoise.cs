using System;
using UnityEngine;

public class GalleryBackgroundNoise : MonoBehaviour
{
    private AudioSource _source;

    private static event Action<AudioClip> OnNewAudioClipRequest;

    private void OnEnable()
    {
        OnNewAudioClipRequest += ReplaceAudio;
    }
    private void OnDisable()
    {
        OnNewAudioClipRequest -= ReplaceAudio;
    }
    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public static void RequestNewAudioClip(AudioClip audioClip) => OnNewAudioClipRequest?.Invoke(audioClip);

    private void ReplaceAudio(AudioClip clip)
    {
        _source.clip = clip;
        _source.Play();
    }
}