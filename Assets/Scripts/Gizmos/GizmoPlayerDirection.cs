using UnityEngine;

namespace GizmosVisualizers
{
    public class GizmoPlayerDirection : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2f);
        }
    }
}