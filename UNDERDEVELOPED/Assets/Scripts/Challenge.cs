using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : MonoBehaviour
{
    public delegate void ChallengeEventHandler();
    public static ChallengeEventHandler OnChallengeGive;
    public static Challenge _instance;
    private string _challengeName, _challengeArea, _challengeLevel;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }

    public void SetChallengeName(string name)
    {
        _challengeName = name;
    }

    public string GetChallengeName()
    {
        return _challengeName;
    }

    public void SetChallengeArea(string area)
    {
        _challengeArea = area;
    }

    public string GetChallengeArea()
    {
        return _challengeArea;
    }

    public void SetChallengeLevel(string level)
    {
        _challengeLevel = level;
    }

    public string GetChallengeLevel()
    {
        return _challengeLevel;
    }

    public void ResetChallengeValues()
    {
        _challengeName = null;
        _challengeArea = null;
        _challengeLevel = null;
    }

    public void SetChallengeValues(string name, string area, string level)
    {
        _challengeName = name;
        _challengeArea = area;
        _challengeLevel = level;

        OnChallengeGive?.Invoke();
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
