using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private SavedGameFileLoader _savedGameFileLoader;
    [SerializeField]
    private GameObject mainWindow, loadWindow, optionWindow, exitWindow, _overwriteWindow;
    private GameObject currentWindow;

    private void Awake()
    {
        mainWindow.SetActive(true);
        loadWindow.SetActive(false);
        optionWindow.SetActive(false);
        exitWindow.SetActive(false);
    }

    private void Update()
    {
        if(currentWindow == null)
        {
            return;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            currentWindow.SetActive(false);
            currentWindow = null;
            mainWindow.SetActive(true);
        }
    }

    private void ReadLoadGame()
    {}

    public void LoadWindowOpen()
    {
        currentWindow = loadWindow;
        loadWindow.SetActive(true);
        mainWindow.SetActive(false);
    }

    public void LoadWindowClose()
    {
        currentWindow = null;
        loadWindow.SetActive(false);
        mainWindow.SetActive(true);
    }

    public void NewGame()
    {
        if (_savedGameFileLoader.SlotAvailable())
        {
            _overwriteWindow.SetActive(true);
            return;
        }
        
        SceneLoader.LoadScene("Computer Laboratory");
    }

    public void OptionWindowOpen()
    {
        currentWindow = optionWindow;
        optionWindow.SetActive(true);
        mainWindow.SetActive(false);
    }

    public void OptionWindowClose()
    {
        currentWindow = null;
        optionWindow.SetActive(false);
        mainWindow.SetActive(true);
    }

    public void ExitWindowOpen()
    {   
        currentWindow = exitWindow;
        exitWindow.SetActive(true);
        mainWindow.SetActive(false);
    }

    public void ExitWindowClose()
    {
        currentWindow = null;
        exitWindow.SetActive(false);
        mainWindow.SetActive(true);
    }

    public void ExitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
