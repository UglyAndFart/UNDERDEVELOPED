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
    private string[] _savedFiles;
    private PlayerData[] _savedGameInfos;

    private void Awake()
    {
        DisableContinue();
        DisableAllSlots();
    }

    private void Start()
    {
        LoadPlayerGame();
    }

    private void LoadPlayerGame()
    {
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $"My Games/Underdeveloped/Saves");
        string currentSaveFilePath = "";
        _savedFiles = Directory.GetFiles(path, "*.plyr");
        //string[][] savedFileInfos;

        if (_savedFiles.Length <= 0)
        {
            return;
        }

        _savedGameInfos = new PlayerData[_savedFiles.Length];

        for (int i = 0; i < _savedFiles.Length; i++)
        {
            Debug.Log("path: " + _savedFiles[i]);
            currentSaveFilePath = _savedFiles[i];
            SaveSystemManager.SetFilePath(currentSaveFilePath);
            Debug.Log("Warp to: " + currentSaveFilePath);
            _savedGameInfos[i] = SaveSystemManager.LoadPlayer();
         
            _saveSlots[i].SetActive(true);
            _saveSlots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _savedGameInfos[i]._name;
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

    public void LoadSelectedGameFile(int slotIndex)
    {
        SaveSystemManager.SetFilePath(_savedFiles[slotIndex]);
        SceneLoader.LoadNextScene(_savedGameInfos[slotIndex]._map);
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
