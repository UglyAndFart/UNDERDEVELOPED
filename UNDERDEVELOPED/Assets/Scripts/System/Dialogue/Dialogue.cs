using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public GameObject dialogueBox; // Reference to the DialogueBox GameObject
    public TextMeshProUGUI dialogueText; // Reference to the DialogueText TMP component
    public TextMeshProUGUI nameText; // Reference to the NameText TMP component

    public string[] firstDialogueLines; // Array of dialogue lines for the first part
    public string[] firstNpcNames; // Array of NPC names for the first part
    public string[] secondDialogueLines; // Array of dialogue lines for the second part
    public string[] secondNpcNames; // Array of NPC names for the second part

    private string[] currentLines; // Current dialogue lines
    private string[] currentNpcNames; // Current NPC names
    private int index; // Current dialogue line index
    public float textSpeed = 0.05f; // Speed at which text is displayed

    void Start()
    {
        dialogueText.text = string.Empty; // Clear the dialogue text
        nameText.text = string.Empty; // Clear the name text
        dialogueBox.SetActive(false); // Hide dialogue box initially
    }

    void Update()
    {
        // Check for mouse click only if dialogue is active
        if (dialogueBox.activeSelf && Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == currentLines[index]) // If the current line is fully displayed
            {
                NextLine(); // Go to the next line
            }
            else
            {
                StopAllCoroutines(); // Stop typing effect
                dialogueText.text = currentLines[index]; // Show the current line immediately
            }
        }
    }

    // Start the first dialogue
    public void StartDialogue()
    {
        currentLines = firstDialogueLines; // Set the first dialogue lines
        currentNpcNames = firstNpcNames; // Set the first NPC names
        index = 0; // Start from the first line
        dialogueBox.SetActive(true); // Show the dialogue box
        StartCoroutine(TypeLine()); // Start typing the first line
    }

    // Start the second dialogue (after the second cutscene)
    public void StartSecondDialogue()
    {
        currentLines = secondDialogueLines; // Set the second dialogue lines
        currentNpcNames = secondNpcNames; // Set the second NPC names
        index = 0; // Reset to the first line of the second dialogue
        dialogueBox.SetActive(true); // Show the dialogue box again
        StartCoroutine(TypeLine()); // Start typing the first line of the second dialogue
    }

    IEnumerator TypeLine()
    {
        nameText.text = currentNpcNames[index]; // Display the current NPC's name
        dialogueText.text = string.Empty; // Clear previous dialogue text

        foreach (char c in currentLines[index].ToCharArray())
        {
            dialogueText.text += c; // Add each character one by one
            yield return new WaitForSeconds(textSpeed); // Wait between characters
        }
    }

    void NextLine()
    {
        if (index < currentLines.Length - 1)
        {
            index++; // Move to the next dialogue line
            StartCoroutine(TypeLine()); // Start typing the new line
        }
        else
        {
            EndDialogue(); // End the dialogue if all lines are done
        }
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false); // Hide the dialogue box
        FindObjectOfType<NPCDialogueManager>().OnDialogueEnd(); // Notify the NPCDialogueManager that dialogue is done
    }
}
