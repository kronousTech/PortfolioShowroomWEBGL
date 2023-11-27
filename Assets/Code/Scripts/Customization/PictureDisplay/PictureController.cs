using KronosTech.AssetManagement;
using KronosTech.Customization.Decoration;
using KronosTech.Customization.Pictures;
using KronosTech.Services;
using KronosTech.ShowroomGeneration;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace KronosTech.Customization.Pictures
{
    public static class PictureController
    {
        private static string _manifestURL = "https://raw.githubusercontent.com/kronousTech/Portfolio-WEBGL-PC/main/Content/Bundles/gallery/memes.manifest";

        private static readonly List<Sprite> _picturesList = new();
        private static readonly List<PictureDisplay> _displaysList = new();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            AssetsLoader.OnBundlesDownload += GetPictures;
            GenerateShowroom.OnGenerationEnd += (state) => SetPictures();
        }

        private static void GetPictures()
        {
            var temporaryObject = new GameObject("TemporaryCoroutineRunner");
            var temporaryRunner = temporaryObject.AddComponent<CoroutineRunner>();

            temporaryRunner.StartCoroutine(CallWebRequest.GetRequest(_manifestURL, (string data, string error) =>
            {
                if (!string.IsNullOrEmpty(error))
                {
                    Debug.LogError("Error getting bundles data: " + error);
                    return;
                }

                foreach (Match match in new Regex(@"- Assets/.*").Matches(data))
                {
                    var asset = new Asset(match.Value.Split('/')[^1], "memes", AssetCategory.gallery);

                    _picturesList.Add(ServiceLocator.Instance.GetWebImagesService().LoadImage(asset));
                }
            }));
        }
        private static void SetPictures()
        {
            var availablePictures = new List<Sprite>(_picturesList);

            foreach (var item in _displaysList)
            {
                var pictureIndex = Random.Range(0, availablePictures.Count);

                item.SetPicture(availablePictures[pictureIndex]);

                availablePictures.RemoveAt(pictureIndex);

                if(availablePictures.Count == 0)
                {
                    availablePictures = new List<Sprite>(_picturesList);
                }
            }
        }

        public static void Add(PictureDisplay model)
        {
            _displaysList.Add(model);
        }
        public static void Remove(PictureDisplay model)
        {
            _displaysList.Remove(model);
        }
    }
}