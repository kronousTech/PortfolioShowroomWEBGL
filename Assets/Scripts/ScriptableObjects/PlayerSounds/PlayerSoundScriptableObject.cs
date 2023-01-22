using System;
using UnityEngine;

namespace Sounds.PlayerSounds 
{
    [CreateAssetMenu(fileName = "PlayerSoundPack", menuName = "ScriptableObjects/PlayerSounds", order = 1)]
    public class PlayerSoundScriptableObject : ScriptableObject
    {
        public PlayerSound walking;
        public PlayerSound jumping;
        public PlayerSound landing;
    }

    [Serializable]
    public class PlayerSound
    {
        public AudioClip clip;
        public float volume;
    }
}