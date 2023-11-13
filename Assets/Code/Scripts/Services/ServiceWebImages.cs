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

    private static Sprite TextureToSprite(Texture texture)
    {
        RenderTexture renderTexture = RenderTexture.GetTemporary(texture.width, texture.height);
        Graphics.Blit(texture, renderTexture);

        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTexture;

        Texture2D texture2D = new Texture2D(texture.width, texture.height);
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTexture);

        return Sprite.Create(texture2D, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f)); ;
    }
}