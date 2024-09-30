using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotkeysReader : MonoBehaviour
{
    public static HotkeysReader _instance;
    private HUDManager _hudManager;
    [Header("Code Editor")]
    [SerializeField]
    private KeyCode _codeEditorKey;

    [Header("Inventory")]
    [SerializeField]
    private KeyCode _inventoryKey;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;
        _hudManager = HUDManager._instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_codeEditorKey))
        {
            if (_hudManager._codeEditorOnScreen)
            {
                _hudManager.CloseCodeEditor();
                return;
            }

            _hudManager.OpenCodeEditor();
            return;
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
