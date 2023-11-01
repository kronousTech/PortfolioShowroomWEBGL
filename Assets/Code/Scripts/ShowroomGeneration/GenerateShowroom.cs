using System.Collections.Generic;
using UnityEngine;

namespace KronosTech.ShowroomGeneration
{
    public class GenerateShowroom : MonoBehaviour
    {
        [SerializeField] private GalleryTileExit _galleryStart;

        [Header("Parents")]
        [SerializeField] private Transform _tilesParent;
        [SerializeField] private Transform _corridorsParent;
        [SerializeField] private Transform _roomsParent;

        private void OnEnable()
        {
            ShowroomGenerationEvents.OnRoomsSelection += GenerateRooms;
        }
        private void OnDisable()
        {
            ShowroomGenerationEvents.OnRoomsSelection -= GenerateRooms;
        }

        private void GenerateRooms(List<GalleryRoom> rooms) 
        {
            _tilesParent.ClearChildren();
            _corridorsParent.ClearChildren();
            _roomsParent.DisableChildren();
            
            var remainingRooms = rooms.Count;
            GalleryTileExit nextExit = null;
            var roomIndex = 0;
            
            while (remainingRooms > 0)
            {
                var corridor = Instantiate(GalleryGenerationPieces.GetCorridor(), _corridorsParent);
                corridor.Place(nextExit != null ? nextExit : _galleryStart);

                nextExit = corridor.GetExit;

                // Instantiate Tile
                var currentTile = Instantiate(GalleryGenerationPieces.GetTile(remainingRooms), _tilesParent);
                currentTile.Initialize(remainingRooms, nextExit, (GalleryTileExit exit, GalleryTileExit[] roomPositions) =>
                {
                    if(exit != null)
                    {
                        nextExit = exit;
                    }

                    // Add Display Rooms
                    for (int i = 0; i < roomPositions.Length; i++)
                    {
                        if(roomIndex < rooms.Count)
                        {
                            rooms[roomIndex++].Place(roomPositions[i]);

                            remainingRooms--;
                        }
                        else
                        {
                            Instantiate(GalleryGenerationPieces.GetWall, _corridorsParent)
                            .Place(roomPositions[i]);
                        } 
                    }
                });
            }
        }
    }
}