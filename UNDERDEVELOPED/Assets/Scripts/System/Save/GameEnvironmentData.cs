[System.Serializable]
public class GameEnvironmentData
{
    public bool[][] _bossesState, _chestsState, _potsState, _cutsceneState;

    
    public GameEnvironmentData(bool[][] bossesState, bool[][] chestsState, bool[][] potsState, bool[][] cutsceneState)
    {
        _bossesState = bossesState;
        _chestsState = chestsState;
        _potsState = potsState;
        _cutsceneState = cutsceneState;
    } 
}
