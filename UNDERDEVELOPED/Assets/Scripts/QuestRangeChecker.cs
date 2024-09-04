using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestRangeChecker : MonoBehaviour
{
    [SerializeField]
    private TutorialQuestManager _tutorialQuestManager;
    public void OnTriggerEnter2D(Collider2D collider)
    {
        _tutorialQuestManager.SetPlayerInQuestRange(true);
        Debug.Log("Inrange");
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        _tutorialQuestManager.SetPlayerInQuestRange(false);
    }
}
