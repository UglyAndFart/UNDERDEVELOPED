using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameEnvironment : MonoBehaviour
{
    public static GameEnvironment _instance;
    [SerializeField]
    private bool[] _southForestCutscenes, _sf2;
    private List<List<bool>> _bossesState, _chestsState, _potsState, _cutscenesState;

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
        _cutscenesState = new List<List<bool>>(4);
        _cutscenesState.Add(_southForestCutscenes.ToList());
        // _cutscenesState = new List<List<bool>>(4);
        // _cutscenesState[0] = _southForestCutscene.ToList();
        // _cutscenesState[1] = _sf2.ToList();
    }

    public void SetGameEnvironment()
    {
        GameData gameData = SaveSystemManager.LoadGame();

        _bossesState = gameData._bossesState;
        _chestsState = gameData._chestsState;
        _potsState =gameData._potsState;
        _cutscenesState = gameData._cutscenesState;
    }

    public void SetEnvironmentData()
    {
        GameEnvironmentData environmentData = new GameEnvironmentData(ConvertToArray(_bossesState),
            ConvertToArray(_chestsState), ConvertToArray(_potsState), ConvertToArray(_cutscenesState));
    }

    public List<List<bool>> GetBossesStates()
    {
        return _bossesState;
    }

    public void SetBossesStates(List<List<bool>> newBossesState)
    {
        _bossesState = newBossesState;
    }

    public List<List<bool>> GetChestsStates()
    {
        return _chestsState;
    }

    public void SetChestsStates(List<List<bool>> newChestsState)
    {
        _chestsState = newChestsState;
    }

    public List<List<bool>> GetPotsStates()
    {
        return _potsState;
    }

    public void SetPotsStates(List<List<bool>> newPotsState)
    {
        _potsState = newPotsState;
    }

    public List<List<bool>> GetCutsceneStates()
    {
        return _cutscenesState;
    }

    public void SetCutsceneStates(List<List<bool>> newCutsceneState)
    {
        _cutscenesState = newCutsceneState;
    }

    private bool[][] ConvertToArray(List<List<bool>> list)
    {
        if (list == null)
            return null;

        bool[][] jaggedArray = new bool[list.Count][];

        for (int i = 0; i < list.Count; i++)
        {
            jaggedArray[i] = list[i].ToArray();
        }

        return jaggedArray;
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}