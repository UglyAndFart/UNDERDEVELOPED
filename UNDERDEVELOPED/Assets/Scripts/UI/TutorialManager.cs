using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject tutorialPanel;          // Main Tutorial Panel
    public Button closeButton;                // Button to close the panel
    public TMP_Text tutorialText;             // Text at the top that changes with category
    public Button[] categoryButtons;          // Buttons for categories like UI, Combat, etc.
    public GameObject scrollViewContent;      // Content inside the Scroll View for toggle buttons
    public GameObject toggleButtonPrefab;     // Prefab for the toggle buttons
    public TMP_Text detailsText;              // Text in the details panel
    public GameObject detailsPanel;           // Panel that shows the details

    [Header("Tutorial Data")]
    public TutorialCategory[] tutorialCategories;  // Array to hold all tutorial categories and topics

    private Toggle currentToggle;             // Keeps track of the active toggle

    void Start()
    {
        // Initialize the tutorial panel (hide on start)
        tutorialPanel.SetActive(false);

        // Assign button listeners for each category
        foreach (Button categoryButton in categoryButtons)
        {
            categoryButton.onClick.AddListener(() => OnCategorySelected(categoryButton));
        }

        // Assign listener for the close button
        closeButton.onClick.AddListener(CloseTutorialPanel);
    }

    public void OpenTutorialPanel()
    {
        tutorialPanel.SetActive(true);
    }

    public void CloseTutorialPanel()
    {
        tutorialPanel.SetActive(false);
    }

    void OnCategorySelected(Button selectedCategoryButton)
    {
        // Clear the scroll view content first
        foreach (Transform child in scrollViewContent.transform)
        {
            Destroy(child.gameObject);
        }

        // Find the corresponding category
        string categoryName = selectedCategoryButton.name;
        TutorialCategory selectedCategory = GetCategoryByName(categoryName);
        
        if (selectedCategory == null)
        {
            Debug.LogError($"Category not found: {categoryName}");
            return; // Exit if the category is not found
        }

        // Update the tutorial text to the selected category
        tutorialText.text = selectedCategory.categoryName;

        // Populate scroll view with topics for this category
        foreach (var topic in selectedCategory.topics)
        {
            GameObject toggleButtonObj = Instantiate(toggleButtonPrefab, scrollViewContent.transform);
            Toggle toggle = toggleButtonObj.GetComponent<Toggle>();
            TMP_Text toggleText = toggleButtonObj.GetComponentInChildren<TMP_Text>();
            toggleText.text = topic.topicName;  // Update toggle text
            
            // Debugging the toggle text
            Debug.Log($"Creating toggle for topic: {toggleText.text}");

            // Add listener for the toggle button
            toggle.onValueChanged.AddListener(delegate
            {
                if (toggle.isOn)
                {
                    UpdateDetailsPanel(topic);
                    if (currentToggle != null && currentToggle != toggle)
                    {
                        currentToggle.isOn = false;
                    }
                    currentToggle = toggle;
                }
            });
        }
    }

    // Function to update details panel with selected topic
    void UpdateDetailsPanel(TutorialTopic selectedTopic)
    {
        detailsText.text = selectedTopic.details;
        detailsPanel.SetActive(true);
    }

    // Helper function to get category by name
    TutorialCategory GetCategoryByName(string categoryName)
    {
        foreach (var category in tutorialCategories)
        {
            if (category.categoryName == categoryName)
            {
                return category;
            }
        }
        return null;
    }
}

[System.Serializable]
public class TutorialCategory
{
    public string categoryName;   // Name of the category (e.g., "UI", "Combat")
    public TutorialTopic[] topics; // Array of topics in the category
}

[System.Serializable]
public class TutorialTopic
{
    public string topicName;  // Name of the topic (e.g., "Basic Attacks")
    public string details;    // Detailed description or information for the topic
}
