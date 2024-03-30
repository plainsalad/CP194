using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReadMap : MonoBehaviour, IInteractable
{
    // public static bool IsViewingMap = false;
    // public static ReadMap Instance { get; private set; }

    public GameObject player;
    public GameObject noteUI;
    public GameObject hud;
    public GameObject lighter;
    public GameObject cam;

    public GameObject ingamemap;

    public RawImage mapImage;

    public Material dissolveMaterial; // Assign the dissolve material in the inspector

    public float startDissolveDelay = 2f; // Delay before the dissolve starts
    public float dissolveDuration = 3f; // Duration of the dissolve effect

    private FirstPersonMovement firstPersonMovementScript;
    private Jump jumpScript;
    private Crouch crouchScript;
    private Zoom zoomScript;
    void Start()
    {
        // Instance = this;
        // Assigning the original material
        // mapImage.material = dissolveMaterial;

        noteUI.SetActive(false);
        hud.SetActive(true);
        lighter.SetActive(true);

        // Get references to the movement scripts
        firstPersonMovementScript = player.GetComponent<FirstPersonMovement>();
        jumpScript = player.GetComponent<Jump>();
        crouchScript = player.GetComponent<Crouch>();
        zoomScript = cam.GetComponent<Zoom>();
        dissolveMaterial.SetFloat("_Fade", 0f);
    }

    public void Interact()
    {
        // IsViewingMap = true;
        UIManager.Instance.SetUIActive(true);
        UIManager.Instance.CloseActiveUI += ExitButton;
        // Start the dissolve coroutine
        StartCoroutine(StartDissolveEffect());

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

    IEnumerator StartDissolveEffect()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(startDissolveDelay);

        float elapsedTime = 0f;

        while (elapsedTime < dissolveDuration)
        {
            elapsedTime += Time.deltaTime;
            float fade = Mathf.Clamp01(elapsedTime / dissolveDuration);
            dissolveMaterial.SetFloat("_Fade", fade);
            yield return null;
        }

        // Ensure the fade is set to its final value
        dissolveMaterial.SetFloat("_Fade", 1f);
        // Optionally deactivate the map image gameobject if you want it to disappear
        mapImage.gameObject.SetActive(false);
    }

    public void ExitButton()
    {
        // IsViewingMap = false;
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
        ingamemap.SetActive(false);
    }
}
