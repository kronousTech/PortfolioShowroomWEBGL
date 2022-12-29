using System;
using UnityEngine;
using UnityEngine.Events;

namespace Teleports
{
    [Serializable]
    public struct TeleportInfo
    {
        public string name;
        public Transform location;
        public UnityEvent events;
    }
}
