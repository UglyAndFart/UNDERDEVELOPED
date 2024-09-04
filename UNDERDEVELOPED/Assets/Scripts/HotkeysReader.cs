using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotkeysReader : MonoBehaviour
{
    [SerializeField]
    private HUDManager hudManager;
    [SerializeField]
    private KeyCode _codeEditorKey;
    [SerializeField]
    private GameObject _codeEditorUI;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(_codeEditorKey) && !hudManager._codeEditorOnScreen)
        {
            hudManager.OpenCodeEditor();
            return;
        }
        
        if (Input.GetKeyDown(_codeEditorKey))
        {
            hudManager.CloseCodeEditor();
            return;
        }
    }
}
