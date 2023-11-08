using KronosTech.InputSystem;
using Sounds.PlayerSounds;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _walkingSource;
    [SerializeField] private AudioSource _jumpingSource;
    [SerializeField] private AudioSource _landingSource;

    private void Awake()
    {
        ChangeSoundPack(GroundType.Wood);
    }
    private void OnEnable()
    {
        PlayerSteps.OnStep += PlayStepsSound;
        StarterAssetsInputs.OnJumpEvent += PlayJumpingSound;
        PlayerGrounded.OnGroundedEvent += PlayLandingSound;
    }
    private void OnDisable()
    {
        PlayerSteps.OnStep -= PlayStepsSound;
        StarterAssetsInputs.OnJumpEvent -= PlayJumpingSound;
        PlayerGrounded.OnGroundedEvent -= PlayLandingSound;
    }
    public void ChangeSoundPack(GroundType type) 
    {
        var pack = Resources.Load<PlayerSoundScriptableObject>("SoundPacks/PlayerSounds/PlayerSoundPack - " + type.ToString());
        if(pack == null) 
        {
            Debug.Log("Failed to get sound pack");
            return;
        }
    
        _walkingSource.clip = pack.walking.clip;
        _walkingSource.volume = pack.walking.volume;
    
        _jumpingSource.clip = pack.jumping.clip;
        _jumpingSource.volume = pack.jumping.volume;
    
        _landingSource.clip = pack.landing.clip;
        _landingSource.volume = pack.landing.volume;
    }
    
    private void PlayStepsSound()
    {
        PlaySound(_walkingSource);
    }
    private void PlayJumpingSound()
    {
        if (!_walkingSource.isPlaying)
        {
            PlaySound(_jumpingSource);
        }
    }
    private void PlayLandingSound(bool state)
    {
        if(state)
            PlaySound(_landingSource);
    }
    private void PlaySound(AudioSource source) 
    {
        var pitch = Random.Range(0.75f, 1f);
        source.pitch = pitch;
        source.Play();
    }
}