using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public UnityEngine.UI.Image npcImage;

    public string[] firstDialogueLines;
    public string[] firstNpcNames;
    public Sprite[] firstNpcImages;
    public string[] secondDialogueLines;
    public string[] secondNpcNames;
    public Sprite[] secondNpcImages;

    public GameObject skipConfirmationPanel; // Reference to the skip confirmation panel
    public Button yesButton; // Reference to the Yes button in the confirmation
    public Button noButton; // Reference to the No button in the confirmation

    private string[] currentLines;
    private string[] currentNpcNames;
    private Sprite[] currentNpcImages;
    private int index;
    public float textSpeed = 0.05f;
    private bool isPaused = false; // To keep track of whether the dialogue is paused

    public Toggle autoToggle; // Toggle for auto mode
    public Button skipButton; // Button for skipping

    private Coroutine typingCoroutine; // Reference to the active typing coroutine

    void Start()
    {
        dialogueText.text = string.Empty;
        nameText.text = string.Empty;
        dialogueBox.SetActive(false);
        npcImage.gameObject.SetActive(false);
        skipConfirmationPanel.SetActive(false); // Ensure pop-up is initially hidden

        // Hook up the Yes and No button listeners
        yesButton.onClick.AddListener(SkipDialogue);
        noButton.onClick.AddListener(CloseConfirmation);

        // Hook up the skip button listener
        skipButton.onClick.AddListener(ShowSkipConfirmation);
    }

    void Update()
    {
        if (isPaused) return; // Do nothing if the dialogue is paused

        if (dialogueBox.activeSelf && Input.GetMouseButtonDown(0) && !autoToggle.isOn)
        {
            if (dialogueText.text == currentLines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = currentLines[index];
            }
        }
    }

    // Start the first dialogue
    public void StartDialogue()
    {
        currentLines = firstDialogueLines;
        currentNpcNames = firstNpcNames;
        currentNpcImages = firstNpcImages;
        index = 0;
        dialogueBox.SetActive(true);
        npcImage.gameObject.SetActive(true);
        UpdateNpcImage();
        typingCoroutine = StartCoroutine(TypeLine());
    }

    // Start the second dialogue
    public void StartSecondDialogue()
    {
        currentLines = secondDialogueLines;
        currentNpcNames = secondNpcNames;
        currentNpcImages = secondNpcImages;
        index = 0;
        dialogueBox.SetActive(true);
        npcImage.gameObject.SetActive(true);
        UpdateNpcImage();
        typingCoroutine = StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        nameText.text = currentNpcNames[index];
        dialogueText.text = string.Empty;
        UpdateNpcImage();

        foreach (char c in currentLines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        // Automatically move to the next line if auto mode is on
        if (autoToggle.isOn && !isPaused) // Only continue if not paused
        {
            yield return new WaitForSeconds(1f); // Add a delay before moving to the next line
            NextLine();
        }
    }

    void NextLine()
    {
        if (index < currentLines.Length - 1)
        {
            index++;
            typingCoroutine = StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
        npcImage.gameObject.SetActive(false);
        FindObjectOfType<NPCDialogueManager>().OnDialogueEnd();
    }

    // Show the confirmation dialog when Skip is clicked
    public void ShowSkipConfirmation()
    {
        isPaused = true; // Pause the dialogue
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); // Stop the typing effect immediately
        }

        autoToggle.isOn = false; // Disable the auto toggle when the skip button is clicked
        autoToggle.interactable = false; // Optionally, make the toggle unclickable while the confirmation is up

        skipConfirmationPanel.SetActive(true); // Show the confirmation panel
    }

    // Skip the dialogue when Yes is clicked
    public void SkipDialogue()
    {
        skipConfirmationPanel.SetActive(false); // Hide the confirmation panel
        isPaused = false; // Unpause
        EndDialogue(); // Skip to the end of the dialogue
    }

    // Close the confirmation dialog when No is clicked
    public void CloseConfirmation()
    {
        skipConfirmationPanel.SetActive(false); // Hide the confirmation panel
        isPaused = false; // Unpause

        autoToggle.interactable = true; // Re-enable the auto toggle if necessary

        // Resume the typing effect
        if (index < currentLines.Length)
        {
            typingCoroutine = StartCoroutine(TypeLine()); // Resume the typing coroutine
        }
    }

    void UpdateNpcImage()
    {
        if (index < currentNpcImages.Length)
        {
            npcImage.sprite = currentNpcImages[index];
        }
    }
}
