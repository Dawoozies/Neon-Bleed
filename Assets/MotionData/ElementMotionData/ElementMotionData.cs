using LitMotion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ElementMotionData", menuName = "ScriptableObjects/ElementMotionData", order = 3)]
public class ElementMotionData : ScriptableObject
{
    public float transitionTime;
    public Vector3 scale;
    public Gradient colorOverTransition;
    public Ease easing;
}