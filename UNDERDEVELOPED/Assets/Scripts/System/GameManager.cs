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
            Debug.Log("GameManager: Player Not Found");
        }

        SceneManager.sceneLoaded += OnSceneLoad;
        StartCoroutine(PlayerDataAutoSave());
    }

    private IEnumerator PlayerDataAutoSave()
    {
        while(true)
        {
            SaveSystemManager.SavePlayer(_player);
            Debug.Log("PlayerData saved");
            yield return new WaitForSeconds(_saveInterval);
        }
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

            SaveSystemManager.SavePlayer(_player);
        }
    }

    private void OnDestroy()
    {
        if (_player == null)
        {
            Debug.Log("GameManager: saved onDestroy");
            SaveSystemManager.SavePlayer(_player);        
        }

        if (_instance == this)
        {
            _instance = null;
        }
    }
}
