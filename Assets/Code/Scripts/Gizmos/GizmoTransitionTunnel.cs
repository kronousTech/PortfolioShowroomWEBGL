using UnityEngine;

namespace GizmosVisualizers
{
    [ExecuteInEditMode]
    public class GizmoTransitionTunnel : MonoBehaviour
    {
        private Mesh _mesh;
        private void Awake()
        {
            _mesh = Resources.Load<GameObject>("BasicPrimitives/Cube").GetComponent<MeshFilter>().sharedMesh;
        }
        private void OnDrawGizmos()
        {
            var planesScale = new Vector3(transform.localScale.x, transform.localScale.y, 0.1f);
            var basePosition = transform.position;
            var offset = new Vector3(0, 0, transform.localScale.z / 2f);
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawMesh(_mesh, basePosition + offset, Quaternion.identity, planesScale);
            Gizmos.color = new Color(0, 0, 1, 0.5f);
            Gizmos.DrawMesh(_mesh, basePosition - offset, Quaternion.identity, planesScale);
            //Gizmos.color = Color.blue;
            //Gizmos.DrawMesh(sideMesh, transform.position - transform.localScale, Quaternion.identity, transform.localScale);
        }
    }
}

