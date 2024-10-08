using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class VelocityComponent : MonoBehaviour, IVelocityComponent
{
    public Vector2 v => velocity;
    public Vector2 velocity;
}