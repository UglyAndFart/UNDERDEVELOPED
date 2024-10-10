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
            if (_hudManager.activeUI == 0)
            {
                _hudManager.OpenCodeEditor();
            }
            // else if (_activeUI == 1)
            // {
            //     _hudManager.CloseCodeEditor();
            //     _activeUI = 0;
            // }

            // Debug.Log("HotKeysReader: CodeEditor Pressed");
        }
        else if (Input.GetButtonDown("Inventory"))
        {
            if (_hudManager.activeUI == 0)
            {
                _hudManager.OpenInventory();
            }
            else if (_hudManager.activeUI == 2)
            {
                _hudManager.CloseInventory();
            }

            // Debuwg.Log("HotKeysReader: Inventory Pressed");
        }
        else if (Input.GetButtonDown("Codex"))
        {  
            if (_hudManager.activeUI == 0)
            {
                _hudManager.OpenTutorial();
            }
            else if (_hudManager.activeUI == 3)
            {
                _hudManager.CloseTutorial();
            }
        }
        else if (Input.GetButtonDown("Options"))
        {  
            if (_hudManager.activeUI == 0)
            {
                _hudManager.OpenIngameMenu();
            }
            else if (_hudManager.activeUI == 4)
            {
                _hudManager.CloseIngameMenu();
            }
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            _hudManager.CloseCodeEditor();
            _hudManager.CloseInventory();
            _hudManager.CloseTutorial();
            _hudManager.CloseIngameMenu();
        }
        else
        {
            Debug.Log("HotkeysReader: No key :(");
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
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
