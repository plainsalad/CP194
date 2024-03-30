using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpLighterBlock : MonoBehaviour
{

    public GameObject LighterOnPlayer;
    // public GameObject item;
    public GameObject interactLighterText;
    public GameObject player;
    public GameObject hud;
    // public GameObject lighter;
    public GameObject cam;
    // public GameObject gameitem;
    public GameObject lighterInstructionText;
    // public GameObject lockObject;
    // public GameObject block;
    // public bool inReach;

    private FirstPersonMovement firstPersonMovementScript;
    private Jump jumpScript;
    private Crouch crouchScript;
    private FirstPersonLook firstPersonLookScript;
    private Zoom zoomScript;

    // Start is called before the first frame update
    void Start()
    {
        LighterOnPlayer.SetActive(false);
        lighterInstructionText.SetActive(false);
        InitializeReferences();
    }

    void InitializeReferences()
    {
        // Get references to the movement scripts
        firstPersonMovementScript = player.GetComponent<FirstPersonMovement>();
        jumpScript = player.GetComponent<Jump>();
        crouchScript = player.GetComponent<Crouch>();

        // Get reference to camera scripts
        // firstPersonLookScript = cam.GetComponent<FirstPersonLook>();
        zoomScript = cam.GetComponent<Zoom>();
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            interactLighterText.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                this.gameObject.SetActive(false);
                LighterOnPlayer.SetActive(true);
                interactLighterText.SetActive(false);
                lighterInstructionText.SetActive(true);
                DisablePlayerMovement();
            }
        }
        else
        {
            interactLighterText.SetActive(false);
        }
    }

    public void DisablePlayerMovement()
    {
        if (firstPersonMovementScript != null)
        {
            firstPersonMovementScript.enabled = false;
        }

        if (jumpScript != null)
        {
            jumpScript.enabled = false;
        }

        if (crouchScript != null)
        {
            crouchScript.enabled = false;
        }

        // if (firstPersonLookScript != null)
        // {
        //     firstPersonLookScript.enabled = false;
        // }

        if (zoomScript != null)
        {
            zoomScript.enabled = false;
        }
    }
}
