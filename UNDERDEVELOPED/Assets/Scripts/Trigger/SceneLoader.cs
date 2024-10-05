using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private float _switchDelay = 0;
    [SerializeField]
    private bool _useTimer = false, _useTimelineEnd = false;
    [SerializeField]
    private string _sceneToLoad;
    [SerializeField]
    private TimelineEndChecker _timelineEndChecker;

    private void Update()
    {
        if (_useTimer)
        {
            StartCoroutine(StartTimer());       
            _useTimer = false;
        }
        else if (_useTimelineEnd)
        {
            if (_timelineEndChecker.GetTimelineOver())
            {
                StartCoroutine(StartTimer());
                _useTimelineEnd = false;
                Debug.LogWarning("Pee pooooo");
            }
        }
    }

    public static void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(_switchDelay);
        SceneManager.LoadScene(_sceneToLoad);
        StopCoroutine(StartTimer());
        Debug.Log("Stop Timer");
    }

    public void SetSceneToLoad(string sceneToLoad)
    {
        _sceneToLoad = sceneToLoad;
    }
}
