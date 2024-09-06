using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

//Retrive from DB and set to console
public class ChallengeManagerReyal : MonoBehaviour
{
    [SerializeField]
    private DatabaseManager _databaseManager;
    [SerializeField]
    private HUDManager _hudManager;
    [SerializeField]
    private Challenge _challenge;
    [SerializeField]
    private TMP_InputField _editorText;

    private string[] _currentChallengeData;
    private string _challengeName, _challengeArea, _challengeLevel, _challengeText;
    private int _playerAttemptCount;

    private void Start()
    {
        _currentChallengeData = new string[7];
        _playerAttemptCount = 0;
    }

    private void Update()
    {
        if (_hudManager._codeEditorOnScreen)
        {
            LoadChallenge();
        }
    }

    private void LoadChallenge()
    {
        FetchCurrentChallenge();
        SetCurrentChallenge();
        GetChallengeString();
        
        if (_challengeText != null)
        {
            Debug.Log("Console Shit");
            LoadChallengeToConsole();
        }
    }

    private void OnDisable()
    {
        ResetLocalVariables();
    }

    private void FetchCurrentChallenge()
    {
        _challengeName = _challenge.GetChallengeName();
        _challengeArea = _challenge.GetChallengeArea();
        _challengeLevel = _challenge.GetChallengeLevel();
    }

    private void ResetLocalVariables()
    {
        _challengeName = null;
        _challengeArea = null;
        _challengeLevel = null;
        _challengeText = null;
    }

    private void SetCurrentChallenge()
    {
        if (!string.IsNullOrEmpty(_challengeName) || !string.IsNullOrWhiteSpace(_challengeName))
        {
            _currentChallengeData = _databaseManager.GetChallengeViaName(_challengeName);
        }
        else if ((!string.IsNullOrEmpty(_challengeArea) || !string.IsNullOrWhiteSpace(_challengeArea))
            && (!string.IsNullOrEmpty(_challengeLevel) || !string.IsNullOrWhiteSpace(_challengeLevel)))
        {
            _currentChallengeData = _databaseManager.GetChallengeViaAreaAndLevel(_challengeArea, _challengeLevel);
        }
        else
        {
            //Debug.Log("No challenge active");
            _challengeName = null;
            _challengeArea = null;
            _challengeLevel = null;
            _challengeText = null;
        }
    }

    private void LoadChallengeToConsole()
    {
        _editorText.text = _challengeText;
    }

    public void GetChallengeString()
    {
        try
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, Path.Combine(_currentChallengeData[3], _currentChallengeData[0] + ".txt"));
            _challengeText = ReadChallengeTxt(filePath);
        }
        catch (Exception)
        {
            return;
        }
    }

    public string ReadChallengeTxt(string filePath)
    {
        string challengeTxt = "";

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                challengeTxt += line + "\n";
            }
        }

        return challengeTxt;
    }

    public string[] GetCurrentChallenge()
    {
        return _currentChallengeData;
    }
}
