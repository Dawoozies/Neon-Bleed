using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    public static MousePosition ins;
    private void Awake()
    {
        ins = this;
    }
    Camera mainCamera;
    public Vector2 ScreenPos;
    public Vector3 WorldPos;
    private void Start()
    {
        mainCamera = GetComponent<Camera>();
    }
    private void Update()
    {
        ScreenPos = Input.mousePosition;
        WorldPos = mainCamera.ScreenToWorldPoint(ScreenPos);
        WorldPos.z = 0f;
    }
}
