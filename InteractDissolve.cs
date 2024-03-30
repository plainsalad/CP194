using UnityEngine;
using System.Collections;

public class InteractDissolve : MonoBehaviour, IInteractable
{
    public GameObject ingameItem;

    // Reference to the player GameObject (or another reference point)
    public GameObject player;

    public Material dissolveMaterial;


    public float dissolveDuration = 2f;


    // Start is called before the first frame update
    void Start()
    {
        ingameItem.GetComponent<MeshRenderer>().material = dissolveMaterial;
        dissolveMaterial.SetFloat("_Fade", 0f);
    }

    public void Interact()
    {
        StartCoroutine(StartDissolveEffect());
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
                yield return null;
            }

            // Ensure the fade is set to its final value
            dissolveMaterial.SetFloat("_Fade", 1f);
            // Optionally deactivate the wall gameobject if you want it to disappear
            ingameItem.SetActive(false);
            // dissolveSound.Play();
        // }
    }
    // Update is called once per frame
    void Update()
    {
        // You can add any additional update logic here if needed
    }
}
