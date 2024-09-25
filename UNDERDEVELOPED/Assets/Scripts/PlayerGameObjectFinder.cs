using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameObjectFinder : MonoBehaviour
{
    public static Player FindPlayerScript()
    {
        Player[] players = FindObjectsOfType<Player>();
        
        if (players.Length > 1)
        {
            Debug.LogWarning("more than one GameObject with player component is found");
            return null;
        }
        else if (players.Length == 1)
        {
            Debug.Log("Found one");
            return players[0];
        }

        Debug.LogError("No active player GameObject");

        return null;
    }

    public static PlayerManager FindPlayerManagerScript()
    {
        PlayerManager[] playerManger = GameObject.FindObjectsOfType<PlayerManager>();
        
        if (playerManger.Length > 1)
        {
            return null;
        }
        else if (playerManger.Length == 1)
        {
            return playerManger[0];
        }
        return null;
    }

    public static GameObject FindPlayerPrefab()
    {
        GameObject[] gameObjects = FindObjectsOfType<GameObject>(false);

        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.CompareTag("Player"))
            {
                return gameObject;
            }

            // if (gameObject.name == characterType)
            // {
            //     return gameObject;
            // }
        }

        Debug.LogError("No Player Prefab");
        return null;
    }
}
