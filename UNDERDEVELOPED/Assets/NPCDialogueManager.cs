using UnityEngine;
using UnityEngine.Playables; // Required for Timeline
using UnityEngine.SceneManagement; // Required for scene management

public class NPCDialogueManager : MonoBehaviour
{
    public Dialogue dialogue; // Reference to the Dialogue script
    public PlayableDirector arrivalCutscene; // Cutscene for NPC's arrival
    public PlayableDirector conversationCutscene; // Cutscene after the first dialogue
    public PlayableDirector exitCutscene; // Cutscene for NPC exit
    private bool hasTalked = false; // Track if the player has talked to the NPC

    void Start()
    {
        StartArrivalCutscene(); // Start with the arrival cutscene
    }

    void StartArrivalCutscene()
    {
        arrivalCutscene.Play(); // Play arrival cutscene
        // Subscribe to the cutscene end event
        arrivalCutscene.stopped += OnArrivalCutsceneStopped; 
    }

    private void OnArrivalCutsceneStopped(PlayableDirector director)
    {
        StartDialogue(); // Start the dialogue when cutscene stops
        director.stopped -= OnArrivalCutsceneStopped; // Unsubscribe to prevent multiple calls
    }

    void StartDialogue()
    {
        dialogue.TriggerDialogue(); // Start the dialogue
    }

    public void OnDialogueEnd()
    {
        if (!hasTalked)
        {
            hasTalked = true; // Set the flag to true
            conversationCutscene.Play(); // Play conversation cutscene
            Invoke("ContinueConversation", (float)conversationCutscene.duration); // Continue conversation after cutscene
        }
        else
        {
            exitCutscene.Play(); // Play exit cutscene
            Invoke("ExitScene", (float)exitCutscene.duration); // Exit scene after cutscene
        }
    }

    void ContinueConversation()
    {
        dialogue.TriggerDialogue(); // Trigger the next part of the conversation
    }

    void ExitScene()
    {
        SceneManager.LoadScene("NextScene"); // Load the next scene after exit cutscene
    }
}
