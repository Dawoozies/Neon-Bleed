using Array2DEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class TrappingRainWater : MonoBehaviour
{
    [ReorderableList] public int[] heightMap;
    public Vector2Int levelDimensions;
    public Array2DString level;
    public int rainWaterQuantity;
    public bool compute;
    private void OnValidate()
    {
        levelDimensions.x = heightMap.Length;
        levelDimensions.y = MaxHeight(heightMap);
    }
    private void Update()
    {
        if(compute)
        {
            rainWaterQuantity = Trap(heightMap);
            compute = false;
        }
    }
    public int Trap(int[] height)
    {
        int m = MaxHeight(height);
        int n = height.Length;
        int[,] grid = new int[m,n];
        int[] heightValues = new int[n];
        Array.Copy(height, heightValues, n);
        for (int i = m-1; i >= 0; i--)
        {
            for(int j = 0; j < n; j++)
            {
                if (heightValues[j] > 0)
                {
                    heightValues[j]--;
                    grid[i, j]++;
                }
            }
        }
        int waterCount = 0;
        for (int i = 0; i < m; i++)
        {
            for(int j = 0; j < n; j++)
            {
                if (grid[i,j] > 0)
                {
                    continue;
                }
                if(j-1 < 0 || j+1 >= n)
                {
                    continue;
                }
                if (grid[i,j-1] > 0 || grid[i,j+1] > 0)
                {
                    grid[i, j] = 2;
                    waterCount++;
                }
            }
        }

        for (int i = m-1; i >= 0; i--)
        {
            for (int j = n-1; j >= 0; j--)
            {
                if (grid[i, j] != 2)
                {
                    continue;
                }
                if (grid[i, j - 1] == 0 || grid[i,j+1] == 0)
                {
                    grid[i, j] = 0;
                    waterCount--;
                }
            }
        }

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                string value = "";
                if (grid[i,j] == 1)
                {
                    value = "[]";
                }
                if (grid[i,j] == 2)
                {
                    value = "~";
                }
                level.SetCell(j,i, value);
            }
        }

        return waterCount;
    }
    public int MaxHeight(int[] height)
    {
        int currentMaximum = -1;
        for(int i = 0; i < height.Length; i++)
        {
            if (height[i] > currentMaximum)
                currentMaximum = height[i];
        }
        return currentMaximum;
    }
}
