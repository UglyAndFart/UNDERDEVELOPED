using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeGiver : MonoBehaviour
{
    [SerializeField]
    private string _challengeName, _challengeArea, _challengeLevel;
    private Challenge _challenge;
    
    private void Awake()
    {
        _challenge = Challenge._instance;

        if (_challenge == null)
        {
            Debug.LogWarning("ChallengeGiver: Challenge Not Found");
        }
    }

    public void GiveChallenge()
    {
        Debug.Log("Challenge Given!");
        _challenge.SetChallengeName(_challengeName);
        _challenge.SetChallengeArea(_challengeArea);
        _challenge.SetChallengeLevel(_challengeLevel);
    }
}
