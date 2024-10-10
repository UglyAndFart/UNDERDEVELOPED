using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject tutorialPanel;
    public Button closeButton;
    public TMP_Text tutorialText;
    public Button[] categoryButtons;
    public GameObject scrollViewContent;
    public GameObject toggleButtonPrefab;
    public TMP_Text detailsText;
    public GameObject detailsPanel;
    public Image detailsImage;
    public Button nextButton;
    public Button backButton;
    
    [Header("Search Bar")]
    public TMP_InputField searchBar;  // The search input field

    [Header("Tutorial Data")]
    public TutorialCategory[] tutorialCategories;

    private Toggle currentToggle;
    private int additionalInfoIndex = 0;
    private TutorialTopic currentTopic;
    private string currentSearchQuery = "";  // Holds the current search query
    private TutorialCategory currentCategory; // Holds the currently selected category

    void Start()
    {
        tutorialPanel.SetActive(false);

        foreach (Button categoryButton in categoryButtons)
        {
            categoryButton.onClick.AddListener(() => OnCategorySelected(categoryButton));
        }

        closeButton.onClick.AddListener(HUDManager._instance.CloseTutorial);
        nextButton.onClick.AddListener(ShowNextAdditionalInfo);
        backButton.onClick.AddListener(ShowPreviousAdditionalInfo);

        // Listener for search bar input
        searchBar.onValueChanged.AddListener(OnSearchInputChanged);

        nextButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
    }

    public void OpenTutorialPanel()
    {
        tutorialPanel.SetActive(true);
        OnCategorySelected(categoryButtons[0]);
    }

    public void CloseTutorialPanel()
    {
        tutorialPanel.SetActive(false);
    }

    void OnCategorySelected(Button selectedCategoryButton)
    {
        string categoryName = selectedCategoryButton.name;
        currentCategory = GetCategoryByName(categoryName);

        if (currentCategory != null)
        {
            tutorialText.text = currentCategory.categoryName;
            UpdateScrollView();
        }
    }

    // Update the scroll view based on the current category and search query
    void UpdateScrollView()
    {
        // Clear the scroll view content first
        foreach (Transform child in scrollViewContent.transform)
        {
            Destroy(child.gameObject);
        }

        if (currentCategory == null)
        {
            return;
        }

        // Populate scroll view with filtered topics
        Toggle firstToggle = null;
        foreach (var topic in currentCategory.topics)
        {
            if (IsTopicMatchingSearch(topic))  // Filter topics based on search input
            {
                GameObject toggleButtonObj = Instantiate(toggleButtonPrefab, scrollViewContent.transform);
                Toggle toggle = toggleButtonObj.GetComponent<Toggle>();
                TMP_Text toggleText = toggleButtonObj.GetComponentInChildren<TMP_Text>();
                toggleText.text = topic.topicName;

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

                if (firstToggle == null)
                {
                    firstToggle = toggle;
                }
            }
        }

        // Automatically select the first matching topic if available
        if (firstToggle != null)
        {
            firstToggle.isOn = true;
        }
    }

    // Function to check if the topic matches the search input
    bool IsTopicMatchingSearch(TutorialTopic topic)
    {
        if (string.IsNullOrEmpty(currentSearchQuery))
        {
            return true; // If no search query, show all topics
        }

        return topic.topicName.ToLower().Contains(currentSearchQuery.ToLower());
    }

    // Listener for search input changes
    void OnSearchInputChanged(string newSearchQuery)
    {
        currentSearchQuery = newSearchQuery;  // Update the current search query
        UpdateScrollView();  // Re-filter topics based on the current search query
    }

    void UpdateDetailsPanel(TutorialTopic selectedTopic)
    {
        currentTopic = selectedTopic;
        additionalInfoIndex = 0;
        UpdateAdditionalInfo();
    }

    void UpdateAdditionalInfo()
    {
        if (additionalInfoIndex == 0)
        {
            detailsText.text = currentTopic.details;
            detailsImage.sprite = currentTopic.image;
            detailsImage.gameObject.SetActive(currentTopic.image != null);
        }
        else
        {
            if (additionalInfoIndex - 1 < currentTopic.additionalDetails.Length)
            {
                detailsText.text = currentTopic.additionalDetails[additionalInfoIndex - 1];
            }

            if (additionalInfoIndex - 1 < currentTopic.additionalImages.Length)
            {
                detailsImage.sprite = currentTopic.additionalImages[additionalInfoIndex - 1];
                detailsImage.gameObject.SetActive(true);
            }
            else
            {
                detailsImage.gameObject.SetActive(false);
            }
        }

        nextButton.gameObject.SetActive(additionalInfoIndex < currentTopic.additionalDetails.Length);
        backButton.gameObject.SetActive(additionalInfoIndex > 0);
    }

    void ShowNextAdditionalInfo()
    {
        if (additionalInfoIndex < currentTopic.additionalDetails.Length)
        {
            additionalInfoIndex++;
            UpdateAdditionalInfo();
        }
    }

    void ShowPreviousAdditionalInfo()
    {
        if (additionalInfoIndex > 0)
        {
            additionalInfoIndex--;
            UpdateAdditionalInfo();
        }
    }

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
    public string categoryName;
    public TutorialTopic[] topics;
}

[System.Serializable]
public class TutorialTopic
{
    public string topicName;
    public string details;
    public Sprite image;
    public string[] additionalDetails;
    public Sprite[] additionalImages;
}
