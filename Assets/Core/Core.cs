using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public static class Core
{
    public static LexAnalyzer lexAnalyzer;
    public static EngineCoreDataTransfer dataTransfer;
    public static ConsoleModule consoleModule;
    public static GameObject consoleText;
    public static TMPro.TMP_Text logText;
    public static bool isSyntaxCorrect;

    public static void GetConsole()
    {
        consoleModule = GameObject.Find("Console").GetComponent<ConsoleModule>();
        Debug.Log("CORE: Console initialized");
        consoleText = GameObject.Find("ConsoleText");
        logText = consoleText.GetComponent<TMPro.TMP_Text>();
    }

    public static void ShowLog(string log)
    {

    }

    public static void InitCore()
    {
        Debug.Log("CORE: Core initialized");
        Debug.Log($"CORE: Data path: {CoreDirictories.defaultOpenFilePath}");
        dataTransfer = new EngineCoreDataTransfer();
    }

    public static void SetSyntaxAnalyzeResult(string code)
    {
        lexAnalyzer = new LexAnalyzer(dataTransfer.GetCodeList(code));
        if(lexAnalyzer.code != null)
        {
            Debug.Log("CORE: Code list initialized");
            isSyntaxCorrect = lexAnalyzer.Analyze();
            Debug.Log($"CORE: Syntax analyze result: {isSyntaxCorrect}");
        }      
        else
        {
            Debug.LogWarning("CORE: Code list is null");
        }
    }

    public static void InitDefaultCodeArea()
    {

    }
}
