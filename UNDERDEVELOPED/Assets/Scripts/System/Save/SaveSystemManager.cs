using System;
using System.IO;
using UnityEngine;

public class SaveSystemManager : MonoBehaviour
{   
    // public static SaveSystemManager Instance;

    // private void Awake()
    // {
    //     if (Instance == null)
    //     {
    //         Instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    public static void SavePlayer(Player player)
    {
        SaveSystem.SavePlayer(player);
    }

    public static PlayerData LoadPlayer()
    {
        string saveFilePath = DirectoryManager.GetCurrentSaveFolder();
        Debug.Log("From SaveSystemMnager: " + saveFilePath);
        PlayerData playerData = SaveSystem.LoadPlayer(saveFilePath);
        return playerData;
    }
}
