using KronosTech.ShowroomGeneration;
using UnityEngine;

public static class GalleryGenerationPieces
{
    [SerializeField] private static GalleryTile[] _tilesPrefabs;
    [SerializeField] private static GalleryCorridor[] _corridorPrefabs;
    [SerializeField] private static GalleryRoom _endWall;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize() 
    {
        _tilesPrefabs = Resources.LoadAll<GalleryTile>("GalleryGeneration/Tiles");
        _corridorPrefabs = Resources.LoadAll<GalleryCorridor>("GalleryGeneration/Corridors");
        _endWall = Resources.Load<GalleryRoom>("GalleryGeneration/Wall/Wall");
    }

    public static GalleryTile GetTile(int remainingRoomsCount)
    {
        return _tilesPrefabs[Random.Range(0, _tilesPrefabs.Length)];    
    } 
    public static GalleryCorridor GetCorridor()
    {
        return _corridorPrefabs[Random.Range(0, _corridorPrefabs.Length)];
    }
    public static GalleryRoom GetWall => _endWall;
}
