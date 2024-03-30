using UnityEngine;

public class KeyPad : MonoBehaviour, IInteractable // Implement the IInteractable interface
{
    public GameObject InitialDesign;
    public GameObject SwitchStage;
    public GameObject interactText;
    public AudioSource bgMusic;

    void Start()
    {
        // Assuming bgMusic has been assigned in the inspector, otherwise use GetComponent as before
        if(bgMusic == null)
        {
            bgMusic = GameObject.Find("GameManager").GetComponent<AudioSource>();
        }

        // Make sure to disable the interact text initially
        if(interactText != null)
            interactText.SetActive(false);
    }

    // Implement the Interact method of IInteractable
    public void Interact()
    {
        SwitchStage.SetActive(true);
        InitialDesign.SetActive(false);
        
        // Optionally hide interactText if needed
        if(interactText != null)
            interactText.SetActive(false);

        // Change the ambient light and stop the background music
        RenderSettings.ambientLight = new Color(3f / 255f, 3f / 255f, 3f / 255f);
        bgMusic.Stop();
    }

    // You can remove OnTriggerEnter, OnTriggerExit, and the inReach variable

    // Update method can be removed if it's only used for interaction checking

    // The DoorOpens and DoorCloses methods and related commented code can be removed or un-commented and modified depending on the requirements of your keypad logic
}
