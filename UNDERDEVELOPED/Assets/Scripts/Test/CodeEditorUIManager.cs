using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CodeEditorUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _editor;
    [SerializeField]
    private GameObject _console;
    private void OnEnable()
    {
        _editor.SetActive(true);
        _console.SetActive(false);
    }

    public void EditorClick()
    {
        _editor.SetActive(true);
        _console.SetActive(false);
    }

    public void ConsoleClick()
    {
        _editor.SetActive(false);
        _console.SetActive(true);
    }
}
