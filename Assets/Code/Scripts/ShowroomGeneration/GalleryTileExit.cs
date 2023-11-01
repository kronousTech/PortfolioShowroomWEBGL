using UnityEngine;

public class GalleryTileExit : MonoBehaviour
{
    [SerializeField] private bool _addLines = true;

    public Vector3 Position => transform.position;
    public Quaternion Rotation => transform.rotation;
    public bool AddLines => _addLines;
}
