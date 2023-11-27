using UnityEngine;

namespace KronosTech.Customization.Decoration
{
    public class DecorationModel : MonoBehaviour
    {
        private void Awake()
        {
            DecorationController.Add(this);
        }
        private void OnDestroy()
        {
            DecorationController.Remove(this);
        }
    }
}