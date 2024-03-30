using UnityEngine;

public class PlayedAudioOnce : MonoBehaviour
{
    public AudioSource audioSource; // Assign in inspector or find dynamically
    public AudioClip audioClip; 
    private bool hasPlayed = false;

    private void Start()
    {
        // Optionally, get the AudioSource component if not assigned
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        audioSource.clip = audioClip;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the player and the audio hasn't been played yet
        if (other.CompareTag("Player") && !hasPlayed)
        {
            audioSource.Play();
            hasPlayed = true; // Set the flag to true to prevent future playback
        }
    }
}
