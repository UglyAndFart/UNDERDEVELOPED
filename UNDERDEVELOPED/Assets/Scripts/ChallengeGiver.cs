using System.Collections;
using System.Collections.Generic;
using Unity.CodeEditor;
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

        QuestTriggerRange.OnPlayerEnter += GiveChallenge;
    }

    public void GiveChallenge()
    {
        Debug.Log("Challenge Given!");
        _challenge.SetChallengeValues(_challengeName, _challengeArea, _challengeLevel);
    }

    private void OnDestroy()
    {
        QuestTriggerRange.OnPlayerEnter -= GiveChallenge;
    }
}
