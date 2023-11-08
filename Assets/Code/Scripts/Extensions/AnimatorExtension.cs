using UnityEngine;

public class AnimatorExtension : MonoBehaviour
{
    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    public void SetBoolTrue(string name) => _animator.SetBool(name, true);
    public void SetBoolFalse( string name) => _animator.SetBool(name, false);
}