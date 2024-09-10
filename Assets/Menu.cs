using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public Transform priorityTransform;
    public Transform elementsTransform;
    public Transform GetPriorityTransform()
    {
        return priorityTransform;
    }
    public Transform GetElementsTransform()
    {
        return elementsTransform;
    }
}