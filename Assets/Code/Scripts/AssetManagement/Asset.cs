using System;

namespace KronosTech.AssetManagement
{
    [Serializable]
    public class Asset
    {
        public string name;
        public string bundle;
        public AssetCategory category;
    }

    public enum AssetCategory
    {
        gallery = 0,
        portfolio = 1
    }
}