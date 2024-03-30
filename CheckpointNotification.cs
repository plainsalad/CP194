using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointNotification : MonoBehaviour
{
    public float displayTime = 2.0f; // How long the notification is displayed
    public float fadeDuration = 1.0f; // Duration of the fade effect
    private CanvasGroup canvasGroup;

    void Awake()
    {
        // Get or add a CanvasGroup to the UI element to control its opacity
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // Initially set the notification to be fully visible
        canvasGroup.alpha = 1.0f;

        // Start the sequence to display and then fade out the notification
        StartCoroutine(DisplayAndFadeOut());
    }

    IEnumerator DisplayAndFadeOut()
    {
        // Wait for the specified display time
        yield return new WaitForSeconds(displayTime);

        // Gradually fade out the notification
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = 1.0f - (elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the notification is completely invisible and then disable it
        canvasGroup.alpha = 0.0f;
        gameObject.SetActive(false);
    }
}
