using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tape : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        // Add tape to the inventory and disable the tape object
        Inventory.Instance.AddItem("Tape");
        this.gameObject.SetActive(false);
        // Display HUD message: "Picked up tape"
        // Assume HUDManager is a class that handles HUD messages
        HUDManager.ShowMessage("Picked up tape");
    }
}

