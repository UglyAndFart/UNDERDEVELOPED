using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SavedGameFileLoader : MonoBehaviour
{
    [SerializeField]
    private Button _continue;
    [SerializeField]
    private GameObject _overwriteAlert;
    [SerializeField]
    private GameObject[] _saveSlots;
    private List<string> _savedFiles;
    // private PlayerData[] _savedGameInfos;
    private GameData[] _savedGameInfos;
    private string _savesFolderPath;

    private void Awake()
    {
        DisableContinue();
        DisableAllSlots();
        _savesFolderPath = DirectoryManager.GetSavesFolderPath();
    }

    private void Start()
    {
        RetrieveSavedGameFile();
    }

    public void LoadAllGameFile()
    {
        RetrieveSavedGameFile();
        DisplaySavedGameFile();
    }

    //Retrieve the .plyr file inside currentSavesFilepath
    private void RetrieveSavedGameFile()
    {

        if (!Directory.Exists(_savesFolderPath))
        {
            Directory.CreateDirectory(_savesFolderPath);
            return;
        }

        _savedFiles = new List<string>();

        // int numOfSlotFolder = Directory.GetDirectories(_savesFolderPath).Length;

        for (int i = 0; i < 5; i++)
        {
            if (Directory.Exists(Path.Combine(_savesFolderPath, $"Slot {i + 1}")))
            {
                _savedFiles.Add(Directory.GetFiles(Path.Combine(_savesFolderPath, $"Slot {i + 1}"), "*.plyr")[0]);
            }
        }
    }

    //Iterate each file in string[] to display retrieved data
    private void DisplaySavedGameFile()
    {
        // _savedGameInfos = new PlayerData[_savedFiles.Count];
        _savedGameInfos = new GameData[_savedFiles.Count];

        for (int i = 0; i < _savedFiles.Count; i++)
        {
            Debug.Log("path: " + _savedFiles[i]);
            DirectoryManager.SetCurrentSaveFolder(_savedFiles[i]);
            Debug.Log("Warp to: " + _savedFiles[i]);
            // _savedGameInfos[i] = SaveSystemManager.LoadPlayer();
            _savedGameInfos[i] = SaveSystemManager.LoadGame();
         
            _saveSlots[i].SetActive(true);
            
            if (_saveSlots[i].transform.Find("Name") == null)
            {
                Debug.LogWarning("Can't find slot gameobject");
            }
            
            _saveSlots[i].transform.Find("Name").GetComponent<TextMeshProUGUI>().text = _savedGameInfos[i]._name;
        }
        
        // foreach (string file in savedFiles)
        // {

        // }
    }
    
    public bool SlotAvailable()
    {
        if (_savedFiles.Count >= 5)
        {
            return true;
        }

        return false;
    }

    private void DisableContinue()
    {
        _continue.enabled = false;
    }

    private void DisableAllSlots()
    {
        foreach (GameObject slot in _saveSlots)
        {
            slot.SetActive(false);
        }
    }
    
    //Set the slot path as currentsavefolder Then load the next scene
    public void PrepareLoadFile(int slotIndex)
    {
        if (!SlotAvailable())
        {
            _overwriteAlert.SetActive(true);
            return;
        }

        DirectoryManager.SetCurrentSaveFolder(_savedFiles[slotIndex]);
        SceneLoader.LoadScene("Retrieving Game File Data");
    }

    // public void SelectFileToOverwrite(int slotIndex)
    // {
    //     DirectoryManager.SetCurrentSaveFolder(_savedFiles[slotIndex]);
    // }

    // public void OverwriteFile()
    // {
    //     SceneLoader.LoadScene("Computer Laboratory");
    // }

    // private void LoadGame()
    // {
    //     foreach (PlayerData playerData in _savedGameInfos)

    //     if(!File.Exists(path))
    //     {
    //         return null;
    //     }

    //     BinaryFormatter formatter = new BinaryFormatter();
    //     using (FileStream fileStream = new FileStream(path, FileMode.Open))
    //     {
    //         PlayerData playerData = (PlayerData) formatter.Deserialize(fileStream);
    //         return playerData;
    //     }
        
    // }
}
