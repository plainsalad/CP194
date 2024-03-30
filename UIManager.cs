using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private bool isUIActive = false;
    public bool IsGameOver { get; set; } // Flag indicating the game over state

    // Action to close the current active UI
    public event Action CloseActiveUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsUIActive()
    {
        return isUIActive;
    }

    public void SetUIActive(bool active)
    {
        isUIActive = active;
        if (isUIActive)
        {
            SetCursorState(true, CursorLockMode.None);
        }
        else
        {
            SetCursorState(false, CursorLockMode.Locked);
        }
    }

    public void RequestCloseActiveUI()
    {
        CloseActiveUI?.Invoke();
    }
    public static void SetCursorState(bool visible, CursorLockMode lockMode)
    {
        Cursor.visible = visible;
        Cursor.lockState = lockMode;
    }

}
