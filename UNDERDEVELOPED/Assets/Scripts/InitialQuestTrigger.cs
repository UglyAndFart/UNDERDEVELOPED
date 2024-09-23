using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InitialQuestTrigger : MonoBehaviour
{
    [SerializeField]
    private HUDManager _hudManager;
    [SerializeField]
    private GameObject _computer1, _computer2, _computer3, _tutorialOverlay, _codeEditor;
    [SerializeField]
    private TextMeshProUGUI _content1, _content2;
    [SerializeField]
    private ChallengeGiver _challengeGiver;
    private bool _playerInRange;
    private int _computerNumber = 1;
    private bool _windowOpen = false;

    private void Start()
    {
        _computer1.SetActive(true);
        _computer2.SetActive(false);
        _computer3.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _playerInRange)
        {
            if (_computerNumber == 1 && !_windowOpen)
            {
                _tutorialOverlay.SetActive(true);
                _content1.enabled = true;
                _windowOpen = true;
            }
            else if (_computerNumber == 2 && !_windowOpen)
            {
                _tutorialOverlay.SetActive(true);
                _content2.enabled = true;
                _windowOpen = true;
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
    }

    public void SetPlayerInQuestRange(bool value)
    {
        _playerInRange = value;
    }
}
