using System.IO;
using UnityEditor;
using UnityEngine;

public class BundlesFolderDelete : Editor
{
    [MenuItem("KronosTech/Delete Bundles Folder", priority = 1)]
    public static void DeleteFolder()
    {
        string bundlesFolderPath = Application.persistentDataPath + "/bundles";

        if (Directory.Exists(bundlesFolderPath))
        {
            Directory.Delete(bundlesFolderPath, true);
        }
    }
}
