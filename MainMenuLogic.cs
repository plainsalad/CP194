using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    private GameObject mainMenu;
    private GameObject controlsMenu;
    private GameObject storyMenu;
    private GameObject loadingMenu;
    private GameObject storyWIPMenu;
    private GameObject testMenu;
    public AudioSource buttonSound;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu = GameObject.Find("MainMenuCanvas");
        controlsMenu = GameObject.Find("ControlsCanvas");
        storyMenu = GameObject.Find("StoryCanvas");
        loadingMenu = GameObject.Find("LoadingCanvas");
        storyWIPMenu = GameObject.Find("StoryWIPCanvas");
        testMenu = GameObject.Find("TestCanvas");

        mainMenu.GetComponent<Canvas>().enabled = true;
        controlsMenu.GetComponent<Canvas>().enabled = false;
        storyMenu.GetComponent<Canvas>().enabled = false;
        loadingMenu.GetComponent<Canvas>().enabled = false;
        storyWIPMenu.GetComponent<Canvas>().enabled = false;
        testMenu.GetComponent<Canvas>().enabled = false;
    }

    public void ContinueButton()
    {
        loadingMenu.GetComponent<Canvas>().enabled = true;
        storyMenu.GetComponent<Canvas>().enabled = false;
        buttonSound.Play();
        SceneManager.LoadScene("VEretical SLice");
    }

    public void StartButton()
    {
        storyMenu.GetComponent<Canvas>().enabled = true;
        mainMenu.GetComponent<Canvas>().enabled = false;
        buttonSound.Play();
    }

    public void StoryWIPButton()
    {
        storyWIPMenu.GetComponent<Canvas>().enabled = true;
        mainMenu.GetComponent<Canvas>().enabled = false;
        buttonSound.Play();
    }

    public void ContinueWIPButton()
    {
        loadingMenu.GetComponent<Canvas>().enabled = true;
        storyWIPMenu.GetComponent<Canvas>().enabled = false;
        buttonSound.Play();
        SceneManager.LoadScene("Scene_A");
    }

    public void ControlsButton()
    {
        controlsMenu.GetComponent<Canvas>().enabled = true;
        mainMenu.GetComponent<Canvas>().enabled = false;
        buttonSound.Play();
    }

    public void TestButton()
    {
        testMenu.GetComponent<Canvas>().enabled = true;
        mainMenu.GetComponent<Canvas>().enabled = false;
        buttonSound.Play();
    }

    public void MicCheckButton()
    {
        loadingMenu.GetComponent<Canvas>().enabled = true;
        testMenu.GetComponent<Canvas>().enabled = false;
        buttonSound.Play();
        SceneManager.LoadScene("MicCheckScene");
    }

    public void VoiceRecSceneButton()
    {
        loadingMenu.GetComponent<Canvas>().enabled = true;
        testMenu.GetComponent<Canvas>().enabled = false;
        buttonSound.Play();
        SceneManager.LoadScene("Speak2");
    }

    public void BackButton()
    {
        controlsMenu.GetComponent<Canvas>().enabled = false;
        storyMenu.GetComponent<Canvas>().enabled = false;
        storyWIPMenu.GetComponent<Canvas>().enabled = false;
        testMenu.GetComponent<Canvas>().enabled = false;
        mainMenu.GetComponent<Canvas>().enabled = true;
        buttonSound.Play();
    }

    public void QuitButton()
    {
        // This function will be called when the Quit button is clicked
        buttonSound.Play();
        Debug.Log("Quit button clicked");
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
