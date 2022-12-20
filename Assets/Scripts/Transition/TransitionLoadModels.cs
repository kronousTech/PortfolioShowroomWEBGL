using System.Collections.Generic;
using UnityEngine;

namespace Transitions
{
    [RequireComponent(typeof(TransitionTunnel))]
    public class TransitionLoadModels : MonoBehaviour
    {
        [SerializeField] private ClosestSide _defaultSide;
        [SerializeField] private List<GameObject> _onLeftActivation;
        [SerializeField] private List<GameObject> _onRightActivation;

        private void Awake()
        {
            GetComponent<TransitionTunnel>().AddOnLeftActivation(LeftActivation);
            GetComponent<TransitionTunnel>().AddOnRightActivation(RightActivation);
            GetComponent<TransitionTunnel>().AddOnLeftDeactivation(LeftDeactivation);
            GetComponent<TransitionTunnel>().AddOnRightDeactivation(RightDeactivation);

            switch (_defaultSide)
            {
                case ClosestSide.Left:
                    LeftActivation();
                    RightDeactivation();
                    break;
                case ClosestSide.Right:
                    RightActivation();
                    LeftDeactivation();
                    break;
                default:
                    break;
            }
        }

        private void LeftActivation() => SetModelState(_onLeftActivation, true);
        private void RightActivation() => SetModelState(_onRightActivation, true);
        private void LeftDeactivation() => SetModelState(_onLeftActivation, false);
        private void RightDeactivation() => SetModelState(_onRightActivation, false);
        
        private void SetModelState(List<GameObject> models, bool state)
        {
            if (models == null)
                return;

            foreach (var model in models)
            {
                foreach (var meshRenderer in model.GetComponentsInChildren<MeshRenderer>())
                {
                    meshRenderer.enabled = state;
                }
                foreach (var skinnedMeshRenderer in model.GetComponentsInChildren<SkinnedMeshRenderer>())
                {
                    skinnedMeshRenderer.enabled = state;
                }
            }
        }
    }
}