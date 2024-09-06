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
    private TimelineEndChecker _timelineEndChecker;

    private void Update()
    {
        if (_timelineEndChecker.GetTimelineOver())
        {
            EnableObjects();
            DisableObjects();
        }
    }

    private void EnableObjects()
    {
        foreach (GameObject gameObject in _objectsToBeEnabled)
        {
            gameObject.SetActive(true);
        }
    }

    private void DisableObjects()
    {
        foreach (GameObject gameObject in _objectsToBeDisabled)
        {
            gameObject.SetActive(false);
        }
    }
}
