using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadNotes : MonoBehaviour
{
    // public static bool IsViewingNotes = false;
    // public static ReadNotes Instance4 { get; private set; }
    public GameObject player;
    public GameObject noteUI;
    public GameObject hud;
    public GameObject lighter;
    public GameObject cam;
    public bool inReach;
    public GameObject readNoteText;
    public AudioSource doorBang;

    private FirstPersonMovement firstPersonMovementScript;
    private Jump jumpScript;
    private Crouch crouchScript;
    // private FirstPersonLook firstPersonLookScript;
    private Zoom zoomScript;

    private bool notePickedUp = false;

    void Start()
    {
        // Instance4 = this;
        noteUI.SetActive(false);
        hud.SetActive(true);
        lighter.SetActive(true);
        inReach = false;

        // Get references to the movement scripts
        firstPersonMovementScript = player.GetComponent<FirstPersonMovement>();
        jumpScript = player.GetComponent<Jump>();
        crouchScript = player.GetComponent<Crouch>();

        // Get reference to camera scripts
        // firstPersonLookScript = cam.GetComponent<FirstPersonLook>();
        zoomScript = cam.GetComponent<Zoom>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            readNoteText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            readNoteText.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact") && inReach)
        {
            // IsViewingNotes = true;
            UIManager.Instance.SetUIActive(true);
            UIManager.Instance.CloseActiveUI += ExitButton;
            // Disable the movement scripts
            firstPersonMovementScript.enabled = false;
            jumpScript.enabled = false;
            crouchScript.enabled = false;
            // firstPersonLookScript.enabled = false;
            zoomScript.enabled = false;

            // Play the doorBang sound only if the note hasn't been picked up before
            if (!notePickedUp)
            {
                doorBang.Play();
                notePickedUp = true; // Set the flag to true after playing the sound
            }

            noteUI.SetActive(true);
            hud.SetActive(false);
            lighter.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ExitButton()
    {
        // IsViewingNotes = false;
        UIManager.Instance.SetUIActive(false);
        UIManager.Instance.CloseActiveUI -= ExitButton;
        UIManager.SetCursorState(false, CursorLockMode.Locked);
        // Enable the movement scripts
        firstPersonMovementScript.enabled = true;
        // jumpScript.enabled = true;
        crouchScript.enabled = true;
        // firstPersonLookScript.enabled = true;
        zoomScript.enabled = false;

        noteUI.SetActive(false);
        hud.SetActive(true);
        lighter.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
