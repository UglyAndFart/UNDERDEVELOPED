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
    private GameObject[] _saveSlots;
    private List<string> _savedFiles;
    private PlayerData[] _savedGameInfos;
    private string _savesFolderPath;
    public static SavedGameFileLoader _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.Log("SavedGameFileLoader gotrekt");
            Destroy(this);
            return;
        }

        _instance = this;
        
        DisableContinue();
        DisableAllSlots();
        _savesFolderPath = DirectoryManager.GetSavesFolderPath();
    }

    private void Start()
    {
        RetrieveSavedGameFile();
    }

    //Retrieve the .plyr file inside currentSavesFilepath
    private void RetrieveSavedGameFile()
    {
        if (!Directory.Exists(_savesFolderPath))
        {
            Debug.Log("No saves folder");
            Directory.CreateDirectory(_savesFolderPath);
            Debug.Log($"Created Directory: {_savesFolderPath}");
            return;
        }

        int numOfSlot = Directory.GetDirectories(_savesFolderPath).Length;

        _savedFiles = new List<string>();

        for (int i = 0; i < numOfSlot; i++)
        {
            if (Directory.Exists(Path.Combine(_savesFolderPath, $"Slot {i + 1}")))
            {
                _savedFiles.Add(Directory.GetFiles(Path.Combine(_savesFolderPath, $"Slot {i + 1}"), "*.plyr")[0]);
                Debug.Log("Added game file to save Slots");
            }
        }
    }

    //Iterate each file in string[] to display retrieved data
    public void DisplaySavedGameFile()
    {
        string currentSaveFilePath = "";
        _savedGameInfos = new PlayerData[_savedFiles.Count];

        for (int i = 0; i < _savedFiles.Count; i++)
        {
            Debug.Log("path: " + _savedFiles[i]);
            currentSaveFilePath = _savedFiles[i];
            DirectoryManager.SetCurrentSaveFolder(currentSaveFilePath);
            Debug.Log("Warp to: " + currentSaveFilePath);
            _savedGameInfos[i] = SaveSystemManager.LoadPlayer();
         
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
    public void LoadSelectedGameFile(int slotIndex)
    {
        DirectoryManager.SetCurrentSaveFolder(_savedFiles[slotIndex]);
        SceneLoader.LoadNextScene("Retrieving Game File Data");
    }

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
