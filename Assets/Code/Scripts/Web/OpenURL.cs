using UnityEngine;
using UnityEngine.UI;

public class OpenURL : MonoBehaviour
{
    [SerializeField] private string _url;

    private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(OpenWebLink);
    }
    private void OnDisable()
    {
        _button.onClick.RemoveListener(OpenWebLink);
    }
    private void Awake()
    {
        _button = GetComponent<Button>();   
    }

    private void OpenWebLink()
    {
        if (!Application.isFocused)
            return;

        Application.OpenURL(_url);
    }

    public void SetNewURL(string url) => _url = url;
}