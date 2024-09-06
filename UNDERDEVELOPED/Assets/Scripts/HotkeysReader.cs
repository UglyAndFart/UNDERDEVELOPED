using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotkeysReader : MonoBehaviour
{
    [SerializeField]
    private HUDManager _hudManager;
    [SerializeField]
    private KeyCode _codeEditorKey;
    [SerializeField]
    private GameObject _codeEditorUI;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(_codeEditorKey) && !_hudManager._codeEditorOnScreen)
        {
            _hudManager.OpenCodeEditor();
            return;
        }
        
        if (Input.GetKeyDown(_codeEditorKey))
        {
            _hudManager.CloseCodeEditor();
            return;
        }
    }
}
