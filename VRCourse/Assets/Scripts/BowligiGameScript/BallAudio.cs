using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Rigidbody))]
public class BallAudio : MonoBehaviour
{
    private AudioSource audioSource;
    private Rigidbody rb;

    public float minSpeedToPlay = 0.2f; // How fast the ball must move to play sound

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (GameManager.Instance == null || !GameManager.Instance.HasGameStarted())
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
            return;
        }

        if (rb.velocity.magnitude > minSpeedToPlay)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }
}
