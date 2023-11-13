using UnityEngine;

public class RoomInfoTags : MonoBehaviour
{
    [SerializeField] private RoomTagFlags roomTags;

    public RoomTagFlags Tags { get { return roomTags; } }
}