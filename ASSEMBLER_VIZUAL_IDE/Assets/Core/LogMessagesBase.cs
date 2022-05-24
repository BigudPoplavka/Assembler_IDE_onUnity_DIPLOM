using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LogMessagesBase
{
    public static string lexAnalyzerStarted = "LEX_ANALYZER: Analyze started...";
    public static string lexAnalyzerFindEmptyComTemplate = "LEX_ANALYZER: Code has only empty .COM program template!";
    public static string lexAnalyzerIncorrectComTemplate = "LEX_ANALYZER: Error!!! Incorrect .COM program template!";
    public static string lexAnalyzerMissedComTemplatePart = "LEX_ANALYZER: Error!!! Missed part of .COM program template: ";
    public static string lexAnalyzerMissedSegName = "LEX_ANALYZER: Error!!! Missed segment name: %s in string %s ";
    public static string lexAnalyzerUndefIdentificator = "LEX_ANALYZER: Error!!! Undefined identificator: ";
    public static string lexAnalyzerCodeCorrect = "LEX_ANALYZER: Code syntax is correct!";
    public static string lexAnalyzerVarAlrdExist = "LEX_ANALYZER: Error!!! Variable with name %s already exist!";
    public static string valueRangeSizeErr = "Error!!! Value %s not in range of %s values: %s, %s";
    public static string coreInitialized = "CORE: Core initialized";
    public static string coreCodeIsEmpty = "CORE: Code is empty";
    public static string consoleInitialized = "CORE: Console initialized";
    public static string buildingStarted = "CORE: Building started... First step: calling Analyzer...";
    public static string buildingNotStarted = "CORE: Can't build while code has syntax errors!";
    public static string buildingNotStartedE = "CORE: Can't build while code has logical errors!";
 
    public static string LogMessageErrRange(string param_1, string param_2, Register currReg)
    {
        return $"Error!!! Value {param_2} not in range of {param_1} values: {currReg.GetRange(currReg.Size).Min}, {currReg.GetRange(currReg.Size).Max}";
    }

    public static string LogMessageErrMark(string param_1)
    {
        return $"ERROR!!! Mark {param_1} not founded!";
    }
}

