using UnityEngine;

// Ensure the class implements IInteractable if it's part of your project structure.
public class Phone : MonoBehaviour, IInteractable
{
    // Audio components to play the ringing and dialogue
    public AudioSource audioSource;
    public AudioClip ringSound;
    public AudioClip dialogueSound;

    private bool hasInteracted = false;

    void Start()
    {
        // Start ringing when the game starts.
        audioSource.clip = ringSound;
        audioSource.loop = true; // Assuming you want the ring to loop.
        audioSource.Play();
    }

    public void Interact()
    {
        // Check if the phone has been interacted with already.
        if (!hasInteracted)
        {
            // Stop the ringing sound.
            audioSource.Stop();

            // Play the dialogue sound.
            audioSource.clip = dialogueSound;
            audioSource.loop = false; // Dialogue typically doesn't loop.
            audioSource.Play();

            // Set the flag so that interaction only happens once.
            hasInteracted = true;
        }
    }
}
