using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class NewGameFileManager : MonoBehaviour
{
    private string _savesFolderPath;
    private string[] _slotPaths;

    private void Awake()
    {
        _savesFolderPath = DirectoryManager.GetSavesFolderPath();
    }
    
    //Checks if theres available spot for new game file 
    private bool CheckEmptySlot()
    {
        _slotPaths = Directory.GetDirectories(_savesFolderPath);

        if (_slotPaths.Length < 5)
        {
            return true;
        }

        return false;
    }

    //Create new folder name slot n
    //limited to 5 folder
    //if exists it will prompt to pick a slot to be overwritten
    public void CreateNewGame()
    {
        if (!CheckEmptySlot())
        {
            //prompt to choose slot to be overwriten
            Debug.Log("No empty slot available");
            return;
        }
        
        CreateNewFolder();
        CreateNewGameFile();
        SceneLoader.LoadNextScene("South Forest");
    }

    private void CreateNewFolder()
    {
        for (int i = 1; i <= 5; i++)
        {
            if (_slotPaths.Length == 0)
            {
                Debug.Log("No folder found");
                Directory.CreateDirectory(Path.Combine(_savesFolderPath, "Slot 1"));
                DirectoryManager.SetCurrentSaveFolder(Path.Combine(_savesFolderPath, "Slot 1"));
                return;
            }
            else if (!_slotPaths.Contains(Path.Combine(_savesFolderPath, $"Slot {i}")))
            {
                Directory.CreateDirectory(Path.Combine(_savesFolderPath, $"Slot {i}"));
                DirectoryManager.SetCurrentSaveFolder(Path.Combine(_savesFolderPath, $"Slot {i}"));
                return;
            }
            // else if (_slotPaths[--i] == Path.Combine(_savesFolderPath, $"Slot {i}"))
            // {
            //     Directory.CreateDirectory(Path.Combine(_savesFolderPath, $"Slot {i}"));
            //     return;
            // }
        }
    }

    private void CreateNewGameFile()
    {
        string playerName = GameObject.Find("Player").GetComponent<Player>().GetName();
        string path = Path.Combine(DirectoryManager.GetCurrentSaveFolder(), $"{playerName}.plyr");
        DirectoryManager.SetCurrentSaveFolder(path);
        SaveSystemManager.SavePlayer(PlayerGameObjectFinder.FindPlayerScript());
    }

    // private void OnDestroy()
    // {
    //     Debug.Log("NewGameFileManager OnDestroy Trigger");
    //     CreateNewGameFile();    
    // }
}
