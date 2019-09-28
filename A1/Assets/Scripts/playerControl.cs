using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    private float jumpVelocity = 15.0f;
    private float fallMulti = 20.0f;
    private float lowFallMulti = 5.0f;
    Rigidbody rb;

    public bool groundState;
    public bool liveState;
    Collider cl;

    //RETRIEVE COMPONENTS
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cl = GetComponent<Collider>();
    }

    //MANAGE PLAYER
    void updateState()
    {
        //++ +- -+ -- corners of player object, so we can corner jump.
        if (Physics.Raycast(new Vector3(transform.position.x + transform.localScale.x, transform.position.y, transform.position.z + transform.localScale.z), -Vector3.up, cl.bounds.extents.y + 0.1f) || Physics.Raycast(new Vector3(transform.position.x + transform.localScale.x, transform.position.y, transform.position.z - transform.localScale.z), -Vector3.up, cl.bounds.extents.y + 0.1f) || Physics.Raycast(new Vector3(transform.position.x - transform.localScale.x, transform.position.y, transform.position.z + transform.localScale.z), -Vector3.up, cl.bounds.extents.y + 0.1f) || Physics.Raycast(new Vector3(transform.position.x - transform.localScale.x, transform.position.y, transform.position.z - transform.localScale.z), -Vector3.up, cl.bounds.extents.y + 0.1f))
        {
            groundState = true;
        }
        else
        {
            groundState = false;
        }

        //If player falls off platform, they dead.
        if (transform.position.y < -50.0f)
        {
            liveState = false;
        }
       
    }
    void movePlayer()
    {
        //Horizontal planar movement.
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

        //Jump check and physics.
        if (groundState == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<Rigidbody>().velocity = Vector3.up * jumpVelocity;
            }
        }

        if (rb.velocity.y < 0f) //Holding jump key
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMulti - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0f && !Input.GetKey(KeyCode.Space)) //Tapping jump key
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowFallMulti - 1) * Time.deltaTime;
        }

        //Death check and respawn player.
        if(liveState == false)
        {
            Vector3 respawn = new Vector3(-29.0f, -14.5f, 6.0f);
            transform.position = Vector3.MoveTowards(transform.position, respawn, 1000 * Time.deltaTime);
            liveState = true;
        }
    }

    //UPDATE
    void Update()
    {
        updateState();
        movePlayer();
    }
}
