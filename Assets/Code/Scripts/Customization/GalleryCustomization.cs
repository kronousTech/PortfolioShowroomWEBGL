using System;
using System.Collections.Generic;
using UnityEngine;

namespace KronosTech.Customization
{
    public enum CustomizableElement
    {
        Floor = 0,
        Baseboard = 1,
        Wall = 2,
    }

    public class GalleryCustomization
    {
        private static readonly Dictionary<CustomizableElement, Material[]> _materialsDict = new();
        private static readonly Dictionary<CustomizableElement, Material> _selectedMaterialsDict = new();
        private static readonly List<CustomizableRenderer> _renderers = new();

        public static Action<Material[]> OnAddFloorMaterials;
        public static Action<Material[]> OnAddBaseboardMaterials;
        public static Action<Material[]> OnAddWallMaterials;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            _materialsDict.Add(CustomizableElement.Floor, Resources.LoadAll<Material>("GalleryCustomization/Floor"));
            _materialsDict.Add(CustomizableElement.Baseboard, Resources.LoadAll<Material>("GalleryCustomization/Baseboard"));
            _materialsDict.Add(CustomizableElement.Wall, Resources.LoadAll<Material>("GalleryCustomization/Wall"));

            _selectedMaterialsDict.Add(CustomizableElement.Floor, _materialsDict[CustomizableElement.Floor][0]);
            _selectedMaterialsDict.Add(CustomizableElement.Baseboard, _materialsDict[CustomizableElement.Baseboard][0]);
            _selectedMaterialsDict.Add(CustomizableElement.Wall, _materialsDict[CustomizableElement.Wall][0]);
        }
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void AfterInitialize() 
        {
            OnAddFloorMaterials?.Invoke(_materialsDict[CustomizableElement.Floor]);
            OnAddBaseboardMaterials?.Invoke(_materialsDict[CustomizableElement.Baseboard]);
            OnAddWallMaterials?.Invoke(_materialsDict[CustomizableElement.Wall]);
        }

        public static void AddCustomizableRenderer(CustomizableRenderer renderer)
        {
            _renderers.Add(renderer);

            foreach (var item in _selectedMaterialsDict)
            {
                UpdateMat(item.Key, item.Value);
            }
        }
        public static void RemoveCustomizableRenderer(CustomizableRenderer renderer)
        {
            if (_renderers.Contains(renderer))
            {
                _renderers.Remove(renderer);
            }
        }

        private static void UpdateMat(CustomizableElement element, Material material)
        {
            foreach (var item in _renderers)
            {
                item.ReplaceMaterial(element.ToString(), material);
            }
        }

        public static void SetNewCurrentMat(CustomizableElement element, Material material)
        {
            _selectedMaterialsDict[element] = material;

            UpdateMat(element, _selectedMaterialsDict[element]);
        }
        public static void SetNewCurrentMat(CustomizableElement element, int index)
        {
            _selectedMaterialsDict[element] = _materialsDict[element][index];

            UpdateMat(element, _selectedMaterialsDict[element]);
        }
    }
}