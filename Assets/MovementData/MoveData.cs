using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class MoveData : ScriptableObject
{
    public Vector2 moveAxis;
    public float minMove, maxMove;
    public AnimationCurve moveCurve;
    public float driverValue, driverMaxValue;
    public virtual void DriveMovement(float timeDelta)
    {
    }
}
