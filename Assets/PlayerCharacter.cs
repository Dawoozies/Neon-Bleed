using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public Vector2 input;
    public float jumpInput;
    public Vector2 gravity;
    public float maxAirTime;
    float airTime;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), 0f);
        if( Input.GetButton("Jump"))
        {
            jumpInput = 1f;
        }
        else
        {
            jumpInput = 0f;
        }
    }
    private void FixedUpdate()
    {
    }
}
