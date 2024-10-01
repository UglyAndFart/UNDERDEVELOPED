using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    public static SpawnPointManager _instance; 
    public GameObject[] _spawnPoints;

    public bool _newGame = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public GameObject GetSpawnPoint(string spawnPointName)
    {
        foreach (GameObject spawnPoint in _spawnPoints)
        {
            if (spawnPoint.name == spawnPointName)
            {
                Debug.Log("SpawnPointManager: Found Spawn Point");
                return spawnPoint;
            }
        }

        Debug.Log("SpawnPoint Manager: SpawnPoint Doesnt Exists");
        return null;
    }

    // Every scene load check for spawnpoints
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Realm")
        {
            _newGame = true;
        }

        SpawnPointStorage spawnPointStorage = FindObjectOfType<SpawnPointStorage>();
        
        if (spawnPointStorage == null)
        {
            return;
        }

        _spawnPoints = spawnPointStorage.GetComponent<SpawnPointStorage>().spawnPoints;
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
