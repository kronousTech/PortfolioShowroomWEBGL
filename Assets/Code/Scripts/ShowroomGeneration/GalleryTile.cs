using System;
using UnityEngine;

namespace KronosTech.ShowroomGeneration
{
    public class GalleryTile : MonoBehaviour
    {
        [SerializeField] private Transform _exitsParent;

        private GalleryTileExit[] _exits;

        public int GetExitsCount { get { return _exits.Length; } private set { } }

        public void Initialize(int remainingRooms, GalleryTileExit exit, Action<GalleryTileExit, GalleryTileExit[]> exits)
        {
            _exits = new GalleryTileExit[_exitsParent.childCount];

            for (int i = 0; i < _exitsParent.childCount; i++)
            {
                _exits[i] = _exitsParent.GetChild(i).GetComponent<GalleryTileExit>();
            }
            
            transform.position = exit.Position;
            transform.rotation = exit.Rotation;

            if(remainingRooms > _exits.Length)
            {

                for (int i = 1; i < _exits.Length; i++)
                {
                    var currentExit = _exits[i];
                    var currentDistance = Vector3.Distance(currentExit.Position, Vector3.zero);
                    var x = i - 1;

                    while (x >= 0 && Vector3.Distance(_exits[x].Position, Vector3.zero) < currentDistance)
                    {
                        _exits[x + 1] = _exits[x];
                        x--;
                    }

                    _exits[x + 1] = currentExit;
                }

                var furthestIndex = _exits.Length > 2 ? UnityEngine.Random.Range(0, 2) : 0;
                var roomPositions = new GalleryTileExit[_exits.Length - 1];
                var j = 0;

                for (int i = 0; i < _exits.Length ; i++)
                {
                    if (i != furthestIndex)
                    {
                        roomPositions[j++] = _exits[i];
                    }
                }

                exits?.Invoke(_exits[furthestIndex], roomPositions);
            }
            else
            {
                exits?.Invoke(null, _exits);
            }
        }
    }
}