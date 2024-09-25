using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestRangeChecker : MonoBehaviour
{
    [SerializeField]
    private InitialQuestTrigger _initialQuestTrigger;
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Default Player"))
        {
            _initialQuestTrigger.SetPlayerInQuestRange(true);
            Debug.Log("Inrange");
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Default Player"))
        {
            _initialQuestTrigger.SetPlayerInQuestRange(false);
        }
    }
}
