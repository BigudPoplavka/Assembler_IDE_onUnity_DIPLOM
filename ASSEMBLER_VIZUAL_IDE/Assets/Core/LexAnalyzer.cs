using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Text.RegularExpressions;

[System.Serializable]
public class LexAnalyzer: MonoBehaviour
{
    public string[] strLexemms;

    public List<string> code;
    public List<string> variables = new List<string>();
    public List<Variable> variablesTable = new List<Variable>();
    public List<string> analyzedCodeTable = new List<string>();
    public List<string> userCode = new List<string>();
    public List<string[]> analyzedCodeLines = new List<string[]>();
    public List<string> codeTemplateParts = new List<string>();
    public List<string> marks = new List<string>();

    public delegate bool checkLexemm(string lexemm);

    public ErrorChecker ErrorChecker = new ErrorChecker();

    private Error incorrectComTemplate = new Error(ErrorType.IncorrectComTemplate, LogMessagesBase.lexAnalyzerIncorrectComTemplate);
    private Error missedComTemplatePart = new Error(ErrorType.IncorrectComTemplate, LogMessagesBase.lexAnalyzerMissedComTemplatePart);
    private Error missedSegmentName = new Error(ErrorType.IncorrectComTemplate, LogMessagesBase.lexAnalyzerMissedSegName);
    public Error undefIdentificator = new Error(ErrorType.UndefinedIdentificator, LogMessagesBase.lexAnalyzerUndefIdentificator);
    public Error variableAlreadyExist = new Error(ErrorType.varAlreadyExist, LogMessagesBase.lexAnalyzerVarAlrdExist);

    private Dictionary<string, int> lexemmsTable = new Dictionary<string, int>();
    private Dictionary<int, checkLexemm> valuesTable = new Dictionary<int, checkLexemm>();
    private Dictionary<string, int> patternStringsPositions;

    private string endSeg = "ends";
    private string segmentName;

    private char[] legalSymbols = new char[] { '@', '\'', '"', '[', ']', ' ', '+' };

    private int userCodeLines = 0;
    private int templateCodeLines = 0;
    private int strongTemplatePatterns = 0;
    private int patternsWithNamedParts = 0;
    private int expectedPatternsWithName = 3;
    private int expectedStrongTemplatePatterns = 5;

    bool isDone = false;

    public LexAnalyzer()
    {
        InitLexemmsTables();
    }
    public LexAnalyzer(List<string> parsedCode)
    {
        code = parsedCode;

        InitLexemmsTables();
        GlobalVariables._variablesCount = 0;
    }

    public void InitLexemmsTables()
    {
        lexemmsTable.Add(",", 1);
        lexemmsTable.Add("db,dw,dd,dq,dt", 2);
        lexemmsTable.Add("add,sub,idiv,imul,div,mul,inc,dec", 3);
        lexemmsTable.Add("mov,push,pop", 4);
        lexemmsTable.Add("cmp,equ", 5);
        lexemmsTable.Add("cbw,cdq", 6);
        lexemmsTable.Add("al,ah,bl,bh,cl,ch,dl,dh", 7);
        lexemmsTable.Add("ax,bx,cx,dx", 8);
        lexemmsTable.Add("ds,ss,es", 9);  
        lexemmsTable.Add("cs", 10);        
        lexemmsTable.Add("and,or,eor,xor,not", 11);
        lexemmsTable.Add("je,jne,jl,jnge,jle,jng,jg,jnle,jge,jnl,jb,jnae,jbe,jna,ja,jnbe,jae,jnb", 12);
        lexemmsTable.Add("loop,loope,loopz,loopne,loopnz", 13);
        lexemmsTable.Add("lods,lodsb,lodsw,lodsd", 14);
        lexemmsTable.Add("test,bound", 15);
        lexemmsTable.Add("macro,endm,ret", 16);
        lexemmsTable.Add("si,di,sp,bp,ip", 17);
        valuesTable.Add(11, new checkLexemm(isIntDigit));
        valuesTable.Add(12, new checkLexemm(isIdentificator));
    }

    public void ResetLexAnalyzer()
    {
        userCodeLines = 0;
        templateCodeLines = 0;
        strongTemplatePatterns = 0;
        patternsWithNamedParts = 0;
        isDone = false;
        variables.Clear();
        variablesTable.Clear();
        analyzedCodeTable.Clear();
        userCode.Clear();
        analyzedCodeLines.Clear();
        codeTemplateParts.Clear();
        ErrorChecker.ClearErrors();
    }

