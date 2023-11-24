using UnityEditor;
using UnityEngine;

public class BundlesFolderOpen : Editor
{
    [MenuItem("KronosTech/Open Bundles Folder", priority = 0)]
    public static void OpenWindow()
    {
        UnityEditor.EditorUtility.OpenWithDefaultApp(Application.persistentDataPath);
    }
}
