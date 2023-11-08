using UnityEngine;

namespace KronosTech.Customization
{
    public class CustomizableRenderer : MonoBehaviour
    {
        private MeshRenderer _renderer;

        private void OnEnable()
        {
            GalleryCustomization.AddCustomizableRenderer(this);
        }
        private void OnDisable()
        {
            GalleryCustomization.RemoveCustomizableRenderer(this);
        }
        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public void ReplaceMaterial(string materialName, Material material)
        {
            var index = -1;

            for (int i = 0; i < _renderer.materials.Length; i++)
            {
                if (_renderer.materials[i].name.Contains(materialName))
                {
                    index = i;
                    break;
                }
            }

            // Found mat
            if (index > -1)
            {
                var newMats = _renderer.sharedMaterials;
                newMats[index] = material;

                _renderer.materials = newMats;
            }
        }
    }
}