    // Проверки типов
    public bool isIntDigit(string lexemm)
    {
        int tmp = 0;
        if (int.TryParse(lexemm, out tmp))
            return true;
        return false;
    }

    public bool isVarNameEqualsKeyWord(string lexemm)
    {
        Debug.Log($"Trying define var name {lexemm}");
        char[] separators = new char[] { ',' }; 
        if (lexemmsTable.Any(x => x.Key.Split(separators).Contains(lexemm)))   
            return true;
        return false;
    }

    public bool isIdentificator(string str)
    {
        Debug.Log($"LEX_ANALYZER: Is Identificator? : {str}");

        if(DefineIdentificatorType(str) != IdentificatorType.Unknown)
            return true;
        return false;
    }

    public IdentificatorType DefineIdentificatorType(string str)
    {
        int tmp;
        float tmpf;

        if (str.StartsWith("'") && str.EndsWith("'"))
        {
            return IdentificatorType.Char;
        }
        else if (int.TryParse(str, out tmp) || float.TryParse(str, out tmpf))
        {
            return IdentificatorType.Digit;
        }
        else if (str.EndsWith("h"))
        {
            if(!str.StartsWith("0"))
            {
                Core.statusBarRenderer.ShowSolveText("If you want to declare HEX value it's must contains '0' at start and 'h' in the end of HEX value.");
                return IdentificatorType.Unknown;
            }
            if (RegExpressions.hexIdentificator.IsMatch(str))
                return IdentificatorType.Hex;
        }
        else if (str.EndsWith("b"))
        {
            string tmps = str.Substring(0, str.Length);
            if (RegExpressions.binIdentificator.IsMatch(tmps))
                return IdentificatorType.Binary;
        }
        else if (RegExpressions.defineNumOrStrCharArray.IsMatch(str))
            return IdentificatorType.Arr;
        else if (str == "" || str == "?")
            return IdentificatorType.Null;
        else if (IsCorrectVarName(str))
            return IdentificatorType.Var;
        else if (RegExpressions.metka.IsMatch(str))
            return IdentificatorType.Metka;
        
        return IdentificatorType.Unknown;
    }

    public int GetLexemmeCode(string lexemm)
    {
        foreach (KeyValuePair<string, int> pair in lexemmsTable)
            if (pair.Key == lexemm || pair.Key.Contains(lexemm))
                return pair.Value;
        return 0;
    }

    public string GetSegmentName()
    {
        return segmentName;
    }

    public bool IsStrArrayContainsLexemm(string lexemm)
    {
        foreach (KeyValuePair<string, int> pair in lexemmsTable)
            if (pair.Key.Contains(lexemm))
                return true;

        return false;
    }

    // Удаление комментариев
    public void FindAndRemoveComments(List<string> code)
    {
        string processed = string.Empty;

        for(int i = 0; i < code.Count; i++)
        {
            Debug.Log(code[i]);
            if (code[i].StartsWith(";"))
                code.RemoveAt(i);           

            if(code[i].Contains(";"))
            {
                for(int j = 0; j < code[i].Length; j++)
                {
                    if(code[i][j] == ';')
                        break;
                    processed += code[i][j];
                }
                code[i] = processed;
            }
        }
    }

    // Удаление цветовых тегов
    public void FindAndRemoveColorTags(List<string> code)
    {
        for (int i = 0; i < code.Count; i++)
            foreach (string tag in CodeColorizer.tags)
                if (code[i].Contains(tag))
                    code[i] = code[i].Replace(tag, "");              
    }

    public bool IsCorrectVarName(string name)
    {
        if (EnumTypesBase.GetCommandsArray().Contains(name))   
            return false;                                       
        if(RegExpressions.correctVarName.IsMatch(name))
            return true;        
        return false;
    }

    public int GetVarSizeByName(string name)
    {
        return variablesTable.Find(x => x.Name == name).Size;
    }

    // Анализ правильности шаблона
    /*
        nata segment para 'code'
        assume cs:nata, ds:nata
        org 100h
        begin: jmp main
        main proc near
        main endp
        nata ends
        end begin
     */

