using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    public DatabaseManager _databaseManager;
    
    private string[] _currentQuest;
    private string _playerArea, _challenge;
    private int _playerAttempts;
    
    void Start()
    {
        _playerAttempts = 0;
    }

    //fetch all valid challenges from the database and returns only one randomly 
    public string[] RetrieveChallengeViaAreaAndStatus(string area, string status)
    {
        string[][] retrievedChallenges = _databaseManager.LoadAllChallengesViaAreaAndStatus(area, status);
        int randomNum = Random.Range(0, retrievedChallenges.Length - 1);
        return retrievedChallenges[randomNum];
    }
    
    public string[] RetrieveChallengeViaName(string name)
    {
        return _databaseManager.GetChallengeViaName(name);
    }

    // public void RetrieveRandomEasyChallenge()
    // {
    //    _currentQuest = RetrieveChallengeViaAreaAndStatus(_playerArea, "Easy");
    // }

    // public void RetrieveRandomMidChallenge()
    // {
    //     _currentQuest = RetrieveChallengeViaAreaAndStatus(_playerArea, "Mid");
    // }

    // public void RetrieveRandomHardChallenge()
    // {
    //     _currentQuest = RetrieveChallengeViaAreaAndStatus(_playerArea, "Hard");
    // }

    public string[] GetCurrentQuest()
    {
        return this._currentQuest;
    }

    public string GetChallengeString(string[] currentChallenge)
    {
        //RetrieveRandomEasyChallenge(); //remove after
        string filePath = Path.Combine(Application.streamingAssetsPath, Path.Combine(currentChallenge[3], _currentQuest[0] + ".txt"));
        return _challenge = ReadChallengeTxt(filePath);
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

    public string PlayerSolveChallenge(int numOfFailedTest)
    {
        bool playerPass  = numOfFailedTest > 0? false: true;

        if (!playerPass)
        {
            return "Failed";
        }

        _databaseManager.UpdateChallengeStatus(_currentQuest[0], "Done");
        return "Passed";
    }
}
