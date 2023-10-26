using UnityEngine;

[ExecuteInEditMode]
public class GizmoPlayerCollision : MonoBehaviour
{
    private Mesh _capsuleMesh;
    private void Awake()
    {
        _capsuleMesh = Resources.Load<GameObject>("BasicPrimitives/Capsule").GetComponent<MeshFilter>().sharedMesh;
    }
    private void OnDrawGizmos()
    {
        var height = GetComponent<CapsuleCollider>().height;
        Gizmos.color = Color.red;
        Gizmos.DrawMesh(_capsuleMesh,transform.position, Quaternion.identity, new Vector3(1, height / 2f, 1));
    }
}
