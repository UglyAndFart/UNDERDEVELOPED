using System.Collections;
using System.Collections.Generic;
using Unity.CodeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialQuestManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _computer1, _computer2, _computer3, _codeEditor;
    private bool _questComplete, _playerInRange;
    private int _questNumber = 1;
    void Start()
    {
        _computer1.SetActive(true);
        _computer2.SetActive(false);
        _computer3.SetActive(false);
        _playerInRange = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _playerInRange)
        {
            if (_questNumber == 1)
            {
                Computer1Complete();
            }
            else if (_questNumber == 2)
            {
                Computer2Complete();
            }
            else if (_questNumber == 3)
            {
                _questComplete = true;
                _codeEditor.SetActive(true);
            }
        }
    }

    private void Computer1Complete()
    {
        _questNumber = 2;
        _computer1.SetActive(false);
        _computer2.SetActive(true);
    }

    private void Computer2Complete()
    {
        _questNumber = 3;
        _computer2.SetActive(false);
        _computer3.SetActive(true);
    }

    public void SetPlayerInQuestRange(bool value)
    {
        _playerInRange = value;
    }
}
