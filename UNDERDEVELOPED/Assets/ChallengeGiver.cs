using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeGiver : MonoBehaviour
{
    [SerializeField]
    private Challenge _challenge;
    [SerializeField]
    private string _challengeName, _challengeArea, _challengeLevel;
    
    public void GiveChallenge()
    {
        Debug.Log("Challenge Given!");
        _challenge.SetChallengeName(_challengeName);
        _challenge.SetChallengeArea(_challengeArea);
        _challenge.SetChallengeLevel(_challengeLevel);
    }
}
