using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

//Retrive from DB and set to console
public class ChallengeManagerReyal : MonoBehaviour
{
    public static ChallengeManagerReyal _instance;
    private DatabaseManager _databaseManager;
    private HUDManager _hudManager;
    private Challenge _challenge;
    [SerializeField]
    private TMP_InputField _editorText;

    private string[] _currentChallengeData;
    private string _challengeName, _challengeArea, _challengeLevel, _challengeText;
    // private int _playerAttemptCount;
    private bool _loadedChallenge = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(_instance);
            return;
        }

        _instance = this;
    }

    private void Start()
    {
        _hudManager = HUDManager._instance;
        _challenge = Challenge._instance;
        _databaseManager = DatabaseManager._instance;
        _currentChallengeData = new string[7];

        CodeRunner.OnPlayerSuccess += ChallengeSolved;
        // _playerAttemptCount = 0;
    }

    //might be the promblem
    private void Update()
    {
        if (!_hudManager._codeEditorOnScreen)
        {
            _loadedChallenge = false;
            return;
        }
        
        if (_loadedChallenge)
        {
            return;
        }

        LoadChallenge();
        _loadedChallenge = true;
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

    public void ChallengeSolved()
    {
        _hudManager.CloseCodeEditor();
        _databaseManager.UpdateChallengeStatus(_challengeName, "Done");
        _challenge.ResetChallengeValues();
    }

    private void OnDisable()
    {
        ResetLocalVariables();
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = this;
        }

        CodeRunner.OnPlayerSuccess -= ChallengeSolved;
    }
}
