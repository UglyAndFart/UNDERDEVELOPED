using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnvironmentManager : MonoBehaviour
{
    private GameEnvironment _gameEnvironment;
    private bool[] _bossesState, _chestsStates, _potsStates;

    private void Start()
    {
        _gameEnvironment = GetComponent<GameEnvironment>();
    }
    
}
