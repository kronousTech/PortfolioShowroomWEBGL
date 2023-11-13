using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;

namespace KronosTech.Services
{
    public class ServiceRequestAPI : IService
    {
        public IEnumerator GetRequest(string url, Action<Texture2D, string> onComplete)
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
    }
}