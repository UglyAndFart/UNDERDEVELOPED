using System;
using System.IO;
using UnityEngine;

public class SaveSystemManager : MonoBehaviour
{   
    public static SaveSystemManager _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }

        _instance = this;
    }

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
