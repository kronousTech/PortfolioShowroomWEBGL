using KronosTech.AssetManagement;
using KronosTech.Services;
using UnityEngine;

public class ServiceWebImages : IService
{
    public ServiceWebImages() {}

    public Sprite LoadImage(Asset asset)
    {
        var texture = CallAssetBundle.LoadAsset<Texture2D>(asset);
        
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }
}