using System.Collections;
using UnityEngine;

public class AudioDelay : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip yourAudioClip;
    // adjust volume of the audio clip
    public float volume = 1f;
    // adjust priority of the audio clip
    public int priority = 128;
    
    public float delay = 5f;

    void Start()
    {
        // Set the audio clip to play on the AudioSource
        audioSource.clip = yourAudioClip;

        // Start the coroutine to play the audio with a delay
        StartCoroutine(PlayAudioWithDelay());
    }

    IEnumerator PlayAudioWithDelay()
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(delay);

        // Play the audio clip on the AudioSource
        audioSource.Play();
    }
}
