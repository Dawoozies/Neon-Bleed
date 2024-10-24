using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paths : MonoBehaviour
{
    public static Paths ins;
    Path[] AllPaths;
    private void Awake()
    {
        ins = this;
        AllPaths = GetComponentsInChildren<Path>();
        foreach (Path path in AllPaths)
        {
            path.SetUpEdges();
        }
    }
    public Tuple<Transform, Transform>[] GetPath(int index)
    {
        if (index < 0 || index >= AllPaths.Length)
            return null;
        return AllPaths[index].GetEdges();
    }
}