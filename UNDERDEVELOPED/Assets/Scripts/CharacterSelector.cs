using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    private Player _player;
    private CharacterPrefabLoader _characterPrefabLoader; 

    private void Awake()
    {
        _player = PlayerGameObjectFinder.FindPlayerScript();
        _characterPrefabLoader = GameObject.Find("Prefab Handler").GetComponent<CharacterPrefabLoader>();
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
        _characterPrefabLoader.EnableCharacter();
        SceneLoader.LoadNextScene("Realm"); // temp
    }
}
