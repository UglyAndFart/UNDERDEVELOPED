using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameEnvironmentDataLoader : MonoBehaviour
{
    private GameEnvironmentManager _gameEnvironmentManager;
    [SerializeField]
    private PlayableDirector[] _cutscenes;
    [SerializeField]
    private GameObject[] _cutscenesObject;
    // [SerializeField]
    // private GameObject[] _bosses;
    [SerializeField]
    private GameObject[] _bossObjects;
    [SerializeField]
    private  TimelineEndChecker _timelineEndChecker;
    private int _cutsceneNum = 0, _bossNum = 0, area = 0;
    private void Awake()
    {
        _gameEnvironmentManager = GameEnvironmentManager._instance;
    }

    private void Start()
    {
        OnSceneLoad();
        EnableCutscene();
        EnableBoss();
        //HUDManager._instance.OpenTutorialButton();
        HUDManager._instance.OpenOptions();
        HUDManager._instance.CloseBossHealthbar();
    }

    private void Update()
    {
        CutsceneEnd();
        DisableBoss();
    }

    private void EnableCutscene()
    {
        if (_cutscenesObject == null)
        {
            return;
        }

        List<List<bool>> cutscenes = GameEnvironment._instance.GetCutsceneStates();
        
        for (int i = 0; i < _cutscenesObject.Length; i++)
        {
            if (cutscenes[area][i])
            {
                continue;
            }

            _cutscenesObject[i].SetActive(true);
        }
    }

    private void CutsceneEnd()
    {
        if (_timelineEndChecker == null)
        {
            return;
        }

        if (!_timelineEndChecker.GetTimelineOver())
        {
            return;
        }

        if (_cutscenes == null)
        {
            return;
        }

        if (_cutsceneNum == 0)
        {
            _gameEnvironmentManager.UpdateGameEnvironment("cutscene", area, _cutsceneNum);
            _cutsceneNum++;
            
            if (_cutsceneNum < _cutscenes.Length)
            {
                _timelineEndChecker.SetCutscene(_cutscenes[_cutsceneNum]);
            }
        }
    }

    private void EnableBoss()
    {
        if (_bossObjects == null || BossHealth._instance == null)
        {
            return;
        }

        List<List<bool>> bosses = GameEnvironment._instance.GetBossesStates();
        
        for (int i = 0; i < _bossObjects.Length; i++)
        {
            if (bosses[area][i])
            {
                continue;
            }

            // BossHealth._instance.SetBoss(_bossObjects[i].GetComponent<Enemy>());
            _bossObjects[i].SetActive(true);
        }
    }

    private void DisableBoss()
    {   
        // if (_bossObjects[_bossNum].GetComponent<Enemy>() == null)
        // {
        //     return;
        // }

        // if (_bossObjects[_bossNum].GetComponent<Enemy>().GetHealth() > 0)
        // {
        //     return;
        // }

        // if (_bossNum == 0)
        // {   
        //     _gameEnvironmentManager.UpdateGameEnvironment("boss", area, _bossNum);
        //     _bossNum++;
        //     Destroy(_bossObjects[_bossNum].GetComponent<Enemy>());
        // }
    }

    private void OnSceneLoad()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "South Forest")
        {
            area = 0;
        }
        else if (scene.name == "SF 2")
        {
            area = 1;
        }
        else if (scene.name == "Outside Town")
        {
            area = 2;
        }
        else if (scene.name == "Town")
        {
            area = 3;
        }
    }

    private void OnDestroy()
    {
        // PlayerManager._instance.SetPreviousMap(SceneManager.GetActiveScene().name);
        string currentScene = SceneManager.GetActiveScene().name;
        
        if (currentScene == "South Forest")
        {
            PlayerManager._instance.SetPreviousMap(currentScene);
        }
        else if (currentScene == "SF 2")
        {
            PlayerManager._instance.SetPreviousMap(currentScene);
        }
        else if (currentScene == "Outside Town")
        {
            PlayerManager._instance.SetPreviousMap(currentScene);
        }
        else if (currentScene == "Town")
        {
            PlayerManager._instance.SetPreviousMap(currentScene);
        }
    }
}
