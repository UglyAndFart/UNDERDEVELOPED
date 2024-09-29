using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField]
    private GameObject _characterSelectionUI;
    
    private Player _player;

    private void Awake()
    {
        _player = Player._instance;
        CodeRunner.OnPlayerSuccess += EnableCharacterSelection;
    }

    public void SelectCharacter(int index)
    {
        if (index == 0)
        {
            _player.SetChacterType("Knight");
        }
        else if (index == 1)
        {
            _player.SetChacterType("Rogue");
        }
        else if (index == 2)
        {
            _player.SetChacterType("Mage");
        }

        Debug.Log("Selected Character Type:" + _player.GetCharacterType());
        _player.SetPlayerName("HelpME");
        //_characterPrefabLoader.EnableCharacter();
        SceneLoader.LoadNextScene("Realm"); // temp
    }

    private void EnableCharacterSelection()
    {
        _characterSelectionUI.SetActive(true);
    }

    private void OnDestroy()
    {
        CodeRunner.OnPlayerSuccess -= EnableCharacterSelection;
    }
}
