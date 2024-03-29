using Core.Player;
using UnityEngine;

namespace Ui.Hud
{
    public class CrosshairDisplay : MonoBehaviour
    {
        private void Awake()
        {
            GameObject.FindObjectOfType<PlayerInteractableObject>().OnLookedAtInteractableObject += SetCrossairState;
        }

        private void SetCrossairState(bool lookingAtInteractable)
        {
            GetComponent<Animator>().SetBool("Looking At Interactable", lookingAtInteractable);
        }
    }

}