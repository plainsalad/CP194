using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameWon : MonoBehaviour
{
    // public GameObject gameWonScreenUI;
    public GameObject player;
    public GameObject enemy;
    public GameObject cam;

    private FirstPersonMovement firstPersonMovementScript;
    private Jump jumpScript;
    private Crouch crouchScript;
    private FirstPersonLook firstPersonLookScript;
    private Zoom zoomScript;
    public AudioSource dialogue;
    public AudioClip sisterClip;
    public AudioClip brotherClip;
    public AudioClip loadingClip;


    void Start()
    {
        // attach sister audio clip to the dialogue AudioSource
        firstPersonMovementScript = player.GetComponent<FirstPersonMovement>();
        jumpScript = player.GetComponent<Jump>();
        crouchScript = player.GetComponent<Crouch>();

        // Get reference to camera scripts
        firstPersonLookScript = cam.GetComponent<FirstPersonLook>();
        zoomScript = cam.GetComponent<Zoom>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetGameWonState();
        }
    }

    private void SetGameWonState()
    {
        // Enable the game won screen UI
        // if (gameWonScreenUI != null)
        // {
        //     gameWonScreenUI.SetActive(true);
        // }

        // Disable the player
        firstPersonMovementScript.enabled = false;
        jumpScript.enabled = false;
        crouchScript.enabled = false;
        // firstPersonLookScript.enabled = false;
        zoomScript.enabled = false;

        enemy.SetActive(false);

        StartCoroutine(PlayDialogueSequence());

        // Show and unlock the cursor
        // Cursor.visible = true;
        // Cursor.lockState = CursorLockMode.None;

        // Enable or activate any additional elements like cameras
        // if (mainCamera != null)
        // {
        //     mainCamera.SetActive(true);
        // }
    }
    private IEnumerator PlayDialogueSequence()
    {
        dialogue.clip = sisterClip;
        dialogue.Play();
        yield return new WaitForSeconds(sisterClip.length);
        dialogue.clip = brotherClip;
        dialogue.Play();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Speak");
        asyncLoad.allowSceneActivation = false;

        yield return new WaitForSeconds(brotherClip.length);

        dialogue.clip =loadingClip;
        dialogue.Play();
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                if (!dialogue.isPlaying)
                {
                    asyncLoad.allowSceneActivation = true;
                }
            }
            yield return null;


        }
    }
}
