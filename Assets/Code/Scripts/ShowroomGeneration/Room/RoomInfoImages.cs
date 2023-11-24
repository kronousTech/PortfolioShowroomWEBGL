using KronosTech.AssetManagement;
using KronosTech.Services;
using System;
using UnityEngine;

namespace KronosTech.ShowroomGeneration.Room
{
    public class RoomInfoImages : MonoBehaviour
    {
        private RoomImageSpriteData[] _imageSprites;

        [SerializeField] private ContentData[] _imageData;
        [SerializeField] private RoomDisplayImages[] _displays;

        private void OnEnable()
        {
            AssetsLoader.OnBundlesDownload += LoadImages;   
        }
        private void OnDisable()
        {
            AssetsLoader.OnBundlesDownload -= LoadImages;
        }

        private void LoadImages()
        {
            _imageSprites = new RoomImageSpriteData[_imageData.Length];

            var loadCount = 0;

            for (int i = 0; i < _imageData.Length; i++)
            {
                var sprite = ServiceLocator.Instance.GetWebImagesService().LoadImage(_imageData[i].asset);
                
                _imageSprites[i].title = _imageData[i].title;   
                _imageSprites[i].sprite = sprite;
                
                loadCount++;
                
                if(loadCount == _imageData.Length)
                {
                    AddSpritesToDisplays();
                }
            }
        }

        private void AddSpritesToDisplays()
        {
            for (int i = 0; i < _displays.Length; i++)
            {
                _displays[i].AddSprites(_imageSprites);
            }
        }
    }

    
    [Serializable]
    public struct RoomImageSpriteData
    {
        public string title;
        public Sprite sprite;
    }
}