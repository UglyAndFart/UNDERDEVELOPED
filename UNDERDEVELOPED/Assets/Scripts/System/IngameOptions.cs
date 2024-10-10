using UnityEngine;
using UnityEngine.UI;

public class IngameOptions : MonoBehaviour
{
    public static IngameOptions _instance;

    [Header("Buttons")]
    [SerializeField]
    private Button _btnMenu;
    [SerializeField]
    private Button _btnCodex;
    [SerializeField]
    private Button _btnCodeEditor;
    [SerializeField]
    private Button _btnInventory;


    [Header("Ingame Options")]
    [SerializeField]
    private Button _btnContinue;
    [SerializeField]
    private Button _btnMainMenu;
    [SerializeField]
    private Button _btnExit;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }

    private void Start()
    {   
        _btnMenu.onClick.AddListener(HUDManager._instance.OpenIngameMenu);
        _btnCodex.onClick.AddListener(HUDManager._instance.OpenTutorial);
        _btnCodeEditor.onClick.AddListener(HUDManager._instance.OpenCodeEditor);
        _btnInventory.onClick.AddListener(HUDManager._instance.OpenInventory);
        _btnContinue.onClick.AddListener(BtnContinue_Click);
        _btnMainMenu.onClick.AddListener(BtnBackToMain_Click);
        _btnExit.onClick.AddListener(BtnExitGame_Click);
    }

    private void BtnContinue_Click()
    {
        HUDManager._instance.CloseIngameMenu();
    }

    public void BtnBackToMain_Click()
    {
        if (SaveSystemManager._instance.transform.parent.transform.parent.gameObject.name != "Persistent GameObject")
        {
            Debug.LogWarning("IngameOptions: Persistent GameObject Not Found");
            return;
        }

        Destroy(SaveSystemManager._instance.transform.parent.transform.parent.gameObject);
        SceneLoader.LoadScene("Main Menu");

        // Debug.Log($"Destroying {CharacterPrefabLoader._instance.GetCurrentCharacter().transform.parent.transform.parent.gameObject}");
        // Destroy(CharacterPrefabLoader._instance.GetCurrentCharacter().transform.parent.transform.parent.gameObject);
        // PlayerManager._instance.enabled = false;
        // GameManager._instance.enabled = false;
        // SceneLoader.LoadNextScene("Main Menu");
    }

    public void BtnExitGame_Click()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
        
        DirectoryManager.SetCurrentSaveFolder("");
    }
}
