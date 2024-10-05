using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class IngameOptions : MonoBehaviour
{
    public static IngameOptions _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }

    private void Start()
    {
        HUDManager._instance.OpenOptions();
    }

    public void BackToMain()
    {
        if (SaveSystemManager._instance.transform.parent.gameObject.name != "Persistent GameObject")
        {
            Debug.LogWarning("IngameOptions: Persistent GameObject Not Found");
            return;
        }

        Destroy(SaveSystemManager._instance.transform.parent.gameObject);
        SceneLoader.LoadScene("Main Menu");

        // Debug.Log($"Destroying {CharacterPrefabLoader._instance.GetCurrentCharacter().transform.parent.transform.parent.gameObject}");
        // Destroy(CharacterPrefabLoader._instance.GetCurrentCharacter().transform.parent.transform.parent.gameObject);
        // PlayerManager._instance.enabled = false;
        // GameManager._instance.enabled = false;
        // SceneLoader.LoadNextScene("Main Menu");
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
        
        DirectoryManager.SetCurrentSaveFolder("");
    }
}
