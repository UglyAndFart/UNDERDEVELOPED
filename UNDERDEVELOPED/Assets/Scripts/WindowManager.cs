using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainWindow;
    [SerializeField]
    private GameObject[] popWindows;
    [SerializeField]
    private KeyCode[] hotKeys;
    private bool popWindowOpen = false;
    private int currentWindowIndex;

    private void Awake()
    {
        mainWindow.SetActive(true);
        CloseAllWindows();
    }

    private void Update()
    {
        if(popWindowOpen)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                popWindows[currentWindowIndex].SetActive(false);
                popWindowOpen = false;
            }
            
            return;
        }
        
        foreach(KeyCode key in hotKeys)
        {
            if(Input.GetKeyDown(key))
            {
                currentWindowIndex = Array.IndexOf(hotKeys, key);
                popWindows[currentWindowIndex].SetActive(true);
                popWindowOpen = true;
            }
        }
    }

    private void CloseAllWindows()
    {
        foreach (GameObject window in popWindows)
        {
            window.SetActive(false);
        }
    }
}
