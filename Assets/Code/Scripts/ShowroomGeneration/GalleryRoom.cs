using UnityEngine;

public class GalleryRoom : MonoBehaviour
{
    [SerializeField] private GameObject _line;

    public void Initialize()
    {
        gameObject.SetActive(false);
    }
    public void Place(GalleryTileExit exit)
    {
        gameObject.SetActive(true);

        transform.position = exit.Position;
        transform.rotation = exit.Rotation;

        if(_line != null)
        {
            _line.SetActive(exit.AddLines);
        }
    }
}