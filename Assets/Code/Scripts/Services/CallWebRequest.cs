using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;

namespace KronosTech.Services
{
    public static class CallWebRequest
    {
        public static IEnumerator GetRequest(string url, Action<Texture2D, string> onComplete)
        {
            using var www = UnityWebRequest.Get(url);
            www.downloadHandler = new DownloadHandlerTexture();

            yield return www.SendWebRequest();

            while (!www.isDone)
            {
                yield return null;
            }

            if(www.result == UnityWebRequest.Result.Success)
            {
                try
                {
                    var query_error = www.error;
                    var texture = ((DownloadHandlerTexture)www.downloadHandler).texture;

                    onComplete?.Invoke(texture, string.Empty);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error downloading thumbnail: " + url + " - " + e.Message);
                    onComplete?.Invoke(null, e.Message);
                }
            }
            else
            {
                Debug.LogError("Error on texture api request: " + url + " - " +  www.result + " - " + www.error);

                onComplete?.Invoke(null, www.error);
            }
        }
        public static IEnumerator GetRequest(string url, Action<byte[], string> onComplete)
        {
            using (var www = UnityWebRequest.Get(url))
            {
                www.downloadHandler = new DownloadHandlerBuffer();

                yield return www.SendWebRequest();

                while (!www.isDone)
                {
                    yield return null;
                }

                if (www.result == UnityWebRequest.Result.Success)
                {
                    try
                    {
                        var query_error = www.error;
                        var data = www.downloadHandler.data;

                        onComplete?.Invoke(data, string.Empty);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("Error downloading video: " + url + " - " + e.Message);
                        onComplete?.Invoke(null, e.Message);
                    }
                }
                else
                {
                    Debug.LogError("Error on video API request: " + url + " - " + www.result + " - " + www.error);
                    onComplete?.Invoke(null, www.error);
                }
            }
        }
        public static IEnumerator GetRequest(string url, Action<string, string> onComplete)
        {
            using (var www = UnityWebRequest.Get(url))
            {
                yield return www.SendWebRequest();

                while (!www.isDone)
                {
                    yield return null;
                }

                if (www.result == UnityWebRequest.Result.Success)
                {
                    try
                    {
                        var query_error = www.error;
                        var data = www.downloadHandler.text;

                        onComplete?.Invoke(data, string.Empty);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("Error getting text data: " + url + " - " + e.Message);
                        onComplete?.Invoke(null, e.Message);
                    }
                }
                else
                {
                    Debug.LogError("Error getting text data: " + url + " - " + www.result + " - " + www.error);
                    onComplete?.Invoke(null, www.error);
                }
            }
        }

        public static IEnumerator GetRequestAssetBundle(string url, Action<byte[], string> onComplete)
        {
            using var www = UnityWebRequest.Get(url);
            www.downloadHandler = new DownloadHandlerBuffer();

            yield return www.SendWebRequest();

            while (!www.isDone)
            {
                yield return null;
            }

            onComplete?.Invoke(www.downloadHandler.data, www.error);
        }
    }
}