using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InitialQuestTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject _computer1, _computer2, _computer3, _tutorialOverlay, _characterSelection;
    [SerializeField]
    private Image _content1, _content2;
    [SerializeField]
    private ChallengeGiver _challengeGiver;
    [SerializeField]
    private GameObject _skipButton;
    private HUDManager _hudManager;
    private bool _playerInRange;
    private int _computerNumber = 1;
    private bool _windowOpen = false;

    private void Awake()
    {
        _hudManager = HUDManager._instance;;
    }

    private void Start()
    {
        _computer1.SetActive(true);
        _computer2.SetActive(false);
        _computer3.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && _playerInRange)
        {
            if (_computerNumber == 1 && !_windowOpen)
            {
                _tutorialOverlay.SetActive(true);
                _content2.enabled = false;
                _content1.enabled = true;
                _windowOpen = true;
            }
            else if (_computerNumber == 2 && !_windowOpen)
            {
                _tutorialOverlay.SetActive(true);
                _content2.enabled = true;
                _windowOpen = true;
                _skipButton.SetActive(true);
            }
            else if (_computerNumber == 3 && !_windowOpen)
            {
                _challengeGiver.GiveChallenge();
                _hudManager.OpenCodeEditor();
            }
        }
    }

    public void ComputerComplete()
    {
        if (_computerNumber == 1)
        {
            _computerNumber = 2;
            _content1.enabled = false;
            _computer1.SetActive(false);
            _computer2.SetActive(true);
            _tutorialOverlay.SetActive(false);
            _windowOpen = false;
        }
        else if (_computerNumber == 2)
        {
            _computerNumber = 3;
            _content2.enabled = false;
            _computer2.SetActive(false);
            _computer3.SetActive(true);
            _tutorialOverlay.SetActive(false);
            _windowOpen = false;
        }
    }

    public void Computer2Complete()
    {
        _computerNumber = 3;
        _content2.enabled = false;
        _computer2.SetActive(false);
        _computer3.SetActive(true);
        _tutorialOverlay.SetActive(false);
        // _skipButton.SetActive(true);
    }

    public void QuestComplete()
    {
        _hudManager.CloseCodeEditor();
        _characterSelection.SetActive(true);
    }

    public void SetPlayerInQuestRange(bool value)
    {
        _playerInRange = value;
    }
}
