using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // Reference to the UI text component
    public string[] lines; // Array of dialogue lines
    public float textSpeed; // Speed at which text is displayed

    private int index;

    void Start()
    {
        textComponent.text = string.Empty; // Clear text on start
        gameObject.SetActive(false); // Ensure dialogue UI is hidden initially
    }

    void Update()
    {
        // Check for mouse click only if dialogue is active
        if (gameObject.activeSelf && Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index]) // If the current line is fully displayed
            {
                NextLine(); // Go to the next line
            }
            else
            {
                StopAllCoroutines(); // Stop typing effect
                textComponent.text = lines[index]; // Show the current line immediately
            }
        }
    }

    public void StartDialogue()
    {
        index = 0; // Start from the first line
        gameObject.SetActive(true); // Show dialogue UI
        textComponent.text = string.Empty; // Clear previous text
        StartCoroutine(TypeLine()); // Start typing the first line
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c; // Add each character one by one
            yield return new WaitForSeconds(textSpeed); // Wait for a bit
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++; // Move to the next line
            textComponent.text = string.Empty; // Clear text for new line
            StartCoroutine(TypeLine()); // Type the new line
        }
        else
        {
            EndDialogue(); // End dialogue if all lines are shown
        }
    }

    public void EndDialogue()
    {
        gameObject.SetActive(false); // Hide dialogue box
        FindObjectOfType<NPCDialogueManager>().OnDialogueEnd(); // Notify NPC manager that dialogue has ended
    }

    public void TriggerDialogue() // Ensure this method is present
    {
        StartDialogue(); // Call the StartDialogue method
    }
}