    public bool IsTemplateCodePart(string line)
    {
        string[] tmp = new string[] { };

        if (RegExpressions.IsOnlyRegexMathString(line))
        {
            strongTemplatePatterns++;
            return true;
        }
        else if (RegExpressions.assumeLine.IsMatch(line))
        {
            patternsWithNamedParts++;
            return true;
        }
        else if(RegExpressions.segName.Matches(line).Count == 2
            || RegExpressions.segName.Matches(line).Count == 4)
        {
            patternsWithNamedParts++;
            return true;
        }
        else
        {
            if(line.Contains(endSeg))
            {
                Debug.Log("!!! line.Contains(endSeg) && line.Split(new char[] { ' ' }).Length == 2 !!!");
                //Debug.Log($"{line}    patternsWithNamedParts { patternsWithNamedParts++}");
                patternsWithNamedParts++;
                return true;
            }
            if (tmp.Length > 1 && tmp[1] == "segment")
            {
                Debug.Log($"--------- Segment def line: {line}  patternsWithNamedParts {patternsWithNamedParts}");
                patternsWithNamedParts++;
                segmentName = line.Split(new char[] { ' ' })[0];
                Debug.Log($"--------- Segment name: {segmentName}");
                return true;
            }
            tmp = line.Split(new char[] { ' ' });
            if (RegExpressions.correctVarName.IsMatch(tmp[0]))
            {
                 switch (tmp.Length)
                 {
                     case 2:
                        if (RegExpressions.endSegment.IsMatch(tmp[1]))
                        {
                            patternsWithNamedParts++;
                            Debug.Log($"--------- End segment line: {line}  patternsWithNamedParts {patternsWithNamedParts}");
                            return true;
                        }   
                           break;
                     case 4:
                        if (RegExpressions.segmentDef.IsMatch(tmp[1] + " " + tmp[2] + " " + tmp[3]))
                        {
                            patternsWithNamedParts++;
                            Debug.Log($"--------- Segment def line: {line}  patternsWithNamedParts {patternsWithNamedParts}");
                            segmentName = line.Split(new char[] { ' ' })[0];
                            Debug.Log($"--------- Segment name: {segmentName}");
                            return true;
                        }
                        //if(tmp[1] == "segment")
                        //{
                        //    Debug.Log($"--------- Segment def line: {line}  patternsWithNamedParts {patternsWithNamedParts}");
                        //    patternsWithNamedParts++;
                        //    segmentName = line.Split(new char[] { ' ' })[0];
                        //    Debug.Log($"--------- Segment name: {segmentName}");
                        //    return true;
                        //}
                        break;
                 }
            }
        }
        return false;
    }

    public void FindAndIsolateTemplateCode(List<string> code)
    {
        Debug.Log($"\n ******************* FindAndIsolateTemplateCode ******************* \n");
        for (int i = 0; i < code.Count; i++)
        {
            if(IsTemplateCodePart(code[i].ToLower()))
            {
                templateCodeLines++;
                Debug.Log($"Template code: {code[i]}   templateCodeLines: {templateCodeLines}");
                codeTemplateParts.Add(code[i].TrimStart(' ').TrimEnd(' '));
            }
            else
            {
                userCodeLines++;
                Debug.Log($"User code: {code[i]}   userCodeLines: {userCodeLines}");
                userCode.Add(code[i]);
                if (code[i].Contains("segment"))
                {
                    segmentName = code[i].Split(new char[] { ' ' })[0];
                    Debug.Log($"--------- Segment name: {segmentName}");
                    codeTemplateParts.Add(code[i].TrimStart(' ').TrimEnd(' '));
                }
            }
        }
    } 

