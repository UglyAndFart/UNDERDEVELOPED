using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTriggerRange : MonoBehaviour
{
    public delegate void QuestTriggerEventHandler(); 
    public static event QuestTriggerEventHandler OnPlayerEnter;
    public static event QuestTriggerEventHandler OnPlayerExit;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") || collider.CompareTag("Default Player"))
        {
            OnPlayerEnter?.Invoke();
            Debug.Log("QuestTriggerRange: Player in range");
        }

        Debug.Log("QuestTriggerRange: Something is inside");
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            OnPlayerExit?.Invoke();
            Debug.LogWarning("QuestTriggerRange: Player left");
        }
    }
}
