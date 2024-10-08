using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager _instance; 

    public delegate void HUDManagerHandler();
    public static event HUDManagerHandler OnUIOpen;
    public static event HUDManagerHandler OnUIClose;

    [SerializeField]
    private GameObject _codeEditor;
    [SerializeField]
    private GameObject _playerStatus;
    [SerializeField]
    private GameObject _deathScreen;
    [SerializeField]
    private GameObject _inventory;
    [SerializeField]
    private GameObject _options;
    [SerializeField]
    private GameObject _tutorialButton;
    [SerializeField]
    private GameObject _tutorialMenu;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }

    public void OpenCodeEditor()
    {
        _codeEditor.SetActive(true);

        OnUIOpen?.Invoke();
    }

    public void CloseCodeEditor()
    {
        _codeEditor.SetActive(false);

        OnUIClose?.Invoke();
    }

    public void OpenPlayerStatus()
    {
        _playerStatus.SetActive(true);
        
        // OnPlayerStatusOpened?.Invoke();
    }

    public void ClosePlayerStatus()
    {
        _playerStatus.SetActive(false);
    }

    public void OpenInventory()
    {
        _inventory.SetActive(true);

        OnUIOpen?.Invoke();
    }

    public void CloseInventory()
    {
        _inventory.SetActive(false);

        OnUIClose?.Invoke();
    }

    public void OpenOptions()
    {
        _options.SetActive(true);
    }

    public void CloseOptions()
    {
        _options.SetActive(false);
    }

    public void OpenTutorial()
    {
        _tutorialMenu.SetActive(true);
        OnUIOpen?.Invoke();
    }

    public void CloseTutorial()
    {
        _tutorialMenu.SetActive(false);
        OnUIClose?.Invoke();
    }

    public void OpenDeathScreen()
    {
        _deathScreen.SetActive(true);
    }

    public void CloseDeathScreen()
    {
        _deathScreen.SetActive(false);
    }
    
    public void OpenTutorialButton()
    {
        _tutorialButton.SetActive(true);
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
