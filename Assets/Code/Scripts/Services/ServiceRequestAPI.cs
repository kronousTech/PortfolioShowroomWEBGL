using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using System.Net;
using UnityEngine.Video;

namespace KronosTech.Services
{
    public class ServiceRequestAPI : IService
    {
        public IEnumerator GetRequest(string url, Action<Texture2D, string> onComplete)
        {
            using var www = UnityWebRequest.Get(url);
//#if UNITY_WEBGL
//            www.SetRequestHeader("Access-Control-Allow-Credentials", "true");
//            www.SetRequestHeader("Access-Control-Allow-Headers", "x-requested-with, Content-Type, origin, authorization, Accepts, accept, client-security-token, access-control-allow-headers");
//            www.SetRequestHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
//            www.SetRequestHeader("Access-Control-Allow-Origin", "*");
//#endif

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
        public IEnumerator GetRequest(string url, Action<byte[], string> onComplete)
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
    }
}