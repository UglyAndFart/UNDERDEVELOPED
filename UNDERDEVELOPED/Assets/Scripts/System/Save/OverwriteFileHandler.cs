using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class OverwriteFileHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _confirmationBox, _overwriteWindow;   
    [SerializeField]
    private GameObject[] _slots;
    private List<string> _savedFiles;
    private GameData[] _savedGameInfos;
    private string _savesFolderPath;

    private void Awake()
    {
        DisableAllSlots();
        _savesFolderPath = DirectoryManager.GetSavesFolderPath();
    }

    private void Start()
    {
        RetrieveGameFiles();
    }

    public void RetrieveGameFiles()
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

        _savedGameInfos = new GameData[_savedFiles.Count];

        for (int i = 0; i < _savedFiles.Count; i++)
        {
            Debug.Log("path: " + _savedFiles[i]);
            DirectoryManager.SetCurrentSaveFolder(_savedFiles[i]);
            Debug.Log("Warp to: " + _savedFiles[i]);
            // _savedGameInfos[i] = SaveSystemManager.LoadPlayer();
            _savedGameInfos[i] = SaveSystemManager.LoadGame();
         
            _slots[i].SetActive(true);
            
            if (_slots[i].transform.Find("Name") == null)
            {
                Debug.LogWarning("Can't find slot gameobject");
            }
            
            _slots[i].transform.Find("Name").GetComponent<TextMeshProUGUI>().text = _savedGameInfos[i]._name;
        }
    }

    public void SelectFileToOverwrite(int slotIndex)
    {
        if (_slots[slotIndex].transform.Find("Name") == null)
        {
            Debug.LogWarning("Can't find slot gameobject");
        }
        
        _confirmationBox.SetActive(true);
        _confirmationBox.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = _savedGameInfos[slotIndex]._name;
        _overwriteWindow.SetActive(false);
        DirectoryManager.SetCurrentSaveFolder(_savedFiles[slotIndex]);
    }

    public void OverwriteFile()
    {
        DirectoryManager.DeleteSaveFolder();
        SceneLoader.LoadScene("Computer Laboratory");
    }

    public void OpenOverwriteWindow()
    {
        _overwriteWindow.SetActive(true);
    }

    private void DisableAllSlots()
    {
        if (_slots == null)
        {
            return;
        }
        
        foreach (GameObject slot in _slots)
        {
            slot.SetActive(false);
        }
    }
}
