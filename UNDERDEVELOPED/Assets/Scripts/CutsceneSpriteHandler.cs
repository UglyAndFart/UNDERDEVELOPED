using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneSpriteHandler : MonoBehaviour
{
    [SerializeField]
    private DialogueCutsceneManager _cutsceneManager;
    public GameObject[] _characterSprites;

    private void Awake()
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
            _cutsceneManager.SetNPCImage(_characterSprites[0].GetComponent<SpriteRenderer>().sprite);
        }
        else if (currentCharacter == "Rogue")
        {
            _characterSprites[1].SetActive(true);
            _cutsceneManager.SetNPCImage(_characterSprites[1].GetComponent<SpriteRenderer>().sprite);
        }
        else if (currentCharacter == "Mage")
        {
            _characterSprites[2].SetActive(true);
            _cutsceneManager.SetNPCImage(_characterSprites[2].GetComponent<SpriteRenderer>().sprite);
        }
    }
}
