using System;
using System.Collections;
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

        public static event Action OnGenerationStart;
        public static event Action<bool> OnGenerationEnd;

        private bool _startedGeneration;

        private void OnEnable()
        {
            RoomsSelector.OnSelection += (rooms) => StartCoroutine(GenerateRooms(rooms));
        }
        private void OnDisable()
        {
            RoomsSelector.OnSelection -= (rooms) => StartCoroutine(GenerateRooms(rooms));
        }

        private IEnumerator GenerateRooms(List<GalleryRoom> rooms) 
        {
            if (_startedGeneration)
                yield break;

            OnGenerationStart?.Invoke();
            
            _tilesParent.ClearChildren();
            _corridorsParent.ClearChildren();
            
            var remainingRooms = rooms.Count;
            GalleryTileExit nextExit = null;
            var roomIndex = 0;

            _startedGeneration = true;

            yield return null;

            while (remainingRooms > 0)
            {
                var corridor = Instantiate(GalleryGenerationPieces.GetCorridor(), _corridorsParent);
                corridor.Place(nextExit != null ? nextExit : _galleryStart);

                nextExit = corridor.GetExit;

                yield return null;

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
                            rooms[roomIndex].ToggleVisibility(true);
                            rooms[roomIndex++].Place(roomPositions[i]);

                            remainingRooms--;
                        }
                        else
                        {
                            Instantiate(GalleryGenerationPieces.GetWall(), _corridorsParent)
                            .Place(roomPositions[i]);
                        }
                    }
                });

                yield return null;
            }

            _startedGeneration = false;

            OnGenerationEnd?.Invoke(rooms.Count > 0);
        }
    }
}