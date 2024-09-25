using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPrefabLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _characters;
    private Player _player;

    private void Awake()
    {
        _player = PlayerGameObjectFinder.FindPlayerScript();
    }

    public void LoadPlayerPrefab()
    {
        Resources.Load(_player.GetCharacterType());
    }

    public void EnableCharacter()
    {
        int index;

        if (_player.GetCharacterType() == "Knight")
        {
            index = 0;
        }
        else if (_player.GetCharacterType() == "Rogue")
        {
            index = 1;
        }
        else if (_player.GetCharacterType() == "Mage")
        {
            index = 2;
        }
        else
        {
            return;
        }

        _player.GetComponent<PlayerManager>().enabled = true;
        _player.transform.parent.Find("Environment").GetComponent<GameEnvironmentManager>().enabled = true;
        _characters[index].SetActive(true);
    }

    // public void FindLoadPlayerPrefab(string prefabName)
    // {
    //     GameObject prefab = Resources.Load<GameObject>(prefabName);
    //     Instantiate(prefab, new Vector3 (0, 0, 0), Quaternion.identity);
    // }
}
