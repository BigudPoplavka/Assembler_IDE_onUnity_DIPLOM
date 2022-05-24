using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[System.Serializable]
public class OnEmulationAvailable: UnityEvent<bool> { }

[System.Serializable]
public static class Core
{
    public static LexAnalyzer lexAnalyzer;
    public static EngineCoreDataTransfer dataTransfer;
    public static Buider buider;
    public static ConsoleModule consoleModule;
    public static Settings settings;
    public static Emulator emulator;

    private static Error _canNotBuild = new Error(ErrorType.CanNotBuild, LogMessagesBase.buildingNotStarted);
    private static Error _canNotBuildE = new Error(ErrorType.CanNotBuild, LogMessagesBase.buildingNotStartedE);

    public static GameObject consoleText;
    public static Button runButton;
    public static Toggles toggles;
    public static StatusBarRenderer statusBarRenderer;
    public static TMPro.TMP_InputField console;
    public static ScrollView scrollView;

    public static bool programType;

    public static List<string> emptyCode = new List<string>();

    public static OnEmulationAvailable onEmulationAvailable;

    public static void GetStateBars()
    {
        statusBarRenderer = GameObject.Find("BottomBar").GetComponent<StatusBarRenderer>();
    }

    public static void GetConsole()
    {
        consoleModule = GameObject.Find("Console").GetComponent<ConsoleModule>();
        consoleText = GameObject.Find("ConsoleText");
    }

    public static void ClearConsole()
    {
        //
    }

    public static void ShowLog(string log)
    {  
        consoleModule.Render(log);
    }

    public static void ShowLog(Error error)
    {
        consoleModule.RenderErr(error.Description);
    }

    public static void CheckSettingsPresetFile()
    {
        if (!File.Exists(CoreDirictories.settings))
        {
            File.Create(CoreDirictories.settings).Dispose();
            settings.SetDefaultSettings();
            settings.WriteSettingsToFile();
        }
        else
        {
            settings.LoadSettings();
        }
        toggles = GameObject.Find("SettingsPanel").GetComponent<Toggles>();
        settings.Toggles = toggles.GetToggles();
        settings.ChangeTogglesValues();
        toggles.gameObject.SetActive(false);
    }

    public static void InitCore()
    {
        dataTransfer = new EngineCoreDataTransfer();
        lexAnalyzer = new LexAnalyzer();
        buider = new Buider();
        settings = new Settings();      

        onEmulationAvailable = new OnEmulationAvailable();

        CheckSettingsPresetFile();

        GlobalVariables._isCodeAnalyzed = false;
    }

    public static void ResetSyntaxAnalyzerState()
    {
        lexAnalyzer.ResetLexAnalyzer();
    }
    public static void SetSyntaxAnalyzeResult(string code)
    {
        lexAnalyzer.code = dataTransfer.GetCodeList(code);
        if(lexAnalyzer.code.Count != 0)
        {
            GlobalVariables._isSyntaxCorrect = lexAnalyzer.Analyze();
            Debug.Log($"CORE: Syntax analyze result: {GlobalVariables._isSyntaxCorrect}");          
        }      
        else
        {
            ShowLog(LogMessagesBase.coreCodeIsEmpty);
        }
    }  
    public static void BuildAnalyzedCode()  
    {
        if (GlobalVariables._isSyntaxCorrect)
        {
            lexAnalyzer.ErrorChecker.ClearCurrOperandsDict();
            ShowLog(LogMessagesBase.buildingStarted);
            (bool, OperationTypeTag, Dictionary<string, OperandType>) currInstrData;

            foreach (string[] line in lexAnalyzer.analyzedCodeLines)
            {
                currInstrData = lexAnalyzer.ErrorChecker.IsCorrectOperation(line);

                Debug.Log($"CORE: BUILD: IsCorrectOperation {currInstrData.Item1}, OperationTypeTag {currInstrData.Item2}");
                if (currInstrData.Item1 == true)
                {
                    buider.AddInstruction(new Instruction(line.ToList(), currInstrData.Item2, currInstrData.Item3));
                }
                else
                {
                    ShowLog(_canNotBuildE);
                }
            }

            if(lexAnalyzer.ErrorChecker.ErrorsCount == 0)
            {
                buider.AddVarAddrToInstructionsObjectCodes();
                GlobalVariables._isEmulationAvailable = true;

                
            }
        }
        else
        {
            ShowLog(_canNotBuild);
        }
    }
}
