using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionDetector : MonoBehaviour
{
    public GameObject gameOverPanel; // Assign this in the Inspector
    public AudioListener mainAudioListener;

    // Add a public reference to the FirstPersonLook script
    public FirstPersonLook firstPersonLookScript;

    public Zoom zoomScript;
    
    public GameObject lighter;

    private void Start()
    {
        // Hide the Game Over UI Panel at the start
        gameOverPanel.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Assuming the enemy has a tag called "Enemy"
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("Player collided with an enemy!");
            // Game Over logic
            GameOver();
        }
    }

    void GameOver()
    {
        // Activate the Game Over UI Panel
        gameOverPanel.SetActive(true);

        // Optionally, stop the game
        Time.timeScale = 0f;

        mainAudioListener.enabled = false;
        if (firstPersonLookScript != null)
        {
            firstPersonLookScript.enabled = false;
            zoomScript.enabled = false;
            lighter.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Set the game over state in UIManager
        UIManager.Instance.IsGameOver = true;
    }

    public void RetryLevel()
    {
        // Assuming you're using SceneManagement
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f; // Make sure to resume normal time scale
        mainAudioListener.enabled = true;

        // Enable the FirstPersonLook script
        if (firstPersonLookScript != null)
        {
            firstPersonLookScript.enabled = true;
            zoomScript.enabled = true;
            lighter.SetActive(true);
            UIManager.SetCursorState(false, CursorLockMode.Locked);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        // Reset the game over state in UIManager
        UIManager.Instance.IsGameOver = false;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f; // Resume normal time scale
        // Reset the game over state in UIManager
        UIManager.Instance.IsGameOver = false;
    }
}
