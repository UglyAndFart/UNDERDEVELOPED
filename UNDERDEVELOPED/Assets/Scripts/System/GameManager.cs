using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private int _saveInterval = 10;

    private void Start()
    {
        StartCoroutine(PlayerDataAutoSave());
    }

    private void Update()
    {
        
    }

    private IEnumerator PlayerDataAutoSave()
    {
        while(true)
        {
            SaveSystemManager.SavePlayer(_player);
            Debug.Log("PlayerData saved");
            yield return new WaitForSeconds(_saveInterval);
        }
    }
}
