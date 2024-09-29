using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager _instance; 

    [SerializeField]
    private GameObject _codeEditor;
    [SerializeField]
    private GameObject _playerStatus;
    [SerializeField]
    private GameObject _deathScreen;
    [SerializeField]
    private GameObject _invenroty;
    [SerializeField]
    private GameObject _options;
    
    public bool _codeEditorOnScreen = false;

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
        _codeEditorOnScreen = true;
        _codeEditor.SetActive(true);
    }

    public void CloseCodeEditor()
    {
        _codeEditorOnScreen = false;
        _codeEditor.SetActive(false);
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
        _invenroty.SetActive(true);
    }

    public void CloseInventory()
    {
        _invenroty.SetActive(false);
    }

    public void OpenOptions()
    {
        _options.SetActive(true);
    }

    public void CloseOptions()
    {
        _options.SetActive(false);
    }

    public void OpenDeathScreen()
    {
        _deathScreen.SetActive(true);
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
