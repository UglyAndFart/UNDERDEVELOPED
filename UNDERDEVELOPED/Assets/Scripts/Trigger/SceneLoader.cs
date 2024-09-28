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
    private bool _useTimer;
    [SerializeField]
    private string _sceneToLoad;

    private void Start()
    {
        if (_useTimer)
        {
            StartCoroutine(StartTimer());       
        }
    }

    public static void LoadNextScene(string sceneToLoad)
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

    public void SetSceneToLoad(string scene)
    {
        _sceneToLoad = scene;
    } 
}
