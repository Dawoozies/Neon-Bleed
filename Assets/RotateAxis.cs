using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAxis : MonoBehaviour
{
    public Vector3 axis;
    public float rotateSpeed;
    float angle;
    private void Update()
    {
        angle = Time.deltaTime * rotateSpeed;
        transform.Rotate(axis, angle);
    }
}
