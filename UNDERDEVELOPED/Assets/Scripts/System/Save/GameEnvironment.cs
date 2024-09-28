using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnvironment : MonoBehaviour
{
    public static GameEnvironment _instance;
    
    [SerializeField]
    private bool[] _bossesState, _chestsState, _potsState;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }

        _instance = this;
    }

    public void SetEnvironmentData()
    {
        GameEnvironmentData environmentData = new GameEnvironmentData(_bossesState, _chestsState, _potsState);
    }

    public bool[] GetBossesStates()
    {
        return _bossesState;
    }

    public void SetBossesStates(bool[] newBossesState)
    {
        _bossesState = newBossesState;
    }

    public bool[] GetChestsStates()
    {
        return _chestsState;
    }

    public void SetChestsStates(bool[] newChestsState)
    {
        _chestsState = newChestsState;
    }

    public bool[] GetPotsStates()
    {
        return _potsState;
    }

    public void SetPotsStates(bool[] newPotsState)
    {
        _potsState = newPotsState;
    }
}