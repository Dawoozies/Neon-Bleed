using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPools;

public class OutOfBoundsCheck : MonoBehaviour
{
    public Vector2 xBounds;
    public Vector2 yBounds;
    Camera mainCamera => CameraControl.ins.mainCamera;
    private void Update()
    {
        bool outOfBounds =
            transform.position.x < xBounds.x + mainCamera.transform.position.x 
            || transform.position.x > xBounds.y + mainCamera.transform.position.x
            || transform.position.y < yBounds.x + mainCamera.transform.position.y 
            || transform.position.y > yBounds.y + mainCamera.transform.position.y;
        if(outOfBounds)
        {
            SharedGameObjectPool.Return(gameObject);
        }
    }
}
