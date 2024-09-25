using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DirectoryManager : MonoBehaviour
{
    //public static DirectoryManager Instance;
    private static string _gameFolderPath; 
    private static string _savesFolderPath;
    private static string _currentSaveFolder;

    private void Awake()
    {
        // if (Instance == null)
        // {
        //     Instance = this;
        //     DontDestroyOnLoad(gameObject);
        // }
        // else
        // {
        //     Destroy(gameObject);
        //     return;
        // }

        LoadGameFolderPath();
        // _gameFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games\\Underdevelop");
    }

    private void Start()
    {
        if(!Directory.Exists(_gameFolderPath))
        {
            Directory.CreateDirectory(_gameFolderPath);
        }

        _savesFolderPath = Path.Combine(_gameFolderPath, "Saves");
    }

    // private void CreateOrReadLocalDirectoryManager()
    // {
    //     string fileName = "GameDIR.gae";
    //     BinaryFormatter formatter = new BinaryFormatter();
    //     string path = Path.Combine(_gameFolderPath, fileName);
        
    //     if (!Directory.Exists(LoadGameFolderPath()))
    //     {
    //         using (FileStream fileStream = new FileStream(path, FileMode.Create))
    //         {            
    //             DirectoryData directoryData = new DirectoryData(_gameFolderPath);
    //             formatter.Serialize(fileStream, directoryData);
    //         }

    //         return;
    //     }

    //     using (FileStream fileStream = new FileStream(path, FileMode.Open))
    //     {
    //         DirectoryData directoryData = (DirectoryData) formatter.Deserialize(fileStream);
    //         _gameFolderPath = directoryData._gameFolderPath;
    //         return;
    //     }
    // }

    private void SaveGameFolderPath(string gameFolderPath)
    {
        PlayerPrefs.SetString("LocalDirManager", gameFolderPath);
        //PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    private void LoadGameFolderPath()
    {
        string defaultGameFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games\\Underdeveloped");
        _gameFolderPath = PlayerPrefs.GetString("LocalDirManager", defaultGameFolder);
    }

    //saves the current gameFolderPath to playerprefs before exiting
    private void OnApplicationQuit()
    {
        SaveGameFolderPath(_gameFolderPath);
    }

    public static void SetSavesFolderPath(string newSavesFolderPath)
    {
        _savesFolderPath = newSavesFolderPath;
    }

    public static string GetSavesFolderPath()
    {
        return _savesFolderPath;
    }

    public static void SetCurrentSaveFolder(string newCurrentSaveFolder)
    {
        _currentSaveFolder = newCurrentSaveFolder;
    }

    public static string GetCurrentSaveFolder()
    {
        return _currentSaveFolder;
    }

    public static void SetGameFolderPath(string newGameFolderPath)
    {
        _gameFolderPath = newGameFolderPath;
    }

    public static string GetGameFolderPath()
    {
        return _gameFolderPath;
    }
}