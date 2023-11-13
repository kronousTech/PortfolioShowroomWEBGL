using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace KronosTech.ShowroomGeneration.Room
{
    public enum InfoCategories
    {
        Role = 0,
        Client = 1,
        Overview = 2,
        Challenges = 3,
        Solutions = 4,
        Lessons = 5
    }
    [Serializable]
    public struct InfoCategory
    {
        public InfoCategories category;
        [TextArea(10, 50)] public string info;
    }

    [ExecuteInEditMode]
    public class RoomInfoText : MonoBehaviour
    {
        [SerializeField] private List<InfoCategory> _infoData;

        [SerializeField] private TextMeshPro[] _texts;

        private void Start()
        {
            var text = string.Empty;

            foreach (var item in _infoData)
            {
                text += "<b><size=90>" + item.category.ToString() + "</b></size>\n"; 
                text += item.info + "\n\n" ;
            }

            foreach (var item in _texts)
            {
                item.text = text;
            }
        }
    }
}