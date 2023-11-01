using UnityEngine;

public class GalleryCorridor : MonoBehaviour
{
    [SerializeField] private GalleryTileExit _exit;

    public GalleryTileExit GetExit => _exit;

    public void Place(GalleryTileExit exit)
    {
        transform.position = exit.Position;
        transform.rotation = exit.Rotation;
    }
}