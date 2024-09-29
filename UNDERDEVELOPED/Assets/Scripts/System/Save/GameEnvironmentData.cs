[System.Serializable]
public class GameEnvironmentData
{
    public bool[] _bossesState, _chestsState, _potsState;
    
    public GameEnvironmentData(bool[] bossesState, bool[] chestsState, bool[] potsState)
    {
        _bossesState = bossesState;
        _chestsState = chestsState;
        _potsState = potsState;
    } 
}
