using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrowTest : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardForce = 500f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Send the ball down the lane in the correct direction
            Vector3 forward = new Vector3(-1, 0, 0); 
            rb.AddForce(forward * forwardForce);
        }
    }
}

