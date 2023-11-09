using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace KronosTech.Customization
{
    public class CustomizableDecal : MonoBehaviour
    {
        private DecalProjector _decal;

        private void OnEnable()
        {
            GalleryCustomization.AddCustomizableDecal(this);
        }
        private void OnDisable()
        {
            GalleryCustomization.RemoveCustomizableDecal(this);
        }
        private void Awake()
        {
            _decal = GetComponent<DecalProjector>();
        }

        public void ReplaceMaterial(Material material)
        {
            _decal.material = material;
        }
    }
}