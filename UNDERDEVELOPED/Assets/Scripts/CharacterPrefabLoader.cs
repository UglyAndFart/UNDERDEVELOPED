using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPrefabLoader : MonoBehaviour
{
    public static CharacterPrefabLoader _instance;
    private GameObject _currentCharacter;

    [SerializeField]
    private GameObject[] _characters;
    private Player _player;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }

        _instance = this;

        _player = Player._instance;
    }

    // public void LoadPlayerPrefab()
    // {
    //     Resources.Load(_player.GetCharacterType());
    // }

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
            Debug.LogWarning("CharacterType Not Found");
            return;
        }

        SetPrefabComponents(index);
    }

    public void EnableCharacterViaIndex(int index)
    {
        SetPrefabComponents(index);
    }

    private void SetPrefabComponents(int index)
    {
        //Debug.Log("Enable Player Manager and GameEnvironment");
        //PlayerManager._instance.enabled = true;
        //_player.transform.parent.Find("Environment").GetComponent<GameEnvironmentManager>().enabled = true;
        //GameEnvironment._instance.enabled = true;
        _currentCharacter = _characters[index];
        _currentCharacter.SetActive(true);
    }

    public void EnableCurrentCharacter()
    {
        _currentCharacter.SetActive(true);
    }

    public GameObject GetCurrentCharacter()
    {
        return _currentCharacter;
    }

    // public void FindLoadPlayerPrefab(string prefabName)
    // {
    //     GameObject prefab = Resources.Load<GameObject>(prefabName);
    //     Instantiate(prefab, new Vector3 (0, 0, 0), Quaternion.identity);
    // }
}
