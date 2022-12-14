using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Debug.GizmosVisualizers
{
    public class GizmoPlayerDirection : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.forward * 2f);
        }
    }
}