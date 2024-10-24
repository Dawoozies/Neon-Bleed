using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
[ExecuteAlways]
public class Path : MonoBehaviour
{
    public Tuple<Transform, Transform>[] edges;
    int previousChildCount = -1;
    //edges = vertices - 1 from euler characteristic of plane graph
    [ContextMenu("SetUpEdges")]
    public void SetUpEdges()
    {
        if (transform.childCount < 2)
            return;
        //edges = vertices - 1
        edges = new Tuple<Transform, Transform>[transform.childCount - 1];
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Tuple<Transform, Transform> e = new Tuple<Transform, Transform>(transform.GetChild(i), transform.GetChild(i + 1));
            edges[i] = e;
        }
        previousChildCount = transform.childCount;
    }
    public Tuple<Transform, Transform>[] GetEdges()
    {
        return edges;
    }
    private void OnDrawGizmosSelected()
    {
        if (transform.childCount < 2)
            return;
        if (edges == null || edges.Length == 0)
            return;

#if UNITY_EDITOR
        if(previousChildCount != transform.childCount)
        {
            SetUpEdges();
        }

        Handles.color = Color.green;
        for (int i = 0; i < transform.childCount; i++)
        {
            Handles.Label(transform.GetChild(i).position, $"VERTEX {i}");
        }
#endif
        Gizmos.color = Color.red;
        for (int i = 0; i < edges.Length; i++)
        {
            Gizmos.DrawLine(edges[i].Item1.position, edges[i].Item2.position);
        }
    }
}