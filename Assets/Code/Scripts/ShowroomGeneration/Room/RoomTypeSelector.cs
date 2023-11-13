using TMPro;
using UnityEngine;

namespace KronosTech.ShowroomGeneration
{
    [ExecuteInEditMode]
    public class RoomTypeSelector : MonoBehaviour
    {
        public enum RoomType
        {
            Room = 0,
            Wall = 1
        }

        [SerializeField] private Transform _typesParent;
        [SerializeField] private RoomType _type;

        private void Awake()
        {
            for (int i = 0; i < _typesParent.childCount; i++)
            {
                _typesParent.GetChild(i).gameObject.SetActive(i == (int)_type);
            }
        }
    }
}