    public bool IsCorrectTemplateCode(List<string> code)
    {
        Debug.Log($"\n ******************* IsCorrectTemplateCode ******************* \n");
        Debug.Log(code.First());
        foreach (string line in code)
            Debug.Log($"TEMPLATE LINE: {line}");

        Debug.Log($"expectedStrongTemplatePatterns: {expectedStrongTemplatePatterns}  find: {strongTemplatePatterns}");
        Debug.Log($"expectedPatternsWithName: {expectedPatternsWithName}  find: {patternsWithNamedParts}");
        if (strongTemplatePatterns == expectedStrongTemplatePatterns
            && patternsWithNamedParts == expectedPatternsWithName)
        {
            ErrorChecker.ClearError(missedComTemplatePart);
            ErrorChecker.ClearError(missedSegmentName);
            ErrorChecker.ClearError(incorrectComTemplate);
            return true;
        }
            
        else
        {
            string tmp;
            if (strongTemplatePatterns < expectedStrongTemplatePatterns)
            {
                Error tmpErr;
                List<string> expectedPatternLines = new List<string>()
                { "org 100h", "begin: jmp main", "main proc near", "main endp", "end begin" };  
                for (int i = 0; i < expectedPatternLines.Count; i++)
                {
                    if (codeTemplateParts.Contains(expectedPatternLines[i]))
                        continue;
                    else
                    {
                        tmpErr = ErrorChecker.AddErrDescription(LogMessagesBase.lexAnalyzerMissedComTemplatePart, expectedPatternLines[i], missedComTemplatePart);
                        ErrorChecker.AddError(tmpErr);
                    }
                }
            }
            if (patternsWithNamedParts < expectedPatternsWithName)
            {


                Error tmpErr = missedSegmentName;
                string assumeTwoSeg = $"assume cs:{segmentName}, ds:{segmentName}";
                string assumesTwoSeg = $"assume ds:{segmentName}, cs:{segmentName}";               
                
                List<string> expectedPatternsWithSegName = new List<string>()
                { segmentName + " segment para 'code'", segmentName + " " + endSeg };

                for (int i = 0; i < expectedPatternsWithSegName.Count; i++)
                {
                    if (RegExpressions.segName.Matches(expectedPatternsWithSegName[i]).Count == 2 ||
                    RegExpressions.segName.Matches(expectedPatternsWithSegName[i]).Count == 4)
                    {
                        continue;
                    }
                    else if (codeTemplateParts.Contains(expectedPatternsWithSegName[i]))
                    {
                        continue;
                    }
                    else
                    {
                        tmp = $"LEX_ANALYZER: Error!!! Missed segment name: {segmentName}!";
                        tmpErr.Description = tmp;
                        ErrorChecker.AddError(missedSegmentName);
                    }
                }
            }
        }
        return false;
    }

    public bool IsLineMustContainsComa(List<string> code)
    {
        List<string> commandsWithComa = new List<string>()
        {
            "mov", "add", "sub", "test", "lea", "cmp"
        };
        return commandsWithComa.Contains(code[0]);
    }
    
    public bool Analyze()
    {
        Core.ShowLog(LogMessagesBase.lexAnalyzerStarted);

        FindAndRemoveColorTags(code);
        FindAndRemoveComments(code);
        FindAndIsolateTemplateCode(code);
        if(!IsCorrectTemplateCode(codeTemplateParts))
        {
            ErrorChecker.AddError(incorrectComTemplate);
            return false;
        }

        foreach (string line in userCode)
            Debug.Log(line);

        if(userCode.Count == 0)
        {
            Core.ShowLog(LogMessagesBase.lexAnalyzerFindEmptyComTemplate);
            return true;
        }
        else
        {
            for (int i = 0; i < code.Count; i++)
                if (!IsTemplateCodePart(code[i]))
                    isDone = IsCorrect(code[i], i + 1);
                else continue;
            return isDone;
        }
    }

    public string GetNormalForm(string line)
    {
        line = line.Trim();
        line = RegExpressions.linePartSpaces.Replace(line, " ");
        line = RegExpressions.comaPattern.Replace(line, " , ");
        return line;
    }

    public bool CheckOneWordOperation(string lexemm, int line)
    {
        if (lexemm == "")
            return true;
        List<string> oneWordOperations = new List<string>() { "ret", "cbw", "cwd", "cdq" };
        if (DefineIdentificatorType(strLexemms[0]) == IdentificatorType.Metka)
        {
            Debug.Log($"METKA: line {line}: {strLexemms[0]}");
            marks.Add(strLexemms[0]);
        }
        if (oneWordOperations.Contains(strLexemms[0].TrimStart().TrimEnd()))
        {
            return true;
        }
        else
        {
            ErrorChecker.AddError(new Error(ErrorType.IllegalOperation, $"ERROR!!! Incorrect format! Command '{strLexemms[0]}' can't be used like one-word operation!"));
            return false;
        }
    }

