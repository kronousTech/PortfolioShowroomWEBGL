using UnityEngine;

namespace KronosTech.ShowroomGeneration.Room
{
    public class RoomInfoTryButton : MonoBehaviour
    {
        [SerializeField] private string _projectURL;
        [SerializeField] private OpenURL[] _buttons;

        private void Start()
        {
            foreach (var item in _buttons)
            {
                if (string.IsNullOrEmpty(_projectURL))
                {
                    item.gameObject.SetActive(false);
                }
                else
                {
                    item.gameObject.SetActive(true);
                    item.SetNewURL(_projectURL);
                }
            }
        }
    }
}