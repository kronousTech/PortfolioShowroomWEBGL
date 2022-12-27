using UnityEngine;
using UnityEngine.UI;

namespace Ui.Options
{
    public class HeadBobbingToggle : MonoBehaviour
    {
        private PlayerHeadBobbing _headBobbing;

        private void Awake()
        {
            _headBobbing = GameObject.FindObjectOfType<PlayerHeadBobbing>();
            if (_headBobbing == null)
            {
                Debug.LogError("Didn't found PlayerHeadBobbing on HeadBobbingToggle");
                return;
            }

            GetComponent<Toggle>().onValueChanged.AddListener(_headBobbing.SetState);
            GetComponent<Toggle>().isOn = _headBobbing.GetState();
        }
    }
}