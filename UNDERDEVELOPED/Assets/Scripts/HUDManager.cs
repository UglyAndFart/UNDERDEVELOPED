using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _codeEditor;
    [SerializeField]
    private GameObject _playerStatus;
    
    public bool _codeEditorOnScreen = false;

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
    }

    public void ClosePlayerStatus()
    {
        _playerStatus.SetActive(false);
    }
}
