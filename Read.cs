using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Read : MonoBehaviour, IInteractable
{
    // public static bool IsViewingItem = false;
    // public static Read Instance2 { get; private set; }
    public GameObject player;
    public GameObject noteUI;
    public GameObject hud;
    public GameObject lighter;
    public GameObject cam;

    private FirstPersonMovement firstPersonMovementScript;
    private Jump jumpScript;
    private Crouch crouchScript;
    private Zoom zoomScript;


    // Start is called before the first frame update
    void Start()
    {
        // Instance2 = this;
        noteUI.SetActive(false);
        hud.SetActive(true);
        lighter.SetActive(true);

        // Get references to the movement scripts
        firstPersonMovementScript = player.GetComponent<FirstPersonMovement>();
        jumpScript = player.GetComponent<Jump>();
        crouchScript = player.GetComponent<Crouch>();
        zoomScript = cam.GetComponent<Zoom>();
    }

    public void Interact()
    {
        // IsViewingItem = true;
        UIManager.Instance.SetUIActive(true);
        UIManager.Instance.CloseActiveUI += ExitButton;
        // Disable the movement scripts
        firstPersonMovementScript.enabled = false;
        jumpScript.enabled = false;
        crouchScript.enabled = false;
        zoomScript.enabled = false;

        // Show the note UI and adjust other UI elements
        noteUI.SetActive(true);
        hud.SetActive(false);
        lighter.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ExitButton()
    {
        // IsViewingItem = false;
        UIManager.Instance.SetUIActive(false);
        UIManager.Instance.CloseActiveUI -= ExitButton;
        UIManager.SetCursorState(false, CursorLockMode.Locked);
        // Enable the movement scripts
        firstPersonMovementScript.enabled = true;
        // jumpScript.enabled = true;
        crouchScript.enabled = true;
        zoomScript.enabled = true;

        noteUI.SetActive(false);
        hud.SetActive(true);
        lighter.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
