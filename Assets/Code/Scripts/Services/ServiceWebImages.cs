using KronosTech.Services;
using System;
using UnityEngine;

public class ServiceWebImages : IService
{
    private readonly ServiceRequestAPI _serviceRequestAPI;

    public ServiceWebImages(ServiceRequestAPI serviceRequestAPI)
    {
        _serviceRequestAPI = serviceRequestAPI;
    }

    public void GetImageSprite(string url, MonoBehaviour mono, Action<Sprite, string> callback)
    {
        mono.StartCoroutine(_serviceRequestAPI.GetRequest(url, (Texture2D texture, string error) =>
        {
            if(string.IsNullOrEmpty(error))
            {
                callback?.Invoke(Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero), error);
            }
            else
            {
                Debug.LogError("Error getting image sprite. - " + url);

                callback?.Invoke(null, error);
            }
        }));
    }
}