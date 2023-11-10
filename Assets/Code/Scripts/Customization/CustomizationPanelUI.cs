using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Customization
{
    public class CustomizationPanelUI : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private MaterialButtonDisplay _materialButtonDisplayPrefab;
        [Header("Parents")]
        [SerializeField] private Transform _floorParent;
        [SerializeField] private Transform _baseboardParent;
        [SerializeField] private Transform _wallParent;
        [SerializeField] private Transform _guidelinesParent;
        [SerializeField] private Transform _backgroundParent;

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

        private void AddButtons(CustomizableElement element, Transform parent, Material[] materials)
        {
            for (int i = 0; i < materials.Length; i++)
            {
                var material = materials[i];
                var button = Instantiate(_materialButtonDisplayPrefab, parent);
                button.Initialize(material);
                button.GetComponent<Button>().onClick.AddListener(() 
                    => GalleryCustomization.SetNewCurrentMat(element, material));
            }
        }
        private void AddButtons(Transform parent, Material[] materials)
        {
            for (int i = 0; i < materials.Length; i++)
            {
                var index = i;
                var button = Instantiate(_materialButtonDisplayPrefab, parent);
                button.Initialize(materials[i]);
                button.GetComponent<Button>().onClick.AddListener(()
                    => GalleryEnvironment.ReplaceEnvironment(index));
            }
        }
    }
}