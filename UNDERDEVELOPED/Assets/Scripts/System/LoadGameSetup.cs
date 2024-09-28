using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameSetup : MonoBehaviour
{
    [SerializeField]
    private SceneLoader _sceneLoader;

    private void Awake()
    {
        Debug.Log($"Set sceneToLoad to: {Player._instance.GetCurrentMap()}");
        _sceneLoader.SetSceneToLoad($"{Player._instance.GetCurrentMap()}");
    }

    //Enable the game manager when switchting to the next scene
   private void OnDestroy()
   {
        Debug.Log("GameManager enabled");
        GameSaveManager._instance.enabled = true;
   }
}