    // Проверка правильности строки
    public bool IsCorrect(string str, int line)
    {
        int correct = 0;

        if (str.TrimEnd() == "\n") return true;

        if (RegExpressions.defineNumOrStrCharArray.IsMatch(str))
        {
            string arrValue = RegExpressions.arrayValues.Match(str).Value;
            Debug.Log($"Array: {str}");
            Debug.Log(arrValue);
            return true;
        }

        str = GetNormalForm(str);
        Debug.Log($"NORMAL FORM: {str}");
        strLexemms = str.Split(new char[] { ' ' }); 
        
        if(strLexemms.Length == 1)
        {
            CheckOneWordOperation(strLexemms[0], line);
        }

        foreach (string lexemm in strLexemms)
        {
            if (lexemmsTable.ContainsKey(lexemm) || IsStrArrayContainsLexemm(lexemm))
            {
                lexemm.ToLower();
                analyzedCodeTable.Add($" Line {line,5} | {lexemm} | {GetLexemmeCode(lexemm)}");
                Debug.Log($" Line {line,5} | {lexemm} | {GetLexemmeCode(lexemm)}");

                correct++;
            }
            else
            {
                if(isIdentificator(lexemm))
                {
                    Debug.Log($"Type: {DefineIdentificatorType(lexemm)}");
                    Debug.Log($"key at 1 {lexemmsTable.ElementAt(1).Key} strLexemms[strLexemms.Length - 2] = {strLexemms[strLexemms.Length - 2]}");

                    if (lexemmsTable.ElementAt(1).Key.Contains(strLexemms[strLexemms.Length - 2]) && lexemm != ",")
                    {
                        
                        Debug.Log($"LEX_ANALYZER: String {line,5} | variable {lexemm} after {strLexemms[strLexemms.Length - 2]}");
                        if (isVarNameEqualsKeyWord(strLexemms[0]))    
                        {
                            string tmp = $"LEX_ANALYZER: Error!!! Variable can't be named like keyword '{strLexemms[0]}'!";
                            ErrorChecker.AddError(new Error(ErrorType.IllegalSymbol, tmp));
                        }
                        else if(variablesTable.Any(x => x.Name == lexemm))  // just if
                        {
                            string tmp = LogMessagesBase.lexAnalyzerVarAlrdExist;
                            tmp = $"LEX_ANALYZER: Error!!! Variable with name {strLexemms[0]} already exist!";
                            ErrorChecker.AddError(new Error(ErrorType.varAlreadyExist, tmp));
                        }
                        else
                        {
                            variablesTable.Add(new Variable(strLexemms[0], line, strLexemms[1], lexemm));
                            if (variablesTable.Count == 1)
                                variablesTable[0].Addres = "0";
                            Debug.Log($"LEX_ANALYZER: String {line,5} | {variablesTable.Last().GetVariableData()}");
                            variables.Add(lexemm);

                            correct++;
                        }
                    }
                    Debug.Log($"LEX_ANALYZER: String {line,5} | identificator {lexemm} | type: {DefineIdentificatorType(lexemm)}");
                }
                else
                {
                    Error tmp = ErrorChecker.AddErrDescription(LogMessagesBase.lexAnalyzerUndefIdentificator, $"{lexemm} in line {line+1}", undefIdentificator);
                    ErrorChecker.AddError(tmp);
                }
            }

            if (IsLineMustContainsComa(strLexemms.ToList()))
            {
                if (strLexemms[2] != ",")
                {
                    ErrorChecker.AddError(new Error(ErrorType.MissedComa, $"ERROR!!! Missed ',' after '{strLexemms[1]}'"));
                    return false;
                }
            }
        }
        /* End loop */
        if (correct == strLexemms.Length)
        {
            Core.ShowLog($"LEX_ANALYZER: String {line+1,5} ===> Done | correct lexemms: {correct} | lexemms in line: {strLexemms.Length}");
            analyzedCodeLines.Add(strLexemms);
            return true;
        }
        else
        {
            Core.ShowLog(new Error(ErrorType.IllegalOperation, $"LEX_ANALYZER: String {line + 1,5}  ===> ERROR! | correct lexemms: {correct} | lexemms in line: {strLexemms.Length}"));
            return false;
        }
    }
}

