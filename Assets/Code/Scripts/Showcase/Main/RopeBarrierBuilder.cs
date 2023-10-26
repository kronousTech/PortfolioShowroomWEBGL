using System.Collections.Generic;
using UnityEngine;

public class RopeBarrierBuilder : MonoBehaviour
{
    [SerializeField] private LineRenderer ropeExample;
    [SerializeField] private List<GameObject> _bars = new List<GameObject>();
    [SerializeField] private List<GameObject> _ropes = new List<GameObject>();
    [SerializeField] private int _bezierVerticesCount;
    [SerializeField] private float _downAmount;

    private void Awake()
    {
        GetBars();
    }

    private void Start()
    {
        SetupRopes();
    }

    [ContextMenu("Get Bars")]
    private void GetBars()
    {
        _bars.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).name.Contains("Rope Bar"))
            {
                _bars.Add(transform.GetChild(i).gameObject);
            }
        }
    }

    [ContextMenu("Build Rope")]
    private void SetupRopes()
    {
        ClearRopes();

        // Add line renderers
        for (int i = 0; i < _bars.Count - 1; i++)
        {
            var newLine = new GameObject("Line " + i);
            var currentHandle = GetHandlePosition(_bars[i]);
            var nextHandle = GetHandlePosition(_bars[i + 1]);
            newLine.transform.SetParent(transform);
            newLine.transform.position = Vector3.Lerp(currentHandle, nextHandle, 0.5f);
            newLine.transform.LookAt(nextHandle);
            var lineRenderer = newLine.AddComponent<LineRenderer>();
            lineRenderer.useWorldSpace = true;
            lineRenderer.positionCount = 2 + _bezierVerticesCount;
            lineRenderer.SetPositions(GetRopePositions(currentHandle, nextHandle));
            lineRenderer.widthCurve = ropeExample.widthCurve;
            lineRenderer.startWidth = ropeExample.startWidth;
            lineRenderer.endWidth = ropeExample.endWidth;
            lineRenderer.material = ropeExample.sharedMaterial;
            lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.TwoSided;

            _ropes.Add(newLine);
        }

        // Add colliders
        foreach (var rope in _ropes) 
        {
            var lineRenderer = rope.GetComponent<LineRenderer>();
            var firstPos = lineRenderer.GetPosition(0);
            var lastPos = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
            var length = Vector3.Distance(firstPos, lastPos);

            var collider = rope.AddComponent<BoxCollider>();
            collider.size = new Vector3(0.1f, 1, length);
            collider.center = new Vector3(collider.center.x, -0.5f, collider.center.z);
        }
    }

    [ContextMenu("Clear Ropes")] 
    private void ClearRopes()
    {
        for (int i = 0; i < _ropes.Count; i++)
        {
            DestroyImmediate(_ropes[i]);
        }
        _ropes = new List<GameObject>();
    }

    private Vector3 GetHandlePosition(GameObject bar)
    {
        return bar.transform.GetChild(2).transform.position;
    }

    private Vector3[] GetRopePositions(Vector3 bar1, Vector3 bar2)
    {
        var p0 = bar1;
        var p1 = ((bar1 + bar2) / 2) + Vector3.down * _downAmount;
        var p2 = bar2;
        var ropePoints = new Vector3[_bezierVerticesCount + 2];
        ropePoints[0] = bar1;
        ropePoints[_bezierVerticesCount + 1] = bar2;

        for (int i = 0; i < _bezierVerticesCount; i++)
        {
            var t = i / (_bezierVerticesCount - 1.0f);
            var position = (1.0f - t) * (1.0f - t) * p0 + 2.0f * (1.0f - t) * t * p1 + t * t * p2;

            ropePoints[i + 1] = position; 
        }

        return ropePoints;
    }
}