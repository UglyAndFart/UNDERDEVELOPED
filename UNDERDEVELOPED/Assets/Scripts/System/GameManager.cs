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
        SaveSystemManager.LoadPlayer();
        StartCoroutine(PlayerDataAutoSave());
    }

    private void Update()
    {
        
    }

    IEnumerator PlayerDataAutoSave()
    {
        while(true)
        {
            SaveSystemManager.SavePlayer(_player);
            yield return new WaitForSeconds(_saveInterval);
        }
    }
}
