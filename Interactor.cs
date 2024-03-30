using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;
    public GameObject interactPrompt; // Assign the UI Text or UI element in the inspector

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the interact prompt is not visible at the start
        interactPrompt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check for interaction raycast
        Ray interactionRay = new Ray(InteractorSource.position, InteractorSource.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(interactionRay, out hitInfo, InteractRange))
        {
            // Check if the object is interactable
            IInteractable interactObj = hitInfo.collider.GetComponent<IInteractable>();
            
            if (interactObj != null)
            {
                // Enable the interact prompt UI if in range
                interactPrompt.SetActive(true);

                // If the interact key is pressed
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactObj.Interact();
                }
            }
            else
            {
                // Disable the interact prompt UI if the object is not interactable
                interactPrompt.SetActive(false);
            }
        }
        else
        {
            // Disable the interact prompt UI if not in range
            interactPrompt.SetActive(false);
        }
    }
}
