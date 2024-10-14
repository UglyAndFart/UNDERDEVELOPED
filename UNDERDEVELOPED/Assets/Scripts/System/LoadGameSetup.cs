using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameSetup : MonoBehaviour
{   
    [SerializeField]
    private SceneLoader _sceneLoader;
    
    //Enable all necessary objects
    private void Awake()
    {
        PlayerManager._instance.enabled = true;
        GameEnvironment._instance.SetGameEnvironment();
        Player._instance.SetPlayer();
        CharacterPrefabLoader._instance.EnableCharacter();
        TopDownMovementController._instance.enabled = true;
        TopDownMovementController._instance.SetPosition(Player._instance.GetPlayerPosition());
        GameManager._instance.enabled = true;

        // ChallengeManager._instance.enabled = true;
        DatabaseManager._instance.enabled = true;
        HotkeysReader._instance.enabled = true;
        ChallengeManagerReyal.instance.enabled = true;
    }

    private void Start()
    {
        Debug.Log($"LoadGameSetup: {Player._instance.GetCurrentMap()}");
        _sceneLoader.SetSceneToLoad(Player._instance.GetCurrentMap());
    }

    private void OnDestroy()
    {
        PlayerStatUpdater._instance.enabled = true;
        IngameOptions._instance.enabled = true;
    }
}
