using System;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    public static void SavePlayer (Player player)
    {
        string playerName = player.GetPlayerName();
        BinaryFormatter formatter = new BinaryFormatter();
        string path = DirectoryManager.GetCurrentSaveFolder();
        
        using (FileStream fileStream = new FileStream(path, FileMode.Create))
        {            
            PlayerData playerData = new PlayerData(player);
            formatter.Serialize(fileStream, playerData);
        }
    }

    //will return null when theres no file inside the saves folder
    public static PlayerData LoadPlayer(string saveFilePath)
    {
        string path = saveFilePath;
        
        if(!File.Exists(path))
        {
            Debug.Log("Save path doesnt exists");
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream fileStream = new FileStream(path, FileMode.Open))
        {
            PlayerData playerData = (PlayerData) formatter.Deserialize(fileStream);
            return playerData;
        }
    }

    public static void SaveGame (Player player, GameEnvironment environment)
    {
        string playerName = player.GetPlayerName();
        BinaryFormatter formatter = new BinaryFormatter();
        string path = DirectoryManager.GetCurrentSaveFolder();
        
        using (FileStream fileStream = new FileStream(path, FileMode.Create))
        {            
            GameData gameData = new GameData(player, environment);
            formatter.Serialize(fileStream, gameData);
        }
    }

    //will return null when theres no file inside the saves folder
    public static GameData LoadGame(string saveFilePath)
    {
        string path = saveFilePath;
        
        if(!File.Exists(path))
        {
            Debug.Log("Save path doesnt exists");
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream fileStream = new FileStream(path, FileMode.Open))
        {
            GameData gameData = (GameData) formatter.Deserialize(fileStream);
            return gameData;
        }
    }
}
