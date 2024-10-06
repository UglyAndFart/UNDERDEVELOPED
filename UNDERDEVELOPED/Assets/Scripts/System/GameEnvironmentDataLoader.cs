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
    [SerializeField]
    private  TimelineEndChecker _timelineEndChecker;
    private int _cutsceneNum = 0, area = 0;
    private void Awake()
    {
        _gameEnvironmentManager = GameEnvironmentManager._instance;
    }

    private void Start()
    {
        OnSceneLoad();
        EnableCutscene();
    }

    private void Update()
    {
        CutsceneEnd();
    }

    private void EnableCutscene()
    {
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
        if (!_timelineEndChecker.GetTimelineOver())
        {
            return;
        }

        if (_cutsceneNum == 0)
        {
            _gameEnvironmentManager.CompletedCutscene("cutscene", area, _cutsceneNum);
            _cutsceneNum++;
            
            if (_cutsceneNum < _cutscenes.Length)
            {
                _timelineEndChecker.SetCutscene(_cutscenes[_cutsceneNum]);
            }
        }
    }

    private void OnSceneLoad()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "South Forest")
        {
            area = 0;
        }
        else if (scene.name == "SF2")
        {
            area = 1;
        }
        else if (scene.name == "Outside town")
        {
            area = 2;
        }
    }

    private void OnDestroy()
    {

    }
}
