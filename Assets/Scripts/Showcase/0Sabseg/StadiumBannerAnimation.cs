using UnityEngine;

namespace PortfolioProject.Sabseg
{
    public class StadiumBannerAnimation : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private const string TEXTURE_NAME = "_BaseMap";

        private void Update()
        {
            var material = GetComponent<MeshRenderer>().material;
            var offset = material.GetTextureOffset(TEXTURE_NAME);
            offset.x += Time.deltaTime * _speed;

            material.SetTextureOffset(TEXTURE_NAME, new Vector2(offset.x, offset.y));
        }
    }
}