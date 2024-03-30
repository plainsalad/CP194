using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeRecorder : MonoBehaviour, IInteractable
{
    public AudioClip tapeAudio;
    public AudioSource tapePlayer;

    public void Start()
    {
        tapePlayer.clip = tapeAudio;
    }
    public void Interact()
    {
        bool hasTape = Inventory.Instance.CheckForItem("Tape");
        if (hasTape)
        {
            // Logic to play the tape
            // Display HUD message: "Playing tape"
            HUDManager.ShowMessage("Playing tape");
            tapePlayer.Play();
        }
        else
        {
            // Display HUD message: "You need a tape"
            HUDManager.ShowMessage("You need a tape");
        }
    }
}

