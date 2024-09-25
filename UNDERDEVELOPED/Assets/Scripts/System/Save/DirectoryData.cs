using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class DirectoryData : MonoBehaviour
{
    public string _gameFolderPath;

    public DirectoryData (string gameFolderPath)
    {
        _gameFolderPath = gameFolderPath;
    }
}
