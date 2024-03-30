using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Collections;

public class VoiceDoor : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    public GameObject tutorialDetector;
    public CanvasGroup tutorialPromptCanvasGroup;
    public float fadeOutDuration = 1.5f;

    // Reference to the wall GameObject
    public GameObject wall;

    // Reference to the player GameObject (or another reference point)
    public GameObject player;

    public Material dissolveMaterial;

    public Material writingDisappearMaterial;
    public GameObject writing;

    public GameObject decoyEnemy;
    public GameObject realEnemy;

    // The proximity range for the door to be affected
    public float proximityRange = 3f;

    public float dissolveDuration = 2f;

    // public AudioSource dissolveSound;
    // public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        decoyEnemy.SetActive(true);
        realEnemy.SetActive(false);
        wall.GetComponent<MeshRenderer>().material = dissolveMaterial;
        writing.GetComponent<MeshRenderer>().material = writingDisappearMaterial;
        dissolveMaterial.SetFloat("_Fade", 0f);
        writingDisappearMaterial.SetFloat("_Fade", 0f);

        keywordRecognizer = new KeywordRecognizer(new string[] { "speak no evil", "where are my berries" });
        keywordRecognizer.OnPhraseRecognized += OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        // Check if the player is within the proximity range
        if (IsPlayerNear() && wall != null)
        {
            if (args.text == "speak no evil")
            {
                Debug.Log("Open door command recognized");
                // Disable the wall GameObject to make it disappear
                StartCoroutine(StartDissolveEffect());
                decoyEnemy.SetActive(false);
                realEnemy.SetActive(true);
                tutorialDetector.SetActive(false);
                
                if (tutorialPromptCanvasGroup != null)
                {
                    StartCoroutine(FadeOutTutorialPrompt());
                }
            }
        }
        else
        {
            Debug.LogWarning("Player is not in proximity or Wall GameObject reference is not set!");
        }
    }

    private IEnumerator FadeOutTutorialPrompt()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Clamp01(1.0f - (elapsedTime / fadeOutDuration));
            tutorialPromptCanvasGroup.alpha = newAlpha;
            yield return null;
        }
        tutorialPromptCanvasGroup.gameObject.SetActive(false);
    }

    // Check if the player is within the proximity range of the wall
    private bool IsPlayerNear()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, wall.transform.position);
            return distance <= proximityRange;
        }

        return false;
    }

    private IEnumerator StartDissolveEffect()
    {
        // Renderer wallRenderer = wall.GetComponent<Renderer>();
        // Material[] materials = wallRenderer.materials;

        // if (materials.Length > 0)
        // {
            // Material dissolveMat = materials[0];
            // dissolveMat.SetFloat("_Fade", 0f);
            float elapsedTime = 0f;

            while (elapsedTime < dissolveDuration)
            {
                elapsedTime += Time.deltaTime;
                float fade = Mathf.Clamp01(elapsedTime / dissolveDuration);
                dissolveMaterial.SetFloat("_Fade", fade);
                writingDisappearMaterial.SetFloat("_Fade", elapsedTime);
                yield return null;
            }

            // Ensure the fade is set to its final value
            dissolveMaterial.SetFloat("_Fade", 1f);
            writingDisappearMaterial.SetFloat("_Fade", 1f);
            // Optionally deactivate the wall gameobject if you want it to disappear
            wall.SetActive(false);
            writing.SetActive(false);
            // dissolveSound.Play();
        // }
    }
    // Update is called once per frame
    void Update()
    {
        // You can add any additional update logic here if needed
    }
}
