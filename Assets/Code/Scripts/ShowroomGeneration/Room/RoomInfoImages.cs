using KronosTech.Services;
using System;
using UnityEngine;

namespace KronosTech.ShowroomGeneration.Room
{
    public class RoomInfoImages : MonoBehaviour
    {
        [SerializeField] private RoomImageData[] _imageData;
        [SerializeField] private RoomDisplayImages[] _displays;

        private RoomImageSpriteData[] _imageSprites;

        private void Start()
        {
            _imageSprites = new RoomImageSpriteData[_imageData.Length];

            var loadCount = 0;

            for (int i = 0; i < _imageData.Length; i++)
            {
                var index = i;

                ServiceLocator.Instance.GetWebImagesService().GetImageSprite(_imageData[index].url, this, (Sprite sprite, string error) =>
                {
                    if (string.IsNullOrEmpty(error))
                    {
                        _imageSprites[index].title = _imageData[index].title;   
                        _imageSprites[index].sprite = sprite;

                        loadCount++;
                    }
                    else
                    {
                        Debug.Log("Error loading sprite");
                    }

                    if(loadCount == _imageData.Length)
                    {
                        AddSpritesToDisplays();
                    }
                });
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
    public struct RoomImageData
    {
        public string title;
        [TextArea(1,5)] public string url;
    }
    [Serializable]
    public struct RoomImageSpriteData
    {
        public string title;
        public Sprite sprite;
    }
}