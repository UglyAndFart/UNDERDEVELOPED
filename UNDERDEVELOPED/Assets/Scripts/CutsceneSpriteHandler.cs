using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneSpriteHandler : MonoBehaviour
{
    public GameObject[] _characterSprites;

    private void Start()
    {
        if (_characterSprites == null)
        {
            Debug.LogWarning("CutSceneManager: _characterSprites is Empty");
            return;
        }
        
        string currentCharacter = Player._instance.GetCharacterType();

        if (currentCharacter == "Knight")
        {
            _characterSprites[0].SetActive(true);
        }
        else if (currentCharacter == "Rogue")
        {
            _characterSprites[1].SetActive(true);
        }
        else if (currentCharacter == "Mage")
        {
            _characterSprites[3].SetActive(true);
        }
    }
}
