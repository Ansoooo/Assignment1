using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    private float jumpVelocity = 15.0f;
    private float fallMulti = 10.0f;
    private float lowFallMulti = 8.0f;
    Rigidbody rb;

    public bool groundState;
    Collider cl;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cl = GetComponent<Collider>();
    }
    void updateState()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, cl.bounds.extents.y + 0.1f))
        {
            groundState = true;
        }
        else
        {
            groundState = false;
        }
    }
    void movePlayer()
    {
        // HORIZONTAL PLANAR MOVEMENTS
        if (Input.GetKey(KeyCode.W)) // Forward
        {
            transform.Translate(0f, 0f, 30f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S)) // Backward
        {
            transform.Translate(0f, 0f, -30f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A)) // Left
        {
            transform.Translate(-30f * Time.deltaTime, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.D)) // Right
        {
            transform.Translate(30f * Time.deltaTime, 0f, 0f);
        }

        //JUMP PHYSICS
        if (groundState == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<Rigidbody>().velocity = Vector3.up * jumpVelocity;
            }
        }

        if (rb.velocity.y < 0f)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMulti - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0f && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowFallMulti - 1) * Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateState();
        movePlayer();
    }
}
