using UnityEngine;
using TMPro;
using System.IO;
using System;
using UnityEngine.UI;

public class CodeRunner : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField]
    private TMP_InputField _editorText;
    [SerializeField]
    private TextMeshProUGUI _consoleText, _instructionText;
    [SerializeField]
    private GameObject _statusPanel, _instructionPanel, _codeEditorPanel;
    
    [Header("Buttons")]
    [SerializeField]
    private Button _btnEditor;
    [SerializeField]
    private Button _btnConsole, _btnReset, _btnSubmit, _btnRun, _btnClose;

    public delegate void CodeRunnerHandler();
    public static event CodeRunnerHandler OnPlayerSuccess;
    private ChallengeManagerReyal _challengeManager;
    //add reference to another gameObject for test status 
    
    private string _exeStoragePath, _codeRunnerPath;
    private string _description;
    private bool _haveError;

    private void Awake()
    {
        SetupListenerToButtons();
    }

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        _exeStoragePath = Path.Combine(DirectoryManager.GetGameFolderPath(), "ExeFile");
        
        if (!Directory.Exists(_exeStoragePath))
        {
            MonoCommands.createDir(_exeStoragePath);
        }

        BtnEditor_Click();

        //_codeRunnerPath = Path.Combine(Application.streamingAssetsPath, "Scripts\\PlayerCodeRunner.txt");
        _challengeManager = ChallengeManagerReyal._instance;
    }

    // public void runCode()
    // {
    //     if (string.IsNullOrEmpty(editor.GetComponent<TMP_InputField>().text) ||
    //     string.IsNullOrEmpty(editor.GetComponent<TMP_InputField>().text))
    //     {
    //         return;
    //     }

    //     if (!checkEntryPoint())
    //     {
    //         addEntryPoint();    
    //     }
    //     else
    //     {
    //         txt = editor.GetComponent<TMP_InputField>().text;
    //     }

    //     MonoCommands.createCS(path, "test", txt);
    //     MonoCommands.compileCS("mcs test.cs", path);

    //     if(MonoCommands.haveCompilationError())
    //     {
    //         string errorMsg = "";
    //         foreach (string str in MonoCommands.consoleCompileError)
    //         {
    //             errorMsg += str;
    //         }
    //         console.GetComponent<TextMeshProUGUI>().text = errorMsg;
    //         return;
    //     }

    //     string output = MonoCommands.runExeFile("mono test.exe", path);

    //     if (MonoCommands.haveRuntimeError())
    //     {
    //         string errorMsg = "";
    //         foreach (string str in MonoCommands.consoleRuntimeError)
    //         {
    //             errorMsg += str;
    //         }
    //         console.GetComponent<TextMeshProUGUI>().text = errorMsg;
    //         return;
    //     }
    //     Debug.Log("Reached the ass");
    //     console.GetComponent<TextMeshProUGUI>().text = output;
    // }

    private string RunCode(string code, string fileName)
    {
        _haveError = true;

        if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(code))
        {
            _haveError = false;
            return "";
        }

        // if (!checkEntryPoint())
        // {
        //     addEntryPoint();    
        // }
        // else
        // {
        //     txt = editor.GetComponent<TMP_InputField>().text;
        // }

        MonoCommands.createCS(_exeStoragePath, fileName, code);
        MonoCommands.compileCS($"mcs {fileName}.cs", _exeStoragePath);

        if(MonoCommands.haveCompilationError())
        {
            _haveError = false;
            string errorMsg = "";

            foreach (string str in MonoCommands.consoleCompileError)
            {
                errorMsg += str;
            }
            return errorMsg;
        }

        string output = MonoCommands.runExeFile($"mono {fileName}.exe", _exeStoragePath);

        if (MonoCommands.haveRuntimeError())
        {
            _haveError = false;
            string errorMsg = "";

            foreach (string str in MonoCommands.consoleRuntimeError)
            {
                errorMsg += str;
            }
            return errorMsg;
        }

        Debug.Log("CodeRunner: PlayerCode Executed!");

        BtnConsole_Click();
        return output;
    }

    //Insert player code inside a Main method
    // public void addEntryPoint()
    // {
    //     _txt = "using System;\n\n" + 
    //     "public class ClassA\n" +
    //     "{\n" +
    //     "public static void Main(string[] args)\n" +
    //     "{\n" +
    //     $"{_editorPanel.GetComponent<TMP_InputField>().text}\n" +
    //     "}\n}";
    // }

    // Compare playercode string if main method exists 
    public bool checkEntryPoint()
    {
        if (_editorText.text.Contains("public static void Main(string[] args)") ||
        _editorText.text.Contains("public static void Main(string[] args){") ||
        _editorText.text.Contains("static void Main(string[] args){") ||
        _editorText.text.Contains("static void Main(string[] args)"))
        {
            return true;
        }
        return false;
    }

    // public void setEditorCode(string code)
    // {
    //     _editorPanel.GetComponent<TMP_InputField>().text = code; 
    // }

    /// <summary>
    /// Run the player code.
    /// Insert PlayerCode inside a Runner class.
    /// output is Set to Console UI text.
    /// </summary>
    public void RunPlayerCode()
    {
        //getPlayerCode from editor
        //Add class and entrypoint
        //add function call inside Main()
        //add PlayerFunction
        
        string functionName = _challengeManager.GetCurrentChallenge()[0];
        string fileName = "PlayerCode";
        string code = "using System;\n\n" +
        "public class PlayerCode\n" +
        "{\n" +
        "\tpublic static void Main(string[] args)" +
        "\t{\n" +
        "\t\tPlayerCode playerCode = new PlayerCode();\n" +
        $"\t\tplayerCode.{functionName}();\n" +
        "\t}\n\n" +
        $"\t{_editorText.text}" +
        "}\n";
        string output = "";
        // using (StreamReader reader = new StreamReader(codeRunnerPath))
        // {
        //     string line;

        //     while ((line = reader.ReadLine()) != null)
        //     {
        //         if(line.Contains("//function Call"))
        //         code += line + "\n";
        //     }
        // }
          
        output = RunCode(code, fileName);
        _consoleText.text = output;
    }

    /// <summary> 
    ///Run the Testcase retrieved from localdb.
    ///Invoke OnPlayerSuccess when PlayerCode passes all the Testcase.
    /// </summary>
    public void RunPlayerCodeTest()
    {
        string fileName = "PlayerCodeTest";
        string testTxtPath = Path.Combine(Application.streamingAssetsPath, _challengeManager.GetCurrentChallenge()[6]);
        string functionName = _challengeManager.GetCurrentChallenge()[0];
        string code = "";
        string output = "";

        using (StreamReader reader = new StreamReader(testTxtPath))
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains("//function call"))
                {
                    code += line.Replace("//function call", $"playerCodeTest.{_challengeManager.GetCurrentChallenge()[0]}();");
                    continue;
                }

                if (line.Contains("//player function"))
                {
                    code += _editorText.text;
                    continue;
                }
                code += line + "\n";
            }
        }
        
        output = RunCode(code, fileName);
        _statusPanel.GetComponent<TextMeshProUGUI>().text = output;

        if (!_haveError)
        {
            Debug.Log("CodeRunner: Player code is wrong");
            return;
        }

        Debug.Log("CodeRunner: Player Answered correctly");
        OnPlayerSuccess?.Invoke();
        //questManager.PlayerSolveChallenge();
    }

    private void SetupListenerToButtons()
    {
        _btnEditor.onClick.AddListener(BtnEditor_Click);
        _btnConsole.onClick.AddListener(BtnConsole_Click);
        _btnSubmit.onClick.AddListener(BtnSubmit_Click);
        _btnReset.onClick.AddListener(BtnReset_Click);
        _btnRun.onClick.AddListener(BtnRun_Click);
        _btnClose.onClick.AddListener(Btn_Close);
    }

    private void BtnEditor_Click()
    {
        _editorText.gameObject.SetActive(true);
        _consoleText.gameObject.SetActive(false);
    }

    private void BtnConsole_Click()
    {
        _editorText.gameObject.SetActive(false);
        _consoleText.gameObject.SetActive(true);
    }

    private void BtnSubmit_Click()
    {
        RunPlayerCodeTest();
    }

    private void BtnRun_Click()
    {
        RunPlayerCode();
    }

    private void BtnReset_Click()
    {
        _challengeManager.ResetChallengeText();
    }

    private void Btn_Close()
    {
        _codeEditorPanel.SetActive(false);
    }
}
