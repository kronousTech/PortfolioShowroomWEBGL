using KronosTech.AssetManagement;

namespace KronosTech.Services
{
    public class ServiceWebVideos : IService
    {
        public ServiceWebVideos() { }

        private static string _baseURL = "https://github.com/kronousTech/Portfolio-WEBGL-PC/raw/main/Content/Videos/";

        public string LoadVideo(Asset asset)
        {
            return _baseURL + asset.category + "/" + asset.bundle + "/" + asset.name + ".mp4";
        }
    }
}