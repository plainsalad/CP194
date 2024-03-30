using System.Collections;
using UnityEngine;

public class InteractLock : MonoBehaviour, IInteractable
{
    public GameObject item;
    public GameObject interactItemText;
    public GameObject player;
    public GameObject hud;
    public GameObject lighter;
    public GameObject cam;
    public GameObject gameitem;
    public GameObject locktext;
    public GameObject lockObject;
    public GameObject block;
    public bool inReach;

    private FirstPersonMovement firstPersonMovementScript;
    private Jump jumpScript;
    private Crouch crouchScript;
    // private FirstPersonLook firstPersonLookScript;
    private Zoom zoomScript;

    // Start is called before the first frame update
    void Start()
    {
        InitializeReferences();
        SetInitialState();
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

    void SetInitialState()
    {
        item.SetActive(false);
        gameitem.SetActive(true);
        if (interactItemText != null) interactItemText.SetActive(false);
    }

    public void Interact()
    {
        // This method gets called when the Interactor script detects an interaction.
        DisablePlayerMovement();
        EnableItem();
        if (interactItemText != null) interactItemText.SetActive(true); // Optionally, show interact text
    }

    // void Update()
    // {
    //     if (Input.GetButtonDown("Interact") && inReach)
    //     {
    //         DisablePlayerMovement();
    //         // firstPersonLookScript.enabled = true;
    //         EnableItem();
    //     }

    //     if (Input.GetButtonDown("Fire2"))
    //     {
    //         Debug.Log("Cancel button pressed.");
    //         EnablePlayerMovement();
    //         DisableItem();
    //     }
    // }

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

    public void EnablePlayerMovement()
    {
        if (firstPersonMovementScript != null)
        {
            firstPersonMovementScript.enabled = true;
        }

        if (jumpScript != null)
        {
            jumpScript.enabled = true;
        }

        if (crouchScript != null)
        {
            crouchScript.enabled = true;
        }

        // if (firstPersonLookScript != null)
        // {
        //     firstPersonLookScript.enabled = true;
        // }

        if (zoomScript != null)
        {
            zoomScript.enabled = true;
        }
    }

    public void EnableItem()
    {
        item.SetActive(true);
        locktext.SetActive(true);
        gameitem.SetActive(false);
        hud.SetActive(false);
        lighter.SetActive(true);
        // Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void DisableItem()
    {
        item.SetActive(false);
        locktext.SetActive(false);
        gameitem.SetActive(true);
        hud.SetActive(true);
        lighter.SetActive(true);
        // Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void CorrectCode()
    {
        EnablePlayerMovement();
        DisableItem();
        interactItemText.SetActive(false);
        Destroy(lockObject);
        Destroy(block);
        Destroy(lighter);
    }

}
