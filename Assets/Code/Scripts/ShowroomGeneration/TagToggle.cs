using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TagToggle : MonoBehaviour
{
    private Toggle _toggle;
    private RoomTagFlags _selectedTag;

    [SerializeField] private TextMeshProUGUI _tagText;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }

    public void Initialize(string tag)
    {
        _tagText.text = tag;

        _selectedTag = System.Enum.Parse<RoomTagFlags>(tag);
    }

    public bool GetTag(out RoomTagFlags tag)
    {
        tag = _selectedTag;

        return _toggle.isOn;
    }

    public void SetInteractability(bool interactable)
    {
        _toggle.interactable = interactable;
    }
}