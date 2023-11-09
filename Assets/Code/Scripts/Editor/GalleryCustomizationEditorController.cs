using KronosTech.Customization;
using UnityEditor;
using UnityEngine;

public class GalleryCustomizationEditorController : EditorWindow
{
    private int _floorIndex = 0;
    private int _baseboardIndex = 0;
    private int _wallIndex = 0;
    private int _guidelineIndex = 0;

    [MenuItem("KronosTech/Gallery Customization")]
    public static void OpenWindow()
    {
        GetWindow<GalleryCustomizationEditorController>("Gallery Customization");
    }

    private void OnGUI()
    {
        GUILayout.Label("Floor", EditorStyles.boldLabel);
        _floorIndex = EditorGUILayout.IntField("Floor material index", _floorIndex);
        _floorIndex = Mathf.Max(_floorIndex, 0);
        if (Application.isPlaying)
        {
            if (GUILayout.Button("REPLACE"))
                GalleryCustomization.SetNewCurrentMat(CustomizableElement.Floor, _floorIndex);
        }
        else GUILayout.Button("REPLACE");


        GUILayout.Label("Baseboard", EditorStyles.boldLabel);
        _baseboardIndex = EditorGUILayout.IntField("Baseboard material index", _baseboardIndex);
        _baseboardIndex = Mathf.Max(_baseboardIndex, 0);
        if (Application.isPlaying)
        {
            if (GUILayout.Button("REPLACE"))
                GalleryCustomization.SetNewCurrentMat(CustomizableElement.Baseboard, _baseboardIndex);
        }
        else GUILayout.Button("REPLACE");


        GUILayout.Label("Wall", EditorStyles.boldLabel);
        _wallIndex = EditorGUILayout.IntField("Wall material index", _wallIndex);
        _wallIndex = Mathf.Max(_wallIndex, 0);
        if (Application.isPlaying)
        {
            if (GUILayout.Button("REPLACE"))
                GalleryCustomization.SetNewCurrentMat(CustomizableElement.Wall, _wallIndex);
        }
        else GUILayout.Button("REPLACE");


        GUILayout.Label("Guideline", EditorStyles.boldLabel);
        _guidelineIndex = EditorGUILayout.IntField("Guideline material index", _guidelineIndex);
        _guidelineIndex = Mathf.Max(_guidelineIndex, 0);
        if (Application.isPlaying)
        {
            if (GUILayout.Button("REPLACE"))
                GalleryCustomization.SetNewCurrentMat(CustomizableElement.Guideline, _guidelineIndex);
        }
        else GUILayout.Button("REPLACE");
    }
}
