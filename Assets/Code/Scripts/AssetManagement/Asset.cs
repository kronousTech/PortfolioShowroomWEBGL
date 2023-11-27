using System;

namespace KronosTech.AssetManagement
{
    [Serializable]
    public class Asset
    {
        public string name;
        public string bundle;
        public AssetCategory category;

        public Asset(string name, string bundle, AssetCategory category)
        {
            this.name = name;
            this.bundle = bundle;
            this.category = category;
        }
    }

    public enum AssetCategory
    {
        gallery = 0,
        portfolio = 1
    }
}