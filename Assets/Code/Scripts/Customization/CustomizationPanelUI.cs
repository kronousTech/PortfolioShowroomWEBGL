using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Customization
{
    public class CustomizationPanelUI : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private MaterialButtonDisplay _materialButtonDisplayPrefab;
        [Header("Parents")]
        [SerializeField] private ToggleGroup _floorParent;
        [SerializeField] private ToggleGroup _baseboardParent;
        [SerializeField] private ToggleGroup _wallParent;
        [SerializeField] private ToggleGroup _guidelinesParent;
        [SerializeField] private ToggleGroup _backgroundParent;

        private void OnEnable()
        {
            GalleryCustomization.OnAddFloorMaterials += (materials) => AddButtons(CustomizableElement.Floor, _floorParent, materials);
            GalleryCustomization.OnAddBaseboardMaterials += (materials) => AddButtons(CustomizableElement.Baseboard, _baseboardParent, materials);
            GalleryCustomization.OnAddWallMaterials += (materials) => AddButtons(CustomizableElement.Wall, _wallParent, materials);
            GalleryCustomization.OnAddGuidelineMaterials += (materials) => AddButtons(CustomizableElement.Guideline, _guidelinesParent, materials);

            GalleryEnvironment.OnAddSkyboxMaterials += (materials) => AddButtons(_backgroundParent, materials);
        }
        private void OnDisable()
        {
            GalleryCustomization.OnAddFloorMaterials -= (materials) => AddButtons(CustomizableElement.Floor, _floorParent, materials);
            GalleryCustomization.OnAddBaseboardMaterials -= (materials) => AddButtons(CustomizableElement.Baseboard, _baseboardParent, materials);
            GalleryCustomization.OnAddWallMaterials -= (materials) => AddButtons(CustomizableElement.Wall, _wallParent, materials);
            GalleryCustomization.OnAddGuidelineMaterials -= (materials) => AddButtons(CustomizableElement.Guideline, _guidelinesParent, materials);

            GalleryEnvironment.OnAddSkyboxMaterials -= (materials) => AddButtons(_backgroundParent, materials);
        }

        private void AddButtons(CustomizableElement element, ToggleGroup parent, Material[] materials)
        {
            for (int i = 0; i < materials.Length; i++)
            {
                var material = materials[i];
                var toggle = Instantiate(_materialButtonDisplayPrefab, parent.transform);
                toggle.Initialize(material);
                if(toggle.TryGetComponent<Toggle>(out var toggleComponent))
                {
                    toggleComponent.group = parent;
                    toggleComponent.onValueChanged.AddListener((value)
                    => { if (value) GalleryCustomization.SetNewCurrentMat(element, material); });
                }
            }
        }
        private void AddButtons(ToggleGroup parent, Material[] materials)
        {
            for (int i = 0; i < materials.Length; i++)
            {
                var index = i;
                var toggle = Instantiate(_materialButtonDisplayPrefab, parent.transform);
                toggle.Initialize(materials[i]);
                if (toggle.TryGetComponent<Toggle>(out var toggleComponent))
                {
                    toggleComponent.group = parent;
                    toggleComponent.onValueChanged.AddListener((value)
                    => { if (value) GalleryEnvironment.ReplaceEnvironment(index); });
                }
            }
        }
    }
}