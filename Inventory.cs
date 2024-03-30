using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    private bool hasTape = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public bool CheckForItem(string itemName)
    {
        if (itemName == "Tape")
        {
            return hasTape;
        }
        return false;
    }

    public void AddItem(string itemName)
    {
        if (itemName == "Tape")
        {
            hasTape = true;
        }
    }
}

