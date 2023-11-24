using KronosTech.AssetManagement;
using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace KronosTech.Services
{
    public static class CallAssetBundle
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            var bundlesFolderPath = Application.persistentDataPath + "/bundles/";
            if (!File.Exists(bundlesFolderPath))
            {
                Directory.CreateDirectory(bundlesFolderPath);
            }
        }
        
        public static T LoadAsset<T>(Asset asset) where T : UnityEngine.Object
        {
            var directory = GetBundlePath(asset.bundle, asset.category);
        
            if (File.Exists(directory))
            {
                var bundle = AssetBundle.LoadFromFile(directory);
                var bundle_asset = bundle.LoadAsset<T>(asset.name);
        
                if (bundle_asset == null)
                {
                    Debug.LogError("SERVICE REQUEST BUNDLE: Error loading bundle asset at directory: " 
                        + asset.name + " - " +  asset.bundle + " - " + directory);
                }
        
                bundle.Unload(false);
        
                return bundle_asset;
            }
            else
            {
                Debug.LogError("SERVICE REQUEST BUNDLE: Asset bundle not found at directory: " + directory);
        
                return null;
            }
        }
        
        public static void DownloadBundle(string bundleName, AssetCategory category, MonoBehaviour mono, Action<string> callback)
        {
            var bundlePath = GetBundlePath(bundleName, category);
            var categoryPath = GetCategoryFolderPath(category);
            var versionFilePath = GetBundlePath(bundleName, category) + ".version";
            var bundleVersion = string.Empty;

            // Current bundle version
            if (File.Exists(versionFilePath))
            {
                var sr = new StreamReader(versionFilePath);
                bundleVersion = GetBundleVersion(sr.ReadToEnd());
                sr.Close();

                // If already downloaded return
                Debug.Log("<color=green>SERVICE REQUEST BUNDLE: Manifest in cache, version: " + bundleVersion + "</color>");
            }
            else
            {
                // Create category folder
                Directory.CreateDirectory(categoryPath);
            }

            //Read downloadable bundle version.
            mono.StartCoroutine(CallWebRequest.GetRequest(GetDownloadableBundleManifestPath(category, bundleName), (string data, string error) =>
            {
                if(!string.IsNullOrEmpty(error))
                {
                    callback.Invoke(error);
                    return;
                }

                var regex = new Regex(@"BundleVersion:\s+(\d+)");
                var match = regex.Match(data);
                var version = match.Groups[1].Value;

                 if (version != bundleVersion)
                 {
                        // Delete old version file
                        if (!string.IsNullOrEmpty(bundleVersion))
                        {
                            // DELETE OLD VERSION FILE
                            File.Delete(versionFilePath);
                            // DELETE OLD BUNDLE
                            File.Delete(bundlePath);

                            Debug.Log("<color=green>SERVICE REQUEST BUNDLE: Deleted old bundle and manifest. Old Bundle version: </color>" + bundleVersion);
                        }

                        var startingTime = DateTime.Now;
                        mono.StartCoroutine(CallWebRequest.GetRequestAssetBundle(GetDownloadableBundlePath(category, bundleName), (byte[] data, string error) =>
                        {
                            if (string.IsNullOrEmpty(error))
                            {
                                // Save the AssetBundle to cache
                                File.WriteAllBytes(bundlePath, data);

                                // Create version file
                                CreateVersionFile(versionFilePath, version);

                                Debug.Log("<color=green>SERVICE REQUEST BUNDLE: Asset bundle downloaded successfully in </color>" +
                                    (DateTime.Now - startingTime));
                            }
                            else
                            {
                                Debug.LogError("SERVICE REQUEST BUNDLE: Error downloading bundle: " + bundleName + " - " + error);
                            }

                            callback?.Invoke(error);
                        }));
                    }
                    else
                    {
                        Debug.Log("<color=green>SERVICE REQUEST BUNDLE: Updated version of bundle already in cache.</color>");

                        callback?.Invoke(string.Empty);
                    }
            }));
        }

        private static string GetDownloadableBundlePath(AssetCategory category, string bundleName)
        {
            return "https://raw.githubusercontent.com/kronousTech/Portfolio-WEBGL-PC/main/Content/Bundles/" + category.ToString() + "/" + bundleName;
        }
        private static string GetDownloadableBundleManifestPath(AssetCategory category, string bundleName)
        {
            return GetDownloadableBundlePath(category, bundleName) + ".manifest";
        }
        private static string GetCategoryFolderPath(AssetCategory category)
        {
            return Application.persistentDataPath + "/bundles/" + category.ToString();
        }
        private static string GetBundlePath(string bundleName, AssetCategory category)
        {
            return GetCategoryFolderPath(category) + "/" + bundleName;
        }
        private static void CreateVersionFile(string path, string version)
        {
            var sr = File.CreateText(path);
            sr.WriteLine(version);
            sr.Close();
        }
        private static string GetBundleVersion(string fileText)
        {
            return Regex.Replace(fileText, @"[\r\n\t\s]+", "");
        }
    }
}