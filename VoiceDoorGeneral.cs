using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Collections;
using System.Collections.Generic;

public class VoiceDoorGeneral : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, GameObject> walls;
    private Dictionary<string, GameObject> writings;
    private Dictionary<string, Material> dissolveMaterials;
    private Dictionary<string, Material> writingMaterials;

    // Reference to the player GameObject (or another reference point)
    public GameObject player;

    // Public arrays to assign in the inspector
    public GameObject[] wallObjects;
    public Material[] wallDissolveMaterials;
    public GameObject[] writingObjects;
    public Material[] writingDisappearMaterials;
    public string[] commands;

    // The proximity range for the door to be affected
    public float proximityRange = 3f;
    public float dissolveDuration = 2f;
    public AudioClip dissolveSound;
    public AudioSource audioSource;

    void Start()
    {
        // Initialize the dictionaries
        walls = new Dictionary<string, GameObject>();
        writings = new Dictionary<string, GameObject>();
        dissolveMaterials = new Dictionary<string, Material>();
        writingMaterials = new Dictionary<string, Material>();

        for (int i = 0; i < commands.Length; i++)
        {
            walls.Add(commands[i], wallObjects[i]);
            writings.Add(commands[i], writingObjects[i]);
            dissolveMaterials.Add(commands[i], wallDissolveMaterials[i]);
            writingMaterials.Add(commands[i], writingDisappearMaterials[i]);

            // Initialize the materials
            wallDissolveMaterials[i].SetFloat("_Fade", 0f);
            writingDisappearMaterials[i].SetFloat("_Fade", 0f);
        }

        keywordRecognizer = new KeywordRecognizer(commands);
        keywordRecognizer.OnPhraseRecognized += OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        if (walls.ContainsKey(args.text) && IsPlayerNear(walls[args.text]))
        {
            Debug.Log($"{args.text} door command recognized");
            StartCoroutine(StartDissolveEffect(args.text));
        }
        else
        {
            Debug.LogWarning("Player is not in proximity or Wall GameObject reference is not set!");
        }
    }

    private bool IsPlayerNear(GameObject wall)
    {
        float distance = Vector3.Distance(player.transform.position, wall.transform.position);
        return distance <= proximityRange;
    }

    private IEnumerator StartDissolveEffect(string command)
    {
        GameObject wall = walls[command];
        GameObject writing = writings[command];
        Material dissolveMaterial = dissolveMaterials[command];
        Material writingMaterial = writingMaterials[command];

        float elapsedTime = 0f;

        while (elapsedTime < dissolveDuration)
        {
            elapsedTime += Time.deltaTime;
            float fade = Mathf.Clamp01(elapsedTime / dissolveDuration);
            dissolveMaterial.SetFloat("_Fade", fade);
            writingMaterial.SetFloat("_Fade", elapsedTime);
            yield return null;
        }

        dissolveMaterial.SetFloat("_Fade", 1f);
        writingMaterial.SetFloat("_Fade", 1f);
        wall.SetActive(false);
        writing.SetActive(false);

        // Optionally play sound if you have an AudioSource component attached
        // AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && dissolveSound != null)
        {
            audioSource.PlayOneShot(dissolveSound);
        }
    }
}
