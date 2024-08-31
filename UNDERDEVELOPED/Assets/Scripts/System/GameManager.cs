using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Player player;
    void Start()
    {
        SaveSystemManager.LoadPlayer();
        StartCoroutine(PlayerDataAutoSave());
    }

    void Update()
    {
        
    }

    IEnumerator PlayerDataAutoSave()
    {
        while(true)
        {
            SaveSystemManager.SavePlayer(player);
            yield return new WaitForSeconds(10);
        }
    }
}
