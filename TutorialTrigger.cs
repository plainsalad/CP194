using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject tutorialPrompt;
    private CanvasGroup canvasGroup;
    public float displayTime = 5.0f; // How long the prompt is displayed before starting to fade
    public float fadeDuration = 1.5f; // Duration of the fade effect

    private void Start()
    {
        // Ensure the tutorial prompt is inactive at start
        tutorialPrompt.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure your player GameObject has the "Player" tag
        {
            // Ensure there's a CanvasGroup component on the tutorial prompt
            if (tutorialPrompt.GetComponent<CanvasGroup>() == null)
            {
                canvasGroup = tutorialPrompt.AddComponent<CanvasGroup>();
            }
            else
            {
                canvasGroup = tutorialPrompt.GetComponent<CanvasGroup>();
            }

            tutorialPrompt.SetActive(true);
            canvasGroup.alpha = 1.0f; // Make sure the prompt is fully visible

            // Start the coroutine to display then fade out the prompt
            StartCoroutine(DisplayAndFadeOut());
        }
    }

    IEnumerator DisplayAndFadeOut()
    {
        // Wait for the specified display time
        yield return new WaitForSeconds(displayTime);

        // Gradually fade out the tutorial prompt
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = 1.0f - (elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the prompt is completely invisible and then disable it
        canvasGroup.alpha = 0.0f;
        tutorialPrompt.SetActive(false);
    }
}
