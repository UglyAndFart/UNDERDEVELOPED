using UnityEngine;
using TMPro;
using System.IO;
using System;
using System.Text;
using UnityEditor.ShaderGraph.Internal;

public class CodeRunner : MonoBehaviour
{
    public GameObject _editor, _console, _status;
    private ChallengeManagerReyal _challengeManager;
    //add reference to another gameObject for test status 

    private string _storagePath, _codeRunnerPath;
    private string _txt;
    void Start()
    {
        _storagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games/Underdeveloped/ExeFile");
        MonoCommands.createDir(_storagePath);
        btnEditor_Click();

        _codeRunnerPath = Path.Combine(Application.streamingAssetsPath, "Scripts/PlayerCodeRunner.txt");
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
        if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(code))
        {
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

        MonoCommands.createCS(_storagePath, fileName, code);
        MonoCommands.compileCS($"mcs {fileName}.cs", _storagePath);

        if(MonoCommands.haveCompilationError())
        {
            string errorMsg = "";
            foreach (string str in MonoCommands.consoleCompileError)
            {
                errorMsg += str;
            }
            return errorMsg;
        }

        string output = MonoCommands.runExeFile($"mono {fileName}.exe", _storagePath);

        if (MonoCommands.haveRuntimeError())
        {
            string errorMsg = "";
            foreach (string str in MonoCommands.consoleRuntimeError)
            {
                errorMsg += str;
            }
            return errorMsg;
        }
        Debug.Log("Reached the ass");
        return output;
    }

    public void btnEditor_Click()
    {
        _editor.SetActive(true);
        _console.SetActive(false);
    }

    public void btnConsole_Click()
    {
        _editor.SetActive(false);
        _console.SetActive(true);
    }

    public void addEntryPoint()
    {
        _txt = "using System;\n\n" + 
        "public class ClassA\n" +
        "{\n" +
        "public static void Main(string[] args)\n" +
        "{\n" +
        $"{_editor.GetComponent<TMP_InputField>().text}\n" +
        "}\n}";
    }

    public bool checkEntryPoint()
    {
        if (_editor.GetComponent<TMP_InputField>().text.Contains("public static void Main(string[] args)") ||
        _editor.GetComponent<TMP_InputField>().text.Contains("public static void Main(string[] args){") ||
        _editor.GetComponent<TMP_InputField>().text.Contains("static void Main(string[] args){") ||
        _editor.GetComponent<TMP_InputField>().text.Contains("static void Main(string[] args)"))
        {
            return true;
        }
        return false;
    }

    public void setEditorCode(string code)
    {
        _editor.GetComponent<TMP_InputField>().text = code; 
    }

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
        $"\t{_editor.GetComponent<TMP_InputField>().text}" +
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
        _console.GetComponent<TextMeshProUGUI>().text = output;
    }

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
                    code += _editor.GetComponent<TMP_InputField>().text;
                    continue;
                }
                code += line + "\n";
            }
        }
        
        output = RunCode(code, fileName);
        _status.GetComponent<TextMeshProUGUI>().text = output;

        //questManager.PlayerSolveChallenge();
    }
}
