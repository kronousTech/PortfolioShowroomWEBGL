using UnityEditor;
using UnityEngine;

namespace GizmosVisualizers
{
    [ExecuteInEditMode]
    public class GizmoTeleportLocation : MonoBehaviour
    {
        private TeleportLocation _teleport;
        private Mesh _capsuleMesh;

        private void Awake()
        {
            _teleport = GetComponent<TeleportLocation>();
            _capsuleMesh = Resources.Load<GameObject>("BasicPrimitives/Capsule").GetComponent<MeshFilter>().sharedMesh;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawMesh(_capsuleMesh, transform.position, Quaternion.identity, Vector3.one);
#if UNITY_EDITOR
            Handles.color = Color.cyan;
            Handles.Label(transform.position + (Vector3.up * 1.5f), _teleport.Name);
#endif
        }
    }
}