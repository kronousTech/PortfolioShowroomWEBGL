using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Customization
{
    public class MaterialButtonDisplay : MonoBehaviour
    {
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void Initialize(Material material)
        {
            if(material.mainTexture != null)
            {
                var texture2D = TextureToTexture2D(material.mainTexture);

                _image.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.zero);
            }
            // Skybox
            else if (material.HasProperty("_Tex"))
            {
                var texture2D = ConvertCubemapToTexture2D((Cubemap)material.GetTexture("_Tex"));

                _image.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.zero);
            }
            
            if(material.HasProperty("_Color"))
                _image.color = material.color;
        }

        private static Texture2D TextureToTexture2D(Texture texture)
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

            return texture2D;
        }

        Texture2D ConvertCubemapToTexture2D(Cubemap cubemap)
        {
            var width = cubemap.width;
            var height = cubemap.height;

            // Create a new Texture2D
            var texture = new Texture2D(width, height, TextureFormat.RGB24, false);

            // Set pixels from the specified face of the cubemap
            var colors = cubemap.GetPixels(CubemapFace.PositiveY);
            texture.SetPixels(colors);

            // Apply changes
            texture.Apply();

            return texture;
        }
    }
}
