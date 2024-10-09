using UnityEngine;

public class BossArea : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player enters the boss area
        if (other.CompareTag("Player")) // Make sure your player GameObject has the tag "Player"
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player exits the boss area
        if (other.CompareTag("Player"))
        {
            if (audioSource != null)
            {
                audioSource.Stop();
            }
        }
    }
}
