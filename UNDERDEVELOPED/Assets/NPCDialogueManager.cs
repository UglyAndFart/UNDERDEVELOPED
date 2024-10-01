using UnityEngine;
using UnityEngine.Playables;

public class NPCDialogueManager : MonoBehaviour
{
    public PlayableDirector arrivalCutscene; // First cutscene (teleport)
    public PlayableDirector conversationCutscene; // Second cutscene (mid-dialogue)
    public PlayableDirector exitCutscene; // Final cutscene

    [SerializeField]
    private NewGameFileManager _newGameFileManager;

    private bool hasTalked = false; // Track if first dialogue has been shown
    private Dialogue dialogue; // Reference to the Dialogue script
    
    void Start()
    {
        dialogue = FindObjectOfType<Dialogue>(); // Reference to the Dialogue script
        StartArrivalCutscene(); // Start with the first cutscene
    }

    void StartArrivalCutscene()
    {
        arrivalCutscene.Play(); // Play the first cutscene
        arrivalCutscene.stopped += OnArrivalCutsceneStopped; // Trigger after cutscene stops
    }

    private void OnArrivalCutsceneStopped(PlayableDirector director)
    {
        // After the first cutscene ends, trigger the first dialogue
        dialogue.StartDialogue(); // Start the first dialogue
        arrivalCutscene.stopped -= OnArrivalCutsceneStopped; // Unsubscribe
    }

    public void OnDialogueEnd()
    {
        if (!hasTalked)
        {
            hasTalked = true; // Mark the first dialogue as done
            PlayConversationCutscene(); // Trigger the second cutscene
        }
        else
        {
            StartExitCutscene(); // Start the exit cutscene after the second dialogue
        }
    }

    void PlayConversationCutscene()
    {
        conversationCutscene.Play(); // Play the second cutscene
        conversationCutscene.stopped += OnConversationCutsceneStopped; // Trigger after cutscene stops
    }

    private void OnConversationCutsceneStopped(PlayableDirector director)
    {
        // After the second cutscene ends, trigger the second part of the dialogue
        dialogue.StartSecondDialogue(); // Trigger the second dialogue part
        conversationCutscene.stopped -= OnConversationCutsceneStopped; // Unsubscribe
    }

    void StartExitCutscene()
    {
        exitCutscene.Play(); // Play the final cutscene
        exitCutscene.stopped += OnExitCutsceneStopped; // Trigger after cutscene stops
    }

    /* Note:    the CreateNewGame call is just temp coz the ExitScene value
                is same as Arrival and Conversation it be called 3 times :>
    */
    private void OnExitCutsceneStopped(PlayableDirector director)
    {
        _newGameFileManager.CreateNewGame();
        SceneLoader.LoadNextScene("South Forest");
        exitCutscene.stopped -= OnExitCutsceneStopped; // Unsubscribe
    }
}
