using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PinBehavior : MonoBehaviour
{
    public AudioClip hitSound;
    private AudioSource audioSource;
    private bool hasBeenHit = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Don’t do anything if the game hasn’t started or already processed this pin
        if (!GameManager.Instance.HasGameStarted() || hasBeenHit) return;

        // Check if hit by the ball or another pin
        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("Pin"))
        {
            hasBeenHit = true;

            // Play hit sound
            if (hitSound != null)
                audioSource.PlayOneShot(hitSound);

            // Add to score
            GameManager.Instance.AddScore(10);

            // Wait a moment before disabling to let the physics look natural
            StartCoroutine(DisableAfterDelay(1f));
        }

        Debug.Log("Pin hit by: " + collision.gameObject.name);
    }

    private IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
