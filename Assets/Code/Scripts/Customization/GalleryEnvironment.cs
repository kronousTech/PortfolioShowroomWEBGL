using System;
using UnityEngine;

public static class GalleryEnvironment
{
    private static Material[] _skyboxes;
    private static AudioClip[] _audioClips;

    public static Action<Material[]> OnAddSkyboxMaterials;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        _skyboxes = Resources.LoadAll<Material>("GalleryCustomization/Skybox");
        _audioClips = Resources.LoadAll<AudioClip>("GalleryCustomization/EnvironmentSounds");

        ReplaceEnvironment(0);
    }
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void AfterInitialize()
    {
        OnAddSkyboxMaterials?.Invoke(_skyboxes);
    }

    public static void ReplaceEnvironment(int index)
    {
        RenderSettings.skybox = _skyboxes[index];

        GalleryBackgroundNoise.RequestNewAudioClip(_audioClips[index]);

        DynamicGI.UpdateEnvironment();
    }
}
