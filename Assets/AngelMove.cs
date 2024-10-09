using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelMove : MonoBehaviour
{
    public Transform targetPoint;
    Rigidbody2D rb;
    //they come on screen go to where they need to be while shooting
    //only bosses have specific movement patterns that require a lot of thought
    //the normal angels just move and shoot
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
