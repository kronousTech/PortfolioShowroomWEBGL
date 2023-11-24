using KronosTech.AssetManagement;
using KronosTech.Services;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class AssetsLoader
{
    private class CoroutineRunner : MonoBehaviour { }

    private static MonoBehaviour _temporaryCoroutineRunner;
    private static string _manifestURL = "https://raw.githubusercontent.com/kronousTech/Portfolio-WEBGL-PC/main/Content/Bundles/AssetBundles.manifest";

    public static event Action<int, int> OnProgress;
    public static event Action OnBundlesDownload;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Initialize()
    {
        var temporaryObject = new GameObject("TemporaryCoroutineRunner");

        _temporaryCoroutineRunner = temporaryObject.AddComponent<CoroutineRunner>();
        _temporaryCoroutineRunner.StartCoroutine(CallWebRequest.GetRequest(_manifestURL, (string data, string error) =>
        {
            if (!string.IsNullOrEmpty(error))
            {
                Debug.LogError("Error getting bundles data: " +  error);
                return;
            }
            
            var matches = Regex.Matches(data, @"Info_\d+:\s+Name:\s+(?<Name>.+)");
            var values = new Dictionary<AssetCategory, List<string>>();

            foreach (Match match in matches)
            {
                var parts = match.Value.Split('/');
                var bundleName = parts[1];
                var category = Enum.Parse<AssetCategory>(parts[0].Split(':')[2].Trim());

                if (values.ContainsKey(category))
                {
                    values[category].Add(bundleName);
                }
                else
                {
                    values.Add(category, new List<string> { bundleName });
                }
            }

            var downloadCount = 0;

            foreach (var item in values)
            {
                foreach (var bundle in item.Value)
                {
                    CallAssetBundle.DownloadBundle(bundle, item.Key, _temporaryCoroutineRunner, (string error) =>
                    {
                        if (!string.IsNullOrEmpty(error))
                        {
                            Debug.LogError("Error downloading bundle: " + bundle + " - " + error);
                        }
                        downloadCount++;

                        OnProgress?.Invoke(downloadCount, matches.Count);

                        if (downloadCount == matches.Count)
                        {
                            Debug.Log("All Downloads Complete");

                            GameObject.Destroy(_temporaryCoroutineRunner);

                            OnBundlesDownload?.Invoke();
                        }
                    });
                }
            }
        }));
    }
}