using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ui.Hud
{
    public class CrosshairDisplay : MonoBehaviour
    {
        private void Awake()
        {
            GameObject.FindObjectOfType<PlayerInteractable>().OnLookingAtInteractable += SetCrossairState;
        }

        private void SetCrossairState(bool lookingAtInteractable)
        {
            GetComponent<Animator>().SetBool("Looking At Interactable", lookingAtInteractable);
        }
    }

}