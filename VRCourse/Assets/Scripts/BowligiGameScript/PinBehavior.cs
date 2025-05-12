using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PinBehavior : MonoBehaviour
{
    public AudioClip hitSound;
    public int scoreValue = 10; // Customize how much each pin is worth

    private AudioSource audioSource;
    private bool hasFallen = false;
    private bool gameStarted = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(WaitForGameStart());
    }

    private IEnumerator WaitForGameStart()
    {
        // Wait until the GameManager says the game has started
        while (GameManager.Instance != null && !GameManager.Instance.HasGameStarted())
        {
            yield return null;
        }

        gameStarted = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!gameStarted || hasFallen) return;

        if (hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        // Register as fallen based on tilt or strong impact
        if (collision.relativeVelocity.magnitude > 1f || IsKnockedOver())
        {
            hasFallen = true;

            // Add score to GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(scoreValue);
            }

            StartCoroutine(DisableAfterDelay(1.5f));
        }

        Debug.Log("Pin hit by: " + collision.gameObject.name);
    }

    private bool IsKnockedOver()
    {
        float angle = Vector3.Angle(Vector3.up, transform.up);
        return angle > 40f;
    }

    private IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
