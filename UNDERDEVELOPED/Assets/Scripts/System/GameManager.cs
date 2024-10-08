using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    private Player _player;
    [SerializeField]
    private GameObject _savingIndicator;
    private GameEnvironment _environment;
    [SerializeField]
    private int _saveInterval = 10;

    private void Awake()
    {
        if (_instance != null & _instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }

    private void Start()
    {
        _player = Player._instance;
        
        if (_player == null)
        {
            Debug.LogWarning("GameManager: Player Not Found");
        }

        _environment = GameEnvironment._instance;
        
        if (_environment == null)
        {
            Debug.LogWarning("GameManager: GameEnvironment Not Found");
        }

        SceneManager.sceneLoaded += OnSceneLoad;
        StartCoroutine(GameDataAutoSave());
    }

    // private IEnumerator PlayerDataAutoSave()
    // {
    //     while(true)
    //     {
    //         SaveSystemManager.SavePlayer(_player);
    //         Debug.Log("PlayerData saved");
    //         yield return new WaitForSeconds(_saveInterval);
    //     }
    // }

    private IEnumerator GameDataAutoSave()
    {
        while(true)
        {
            SaveSystemManager.SaveGame(_player, _environment);
            Debug.Log("GameData saved");
            StartCoroutine(SavingIndicator());
            yield return new WaitForSeconds(_saveInterval);
        }
    }

    private IEnumerator SavingIndicator()
    {
        _savingIndicator.SetActive(true);
        yield return new WaitForSeconds(1);
        _savingIndicator.SetActive(false);
        StopCoroutine(SavingIndicator());
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        //Check if boss is alive
        //if ()


        if (scene.name == "South Forest" )
        {
            // PlayerStatUpdater._instance.enabled = true;
            // ChallengeManager._instance.enabled = true;
            // DatabaseManager._instance.enabled = true;
            // HotkeysReader._instance.enabled = true;
            // IngameOptions._instance.enabled = true;
            // Debug.Log("GameManager: Loading " + HUDManager._instance.transform.parent.gameObject.name);
            // HUDManager._instance.transform.parent.gameObject.SetActive(true);
            // Inventory._instance.transform.parent.gameObject.SetActive(true);

            // SaveSystemManager.SavePlayer(_player);
            SaveSystemManager.SaveGame(_player, _environment);
        }
    }

    private void OnDestroy()
    {
        if (_player != null && (!string.IsNullOrEmpty(DirectoryManager.GetCurrentSaveFolder())
            || !string.IsNullOrWhiteSpace(DirectoryManager.GetCurrentSaveFolder())))
        {
            Debug.Log("GameManager: saved onDestroy");
            // SaveSystemManager.SavePlayer(_player);
            SaveSystemManager.SaveGame(_player, _environment);      
              
        }

        if (_instance == this)
        {
            _instance = null;
        }
    }
}
