using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothTransitionController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _objectsToBeEnabled;
    [SerializeField]
    private GameObject[] _objectsToBeDisabled;
    [SerializeField]
    private Behaviour[] _behavioursToBeEnabled;
    [SerializeField]
    private TimelineEndChecker _timelineEndChecker;
    private bool _objectsActivated = true;
    
    private void Update()
    {
        if (_timelineEndChecker.GetTimelineOver() && _objectsActivated)
        {
            EnableObjects();
            DisableObjects();
            EnableBehaviours();
            _objectsActivated = false;
        }
    }

    private void EnableObjects()
    {
        if (_objectsToBeEnabled == null)
        {
            return;
        }
        
        for (int i = 0; i < _objectsToBeEnabled.Length; i++)
        {
            _objectsToBeEnabled[i].SetActive(true);
        }
    }

    private void DisableObjects()
    {
        if (_objectsToBeDisabled == null)
        {
            return;
        }

        for (int i = 0; i < _objectsToBeDisabled.Length; i++)
        {
            _objectsToBeDisabled[i].SetActive(false);
        }
    }

    private void EnableBehaviours()
    {
        if (_behavioursToBeEnabled == null)
        {
            return;
        }

        for (int i = 0; i < _behavioursToBeEnabled.Length; i++)
        {
            _behavioursToBeEnabled[i].enabled = true;
        }
    }
}
