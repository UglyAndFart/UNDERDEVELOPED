using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotkeysReader : MonoBehaviour
{
    public static HotkeysReader _instance;
    private HUDManager _hudManager;
    // [Header("Code Editor")]
    // [SerializeField]
    // private KeyCode _codeEditorKey;

    // [Header("Inventory")]
    // [SerializeField]
    // private KeyCode _inventoryKey;
    
    private int _activeUI = 0;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }

    private void  Start()
    {
        _hudManager = HUDManager._instance;
        Debug.Log("HotKeysReader: Started");
    }

    //_activeUI value 0 means no UI is open
    private void Update()
    {
        // Debug.Log(Input.GetButtonDown("CodeEditor"));
        if (Input.GetButtonDown("CodeEditor"))
        {
            if (_activeUI == 0)
            {
                _hudManager.OpenCodeEditor();
                _activeUI = 1;
            }
            else if (_activeUI == 1)
            {
                _hudManager.CloseCodeEditor();
                _activeUI = 0;
            }

            // Debug.Log("HotKeysReader: CodeEditor Pressed");
        }
        else if (Input.GetButtonDown("Inventory"))
       {
            if (_activeUI == 0)
            {
                _hudManager.OpenInventory();
                _activeUI = 2;
            }
            else if (_activeUI == 2)
            {
                _hudManager.CloseInventory();
                _activeUI = 0;
            }

            // Debug.Log("HotKeysReader: Inventory Pressed");
       }
        // if (Input.GetKeyDown(_codeEditorKey))
        // {
        //     if (_hudManager._codeEditorOnScreen)
        //     {
        //         _hudManager.CloseCodeEditor();
        //         return;
        //     }

        //     _hudManager.OpenCodeEditor();
        //     return;
        // }
        
        Debug.Log("HotkeysReader: No key :(");
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
