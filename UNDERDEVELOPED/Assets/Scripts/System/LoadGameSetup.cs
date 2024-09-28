using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameSetup : MonoBehaviour
{   
    [SerializeField]
    private SceneLoader _sceneLoader;
    
    private void Awake()
    {
        PlayerManager._instance.enabled = true;
        Player._instance.SetPlayerData();
        CharacterPrefabLoader._instance.EnableCharacter();
        GameManager._instance.enabled = true;
    }

    private void Start()
    {
        Debug.Log($"SceneToLoad: {Player._instance.GetCurrentMap()}");
        _sceneLoader.SetSceneToLoad(Player._instance.GetCurrentMap());
    }
}
