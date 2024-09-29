using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameOptions : MonoBehaviour
{
    public void BackToMain()
    {
        if (SaveSystemManager._instance.transform.parent.gameObject.name != "Persistent GameObject")
        {
            Debug.LogWarning("IngameOptions: Persistent GameObject Not Found");
            return;
        }

        Destroy(SaveSystemManager._instance.transform.parent.gameObject);
        SceneLoader.LoadNextScene("Main Menu");

        // Debug.Log($"Destroying {CharacterPrefabLoader._instance.GetCurrentCharacter().transform.parent.transform.parent.gameObject}");
        // Destroy(CharacterPrefabLoader._instance.GetCurrentCharacter().transform.parent.transform.parent.gameObject);
        // PlayerManager._instance.enabled = false;
        // GameManager._instance.enabled = false;
        // SceneLoader.LoadNextScene("Main Menu");
    }
}
