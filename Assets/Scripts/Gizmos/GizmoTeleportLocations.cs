using System.Collections.Generic;
using Teleports;
using UnityEditor;
using UnityEngine;

namespace GizmosVisualizers
{
    [ExecuteInEditMode]
    public class GizmoTeleportLocations : MonoBehaviour
    {
        private IEnumerable<TeleportInfo> _info;
        private Mesh _capsuleMesh;

        private void Awake()
        {
            _info = GetComponent<TeleportsManager>().GetLocationsInfo();
            _capsuleMesh = Resources.Load<GameObject>("BasicPrimitives/Capsule").GetComponent<MeshFilter>().sharedMesh;
        }

        private void Start()
        {
            foreach (var item in _info)
            {
                if(item.location != null)
                    item.location.transform.name = "Location: " + item.name;
            }
        }

        private void OnDrawGizmos()
        {
            if(_info == null)
            {
                _info = GetComponent<TeleportsManager>().GetLocationsInfo();
            }

            foreach (var item in _info)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawMesh(_capsuleMesh, item.location.position, Quaternion.identity, Vector3.one);
                
                Handles.color = Color.cyan;
                Handles.Label(item.location.position + (Vector3.up * 1.5f), item.name);
            }
        }
    }
}