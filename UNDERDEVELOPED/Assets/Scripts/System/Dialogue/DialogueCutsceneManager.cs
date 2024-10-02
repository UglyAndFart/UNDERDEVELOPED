using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Playables;

public class DialogueCutsceneManager : MonoBehaviour
{
    public enum StepType { Dialogue, Cutscene }

    [System.Serializable]
    public class Step
    {
        public StepType stepType;
        public string[] dialogueLines;
        public string[] npcNames;
        public Sprite[] npcImages;
        public PlayableDirector cutscene;
    }

    public Step[] steps; // Array of steps (cutscenes and dialogues)

    public GameObject dialogueBox; // Reference to the DialogueBox GameObject
    public TextMeshProUGUI dialogueText; // Reference to the DialogueText TMP component
    public TextMeshProUGUI nameText; // Reference to the NameText TMP component
    public Image npcImage; // Reference to the Image component for the NPC

    private int currentStepIndex = 0; // Index of the current step
    private int currentLineIndex = 0; // Index for the current line in dialogue

    public float textSpeed = 0.05f; // Speed of typing effect

    private Coroutine typingCoroutine; // Reference to the active typing coroutine

    public Toggle autoToggle; // Toggle for automatic dialogue advancement
    public GameObject skipConfirmationPanel; // Reference to the skip confirmation panel
    public Button yesButton; // Reference to the Yes button in the confirmation panel
    public Button noButton; // Reference to the No button in the confirmation panel
    public Button skipButton; // Button for skipping dialogues

    private bool isSkipping; // Flag to check if skipping is active

    void Start()
    {
        dialogueBox.SetActive(false); // Hide the dialogue box initially
        skipConfirmationPanel.SetActive(false); // Ensure skip confirmation is hidden

        // Set up button listeners
        yesButton.onClick.AddListener(SkipDialogue);
        noButton.onClick.AddListener(CloseConfirmation);
        skipButton.onClick.AddListener(ShowSkipConfirmation);

        StartSequence(); // Start the sequence immediately
    }

    public void StartSequence()
    {
        currentStepIndex = 0; // Start with the first step
        ProcessCurrentStep(); // Process the first step
    }

    private void ProcessCurrentStep()
    {
        if (currentStepIndex >= steps.Length) // Check if all steps are completed
        {
            EndSequence(); // End the sequence
            return;
        }

        Step currentStep = steps[currentStepIndex]; // Get the current step

        if (currentStep.stepType == StepType.Dialogue) // If it's a dialogue step
        {
            StartDialogue(currentStep); // Start the dialogue
        }
        else if (currentStep.stepType == StepType.Cutscene) // If it's a cutscene step
        {
            StartCutscene(currentStep); // Start the cutscene
        }
    }

    private void StartDialogue(Step step)
    {
        currentLineIndex = 0; // Reset line index for this dialogue step
        dialogueBox.SetActive(true); // Show the dialogue box
        UpdateNpcImage(step); // Update NPC image based on the current step
        typingCoroutine = StartCoroutine(TypeLine(step)); // Start typing the first line
    }

    private IEnumerator TypeLine(Step step)
    {
        nameText.text = step.npcNames[currentLineIndex]; // Update the name text
        dialogueText.text = ""; // Clear previous dialogue text

        foreach (char c in step.dialogueLines[currentLineIndex])
        {
            dialogueText.text += c; // Type each character
            yield return new WaitForSeconds(textSpeed); // Wait between characters
        }

        // Wait for player input to proceed or auto toggle to continue
        while (!Input.GetMouseButtonDown(0) && !isSkipping)
        {
            if (autoToggle.isOn) // Check if auto mode is enabled
            {
                yield return new WaitForSeconds(1f); // Wait before moving to the next line
                NextLine(step); // Move to the next line
                yield break; // Exit the coroutine
            }
            yield return null; // Wait until mouse click or skipping
        }

        if (!isSkipping)
        {
            NextLine(step); // Move to the next line
        }
    }

    private void NextLine(Step step)
    {
        if (currentLineIndex < step.dialogueLines.Length - 1)
        {
            currentLineIndex++; // Move to the next line
            UpdateNpcImage(step); // Update the NPC image
            typingCoroutine = StartCoroutine(TypeLine(step)); // Start typing the next line
        }
        else
        {
            EndDialogue(); // End the dialogue and move to the next step
        }
    }

    private void EndDialogue()
    {
        dialogueBox.SetActive(false); // Hide the dialogue box

        // Move to the next step regardless
        currentStepIndex++; // Increment the step index
        ProcessCurrentStep(); // Process the next step
    }

    private void StartCutscene(Step step)
    {
        dialogueBox.SetActive(false); // Hide the dialogue box during cutscenes
        step.cutscene.Play(); // Play the cutscene
        step.cutscene.stopped += OnCutsceneEnd; // Subscribe to the cutscene end event
    }

    private void OnCutsceneEnd(PlayableDirector director)
    {
        director.stopped -= OnCutsceneEnd; // Unsubscribe from the event
        currentStepIndex++; // Move to the next step
        ProcessCurrentStep(); // Process the next step
    }

    private void EndSequence()
    {
        dialogueBox.SetActive(false); // Hide the dialogue box at the end
        Debug.Log("All steps are completed."); // Log completion
    }

    private void UpdateNpcImage(Step step)
    {
        if (currentLineIndex < step.npcImages.Length)
        {
            npcImage.sprite = step.npcImages[currentLineIndex]; // Update NPC image
        }
    }

    // Show the confirmation dialog when Skip is clicked
    private void ShowSkipConfirmation()
    {
        isSkipping = true; // Set skipping flag
        skipConfirmationPanel.SetActive(true); // Show the confirmation panel
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); // Stop the typing effect immediately
        }
    }

    // Skip the dialogue when Yes is clicked
    private void SkipDialogue()
    {
        skipConfirmationPanel.SetActive(false); // Hide the confirmation panel
        isSkipping = false; // Reset skipping flag

        // End the dialogue to proceed to the next step
        EndDialogue();
    }

    // Close the confirmation dialog when No is clicked
    private void CloseConfirmation()
    {
        skipConfirmationPanel.SetActive(false); // Hide the confirmation panel
        isSkipping = false; // Reset skipping flag

        // Resume the dialogue if it's in progress
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); // Stop the typing coroutine
            typingCoroutine = StartCoroutine(TypeLine(steps[currentStepIndex])); // Resume typing the current line
        }
    }
}
