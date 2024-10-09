using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnvironmentManager : MonoBehaviour
{
    public static GameEnvironmentManager _instance;
    private GameEnvironment _gameEnvironment;
    // private bool[] _bossesState, _chestsStates, _potsStates, _cutscenesStates;
    private List<List<bool>> _bossesStates, _chestsStates, _potsStates, _cutsceneStates;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }

    private void Start()
    {
        _gameEnvironment = GetComponent<GameEnvironment>();
        _cutsceneStates = _gameEnvironment.GetCutsceneStates();
        _bossesStates = _gameEnvironment.GetBossesStates();
    }
    
    public void UpdateGameEnvironment(string arrayName, int area, int index)
    {
        if (arrayName == "cutscene")
        {   
            _cutsceneStates[area][index] = true;
            _gameEnvironment.SetCutsceneStates(_cutsceneStates);
             
            // bossesStates.Add();
            
            // bossesStates[area][index] = true;
            // _gameEnvironment.SetBossesStates(bossesStates);
        }
        else if (arrayName == "boss")
        {
            _bossesStates[area][index] = true;
            _gameEnvironment.SetBossesStates(_bossesStates);
            Debug.Log("GameEnvironmentManager: No error");
        }
        // else if (arrayName == "chest")
        // {
        //     _chestsStates[index] = true;
        //     _gameEnvironment.SetBossesStates(_bossesState);
        // }
        // else if (arrayName == "pot")
        // {
        //     _potsStates[index] = true;
        //     _gameEnvironment.SetBossesStates(_bossesState);
        // }
        // else if (arrayName == "cutscene")
        // {
        //     _cutscenesStates[index] = true;
        //     _gameEnvironment.SetBossesStates(_cutscenesStates);
        // }
    }
    
    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
