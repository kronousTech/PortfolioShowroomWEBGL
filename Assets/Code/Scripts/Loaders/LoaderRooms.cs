using KronosTech.ShowroomGeneration;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KronosTech.Loaders
{
    public static class LoaderRooms
    {
        private static readonly List<GalleryRoom> _initializedRooms = new();

        public static event Action<List<GalleryRoom>> OnRoomsInstantiated;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            var rooms = Resources.LoadAll<GalleryRoom>("GalleryGeneration/Rooms/Done");

            SceneManager.LoadScene(1, LoadSceneMode.Additive);

            for (int i = 0; i < rooms.Length; i++)
            {
                var instantiatedRoom = UnityEngine.Object.Instantiate(rooms[i]);
                instantiatedRoom.ToggleVisibility(false);

                SceneManager.MoveGameObjectToScene(instantiatedRoom.gameObject, SceneManager.GetSceneByBuildIndex(1));

                _initializedRooms.Add(instantiatedRoom);
            }
        }
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void LateInitialize()
        {
            OnRoomsInstantiated?.Invoke(_initializedRooms);
        }
    }
}