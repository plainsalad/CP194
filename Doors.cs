
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour, IInteractable // Implement the IInteractable interface
{
    public Animator door;
    // public GameObject openText; // This might no longer be necessary depending on how you want to signal interactability to the player

    public AudioSource doorSound;

    private bool isOpen = false; // Added to keep track of door state

    void Start()
    {
        // Initially, the door is closed, and openText (if used) is not visible
        // openText?.SetActive(false); // Using null-conditional operator for safety
    }

    // Implement the Interact method
    public void Interact()
    {
        // Toggle door state on interaction

        if (isOpen)
        {
            DoorCloses();
        }
        else
        {
            DoorOpens();
        }
    }

    void DoorOpens()
    {
        door.SetBool("Open", true);
        door.SetBool("Closed", false);
        Debug.Log("Door opens");
        doorSound.Play();
        isOpen = true; // Update state to open

        // Optionally hide the interact prompt if you don't want it to appear continuously
        // openText?.SetActive(false);
    }

    void DoorCloses()
    {
        door.SetBool("Open", false);
        door.SetBool("Closed", true);
        Debug.Log("Door closes");
        isOpen = false; // Update state to closed

        // Optionally show the interact prompt again if the door can be re-opened
        // openText?.SetActive(true);
    }
}
