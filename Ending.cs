using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public GameObject gameWonPanel; // Assign this in the Inspector
    public AudioListener mainAudioListener;

    // References to other scripts and GameObjects
    public FirstPersonLook firstPersonLookScript;
    public Zoom zoomScript;
    public GameObject lighter;

    private void Start()
    {
        // Ensure the game won panel is hidden at the start
        gameWonPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Trigger game won sequence when the player touches the win trigger
        if (other.CompareTag("Player"))
        {
            GameWon();
        }
    }

    void GameWon()
    {
        // Show the game won panel
        gameWonPanel.SetActive(true);

        // Optional: Stop all gameplay actions or slow down time
        // Time.timeScale = 0f;

        // Disable main gameplay sounds and controls
        if (mainAudioListener != null)
        {
            mainAudioListener.enabled = false;
        }

        if (firstPersonLookScript != null)
        {
            firstPersonLookScript.enabled = false;
        }
        
        if (zoomScript != null)
        {
            zoomScript.enabled = false;
        }

        if (lighter != null)
        {
            lighter.SetActive(false);
        }

        // Unlock and show the cursor for UI interaction
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Consider adding any calls to UIManager or other game management systems to handle game won state
    }

    public void LoadMainMenu()
    {
        // Load your main menu scene
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f; // Ensure game time is normal

        // Optionally reset any states or configurations
    }
}
