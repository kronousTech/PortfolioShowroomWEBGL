using KronosTech.AssetManagement;

namespace KronosTech.Services
{
    public class ServiceWebVideos : IService
    {
        public ServiceWebVideos() { }

        private static readonly string _baseURL = "https://media.githubusercontent.com/media/kronousTech/Portfolio-WEBGL-PC/main/Content/Videos/";

        public string LoadVideo(Asset asset)
        {
            return _baseURL + asset.category + "/" + asset.bundle + "/" + asset.name + ".mp4";
        }
    }
}