using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public GameObject GameOverScreenUI;
    public GameObject player;
    public GameObject cam;
    public GameObject lighter;
    public AudioSource blowOutSound;

    private bool gameOverTriggered = false;
    private FirstPersonMovement firstPersonMovementScript;
    private Jump jumpScript;
    private Crouch crouchScript;
    private FirstPersonLook firstPersonLookScript;
    private Zoom zoomScript;

    void Start()
    {
        GameOverScreenUI.SetActive(false);
        firstPersonMovementScript = player.GetComponent<FirstPersonMovement>();
        jumpScript = player.GetComponent<Jump>();
        crouchScript = player.GetComponent<Crouch>();

        // Get reference to camera scripts
        firstPersonLookScript = cam.GetComponent<FirstPersonLook>();
        zoomScript = cam.GetComponent<Zoom>();
    }

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    void OnCollisionEnter(Collision collision)
    {
        if (!gameOverTriggered && collision.gameObject.CompareTag("Player"))
        {
            // Play the sound without interrupting other sounds
            blowOutSound.PlayOneShot(blowOutSound.clip);

            // Set the flag to avoid triggering game over multiple times
            gameOverTriggered = true;

            // Delay showing the game over screen based on the sound duration
            float soundDuration = blowOutSound.clip.length;
            Invoke("ShowGameOverScreen", soundDuration);

            // Turn off the lighter object
            if (lighter != null)
            {
                lighter.SetActive(false);
            }
        }
    }

    void ShowGameOverScreen()
    {
        GameOverScreenUI.SetActive(true);
        firstPersonMovementScript.enabled = false;
        jumpScript.enabled = false;
        crouchScript.enabled = false;
        firstPersonLookScript.enabled = false;
        zoomScript.enabled = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        // cam.SetActive(true);
    }
}
