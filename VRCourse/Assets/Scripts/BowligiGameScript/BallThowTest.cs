using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BallThrowTest : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardForce = 1000f;
    private AudioSource audioSource;
    private bool hasPlayed = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.HasGameStarted())
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            Vector3 forward = new Vector3(-1, 0, 0); 
            rb.AddForce(forward * forwardForce);

            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
                hasPlayed = true;
            }
        }

        if (hasPlayed && rb.velocity.magnitude < 0.05f)
        {
            if (audioSource.isPlaying)
                audioSource.Stop();

            hasPlayed = false;
        }
    }
}
