using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    private GameObject _inventory;
    [SerializeField]
    private GameObject _tutorialMenu;
    [SerializeField]
    private GameObject _options;
    [SerializeField]
    private GameObject _ingameMenu;
    [SerializeField]
    private GameObject _playerStatus;
    [SerializeField]
    private GameObject _deathScreen;
    [SerializeField]
    private GameObject _challengeComplete;
    [SerializeField]
    private GameObject _saveIndicator;
    [SerializeField]
    private GameObject _bossHealthbar;
    [SerializeField]
    private GameObject _questComplete;

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

    public void OpenChallengeComplete()
    {
        _challengeComplete.SetActive(true);
    }

    public void CloseChallengeComplele()
    {
        _challengeComplete.SetActive(false);
    }

    public void OpenBossHealthbar()
    {
        _bossHealthbar.SetActive(true);
    }

    public void CloseBossHealthbar()
    {
        _bossHealthbar.SetActive(false);
    }

    public void OpenSaveIndicator()
    {
        _saveIndicator.SetActive(true);
    }

    public void CloseSaveIndicator()
    {
        _saveIndicator.SetActive(false);
    }

    public void OpenQuestComplete()
    {
        _questComplete.SetActive(true);
    }

    public void CloseQuestComplete()
    {
        _questComplete.SetActive(false);
    }

    public void OpenIngameMenu()
    {
        _ingameMenu.SetActive(true);
    }

    public void CloseIngameMenu()
    {
        _ingameMenu.SetActive(false);
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
