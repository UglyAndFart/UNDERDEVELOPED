using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager _instance;

    [SerializeField]
<<<<<<< Updated upstream:UNDERDEVELOPED/Assets/Scripts/System/GameManager.cs
    Player player;
    void Start()
    {
        SaveSystemManager.LoadPlayer();
=======
    private Player _player;
    [SerializeField]
    private int _saveInterval = 10;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;

        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void Start()
    {
>>>>>>> Stashed changes:UNDERDEVELOPED/Assets/Scripts/System/GameSaveManager.cs
        StartCoroutine(PlayerDataAutoSave());
        _player.SetPlayerData();
    }

    void Update()
    {
        
    }

    IEnumerator PlayerDataAutoSave()
    {
        while(true)
        {
            SaveSystemManager.SavePlayer(player);
            yield return new WaitForSeconds(10);
        }
    }
<<<<<<< Updated upstream:UNDERDEVELOPED/Assets/Scripts/System/GameManager.cs
=======

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        //Check if boss is alive
        
        if (scene.name == "South Forest")
        {
            Debug.Log("OnSceneLoad Save");
            SaveSystemManager.SavePlayer(_player);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("OnSceneLoad save");
        SaveSystemManager.SavePlayer(_player);
    }
>>>>>>> Stashed changes:UNDERDEVELOPED/Assets/Scripts/System/GameSaveManager.cs
}
