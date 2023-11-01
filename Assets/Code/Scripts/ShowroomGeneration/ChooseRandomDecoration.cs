using UnityEngine;

namespace KronosTech.ShowroomGeneration 
{
    public class ChooseRandomDecoration : MonoBehaviour
    {
        private void OnEnable()
        {
            var choosenDecorationIndex = Random.Range(0, transform.childCount);

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(i == choosenDecorationIndex);
            }
        }
    }
}