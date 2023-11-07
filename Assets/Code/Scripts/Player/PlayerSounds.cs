using Sounds.PlayerSounds;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    //[SerializeField] private bool _isEnabled = true;
    //[SerializeField] private PlayerSoundScriptableObject _pack;
    //[SerializeField] private AudioSource _walkingSource;
    //[SerializeField] private AudioSource _jumpingSource;
    //[SerializeField] private AudioSource _landingSource;
    //
    //private void Awake()
    //{
    //    ChangeSoundPack(GroundType.Wood);
    //
    //    if (_isEnabled)
    //        EnablePlayerSounds();
    //}
    //private void OnEnable()
    //{
    //    GetComponent<PlayerGroundCheck>().OnGroundTypeChange += ChangeSoundPack;
    //}
    //private void OnDisable()
    //{
    //    GetComponent<PlayerGroundCheck>().OnGroundTypeChange -= ChangeSoundPack;
    //}
    //
    //public void EnablePlayerSounds()
    //{
    //    _isEnabled = true;
    //    FindObjectOfType<PlayerSteps>().AddListener(PlayStepsSound);
    //    FindObjectOfType<FirstPersonController>().OnJump += PlayJumpingSound;
    //    FindObjectOfType<PlayerGroundCheck>().AddOnGroundStateChangeListener(PlayLandingSound);
    //}
    //public void DisablePlayerSounds()
    //{
    //    _isEnabled = false;
    //    FindObjectOfType<PlayerSteps>().RemoveListener(PlayStepsSound);
    //    FindObjectOfType<FirstPersonController>().OnJump -= PlayJumpingSound;
    //    FindObjectOfType<PlayerGroundCheck>().RemoveOnGroundStateChangeListener(PlayLandingSound);
    //}
    //public void ChangeSoundPack(GroundType type) 
    //{
    //    _pack = Resources.Load<PlayerSoundScriptableObject>("SoundPacks/PlayerSounds/PlayerSoundPack - " + type.ToString());
    //    if(_pack == null) 
    //    {
    //        Debug.Log("Failed to get sound pack");
    //        return;
    //    }
    //
    //    _walkingSource.clip = _pack.walking.clip;
    //    _walkingSource.volume = _pack.walking.volume;
    //
    //    _jumpingSource.clip = _pack.jumping.clip;
    //    _jumpingSource.volume = _pack.jumping.volume;
    //
    //    _landingSource.clip = _pack.landing.clip;
    //    _landingSource.volume = _pack.landing.volume;
    //}
    //public bool GetState()
    //{
    //    return _isEnabled;
    //}
    //
    //private void PlayStepsSound()
    //{
    //    PlaySound(_walkingSource);
    //}
    //private void PlayJumpingSound()
    //{
    //    if (!_walkingSource.isPlaying)
    //    {
    //        PlaySound(_jumpingSource);
    //    }
    //}
    //private void PlayLandingSound(bool state)
    //{
    //    if(state)
    //        PlaySound(_landingSource);
    //}
    //private void PlaySound(AudioSource source) 
    //{
    //    var pitch = Random.Range(0.75f, 1f);
    //    source.pitch = pitch;
    //    source.Play();
    //}
}