using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    // Update this to use TextMeshProUGUI
    public TextMeshProUGUI messageText;

    public static HUDManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public static void ShowMessage(string message)
    {
        Instance.messageText.text = message;
        // Optionally, hide the message after a few seconds
        Instance.StartCoroutine(Instance.ClearMessageAfterDelay(5)); // 5 seconds delay
    }

    private IEnumerator ClearMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText.text = "";
    }
}

