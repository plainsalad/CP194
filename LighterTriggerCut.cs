using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LighterTriggerCut : MonoBehaviour
{
    public PlayableDirector cutsceneDirector;

    public GameObject lighter;
    public GameObject flames;
    public GameObject flamesTexture;
    public AudioSource lighterAudio; // Add an AudioSource component to the same GameObject
    public AudioClip igniteSound; // Assign the clicking sound clip in the Inspector

    private bool isOn;

    void Start()
    {
        isOn = false;
        flames.SetActive(false);
        lighterAudio = GetComponent<AudioSource>();
        // Disable the cutscene at the start
        cutsceneDirector.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && lighter.activeInHierarchy && !isOn)
        {
            flames.SetActive(true);
            flamesTexture.SetActive(true);
            isOn = true;
            cutsceneDirector.gameObject.SetActive(true);

            // Register to the PlayablesDone event
            cutsceneDirector.stopped += OnCutsceneFinished;

            // Play the cutscene
            cutsceneDirector.Play();

            // Play the ignition sound
            PlayIgniteSound();
        }
        else if (Input.GetButtonDown("Fire1") && isOn)
        {
            return;
        }
    }

    void OnCutsceneFinished(PlayableDirector director)
    {
        // Unregister the event to avoid multiple calls
        director.stopped -= OnCutsceneFinished;

        // AdjustLightingForScene();
        // Load the next scene in the background
        LoadNextSceneAsync();
    }

    void PlayIgniteSound()
    {
        // Check if AudioSource and AudioClip are assigned
        if (lighterAudio != null && igniteSound != null)
        {
            lighterAudio.clip = igniteSound;
            lighterAudio.Play();
        }
    }

    // void AdjustLightingForScene()
    // {
    //     // Access the Lighting window settings
    //     UnityEditor.Lightmapping.giWorkflowMode = UnityEditor.Lightmapping.GIWorkflowMode.OnDemand;

    //     // Adjust lighting settings based on your specifications
    //     RenderSettings.skybox = null; // No Skybox Material
    //     RenderSettings.sun = null; // No Sun Source

    //     // Set your HSV color (this needs to be applied to a light or other elements)
    //     // ...

    //     RenderSettings.ambientMode = AmbientMode.Flat;
    //     RenderSettings.ambientLight = new Color(26f / 255f, 26f / 255f, 26f / 255f, 1f); // RGB: 26, 26, 26

    //     QualitySettings.realtimeReflectionProbes = false;
    //     // QualitySettings.intendedResolution = 128;
    //     QualitySettings.resolutionScalingFixedDPIFactor = 1;
    //     QualitySettings.maxQueuedFrames = 1;

    //     // Disable Fog
    //     RenderSettings.fog = false;
    // }

    void LoadNextSceneAsync()
    {
        // Use LoadSceneAsync to load the next scene in the background
        SceneManager.LoadScene("VEretical SLice");
    }
}
