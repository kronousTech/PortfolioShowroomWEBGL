using UnityEngine;

public class RoomTags : MonoBehaviour
{
    [SerializeField] private RoomTagFlags roomTags;

    public RoomTagFlags Tags { get { return roomTags; } }
}