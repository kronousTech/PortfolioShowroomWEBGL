using KronosTech.ShowroomGeneration;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GalleryGenerationPieces
{
    private static GalleryTile _lastTile;
    [SerializeField] private static List<GalleryTile> _tilesPrefabs;
    [SerializeField] private static GalleryCorridor[] _corridorPrefabs;
    [SerializeField] private static GalleryRoom _endWall;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize() 
    {
        _tilesPrefabs = Resources.LoadAll<GalleryTile>("GalleryGeneration/Tiles").ToList();
        _corridorPrefabs = Resources.LoadAll<GalleryCorridor>("GalleryGeneration/Corridors");
        _endWall = Resources.Load<GalleryRoom>("GalleryGeneration/Wall/Wall");
    }

    public static GalleryTile GetTile(int remainingRoomsCount)
    {
        if(_lastTile == null)
        {
            _lastTile = _tilesPrefabs[Random.Range(0, _tilesPrefabs.Count)];

            return _lastTile;
        }
        else
        {
            var indexOfLastTile = _tilesPrefabs.IndexOf(_lastTile);
            var tempTile = _tilesPrefabs[^1];

            _tilesPrefabs[indexOfLastTile] = tempTile;
            _tilesPrefabs[^1] = _lastTile;

            _lastTile = _tilesPrefabs[Random.Range(0, _tilesPrefabs.Count - 1)];

            return _lastTile;
        }
    } 
    public static GalleryCorridor GetCorridor()
    {
        return _corridorPrefabs[Random.Range(0, _corridorPrefabs.Length)];
    }
    public static GalleryRoom GetWall => _endWall;
}
