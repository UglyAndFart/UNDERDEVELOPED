using System;
using System.IO;
using UnityEngine;

public class SaveSystemManager : MonoBehaviour
{   
    public static SaveSystemManager Instance;
    private static string _savedFilePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void SavePlayer(Player player)
    {
        SaveSystem.SavePlayer(player);
    }

    public static PlayerData LoadPlayer()
    {
        Debug.Log("From SaveSystemMnager: " + _savedFilePath);
        PlayerData playerData = SaveSystem.LoadPlayer(_savedFilePath);
        return playerData;
    }

    public static void SetFilePath(string path)
    {
        _savedFilePath = path;
    }
}
