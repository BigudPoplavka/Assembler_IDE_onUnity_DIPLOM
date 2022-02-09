using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ErrorEvent : UnityEvent<string> { }

[System.Serializable]
public class LexAnalyzer: MonoBehaviour
{
    public string currString;
    public string errLog;
    public string identificatorName = @"[A-Z]";

    public string[] strLexemms;

    public List<string> code;
    public List<string> variables = new List<string>();
    public List<string> codeAnalyzeTable = new List<string>();
    public List<string> analyzedCodeTable = new List<string>();

    public delegate bool checkLexemm(string lexemm);

    public Dictionary<string, int> lexemmsTable = new Dictionary<string, int>();
    public Dictionary<int, checkLexemm> valuesTable = new Dictionary<int, checkLexemm>();


    // События

    [SerializeField] public ErrorEvent errorEvent = new ErrorEvent();

    public LexAnalyzer(List<string> parsedCode)
    {
        code = parsedCode;
        InitLexemmsTables();
    }

    public void InitLexemmsTables()
    {
        lexemmsTable.Add(",", 1);
        lexemmsTable.Add("db, dw, dd, dq, dt", 2);
        lexemmsTable.Add("add, sub, idiv, imul, div, mul, inc, dec", 3);
        lexemmsTable.Add("mov, push, pop", 4);
        lexemmsTable.Add("cmp, equ", 5);
        lexemmsTable.Add("pop", 6);
        lexemmsTable.Add("al, ah, bl, bh, cl, ch, dl, dh", 7);
        lexemmsTable.Add("ax, bx, cx, dx", 8);
        lexemmsTable.Add("DS, SS, ES", 9);
        lexemmsTable.Add("CS", 10);
        lexemmsTable.Add("and, or, eor, xor, not", 11);
        lexemmsTable.Add("je, jne, jl, jnge, jle, jng, jg, jnle, jge, jnl, jb, jnae, jbe, jna, ja, jnbe, jae, jnb", 12);
        lexemmsTable.Add("loop, loope, loopz, loopne, loopnz", 13);
        lexemmsTable.Add("lods, lodsb, lodsw, lodsd", 14);
        lexemmsTable.Add("test, bound", 15);
        lexemmsTable.Add("macro, endm, ret", 16);
        lexemmsTable.Add("si, di, sp, bp, ip", 17);

        valuesTable.Add(11, new checkLexemm(isIntDigit));
        valuesTable.Add(12, new checkLexemm(isIdentificator));
    }

    // Проверки типов
    public bool isIntDigit(string lexemm)
    {
        int tmp = 0;
        if (int.TryParse(lexemm, out tmp))
            return true;
        return false;
    }

    public bool isIdentificator(string str)
    {
        Debug.Log($"LEX_ANALYZER: Is Identificator? : {str}");
        string tmp = string.Empty;

        if(DefineIdentificatorType(str) != IdentificatorType.Unknown)
            return true;
        return false;
    }

    public IdentificatorType DefineIdentificatorType(string str)
    {
        int tmp;
        float tmpf;

        if(str.StartsWith("'") && str.EndsWith("'"))
        {
            // строка, символ или массив?

            return IdentificatorType.Char;
        }
        else if(int.TryParse(str, out tmp) || float.TryParse(str, out tmpf))
        {
            return IdentificatorType.Digit;
        }
        else if(str.EndsWith("h"))
        {      
            string tmps = str.Substring(0, str.Length);
            if(RegExpressions.hexIdentificator.IsMatch(tmps))
            {
                return IdentificatorType.Hex;
            }
        }
        else if(str.EndsWith("b"))
        {
            string tmps = str.Substring(0, str.Length);
            if (RegExpressions.binIdentificator.IsMatch(tmps))
            {
                return IdentificatorType.Binary;
            }
        }
        else if(str == "" || str == "?")
        {
            return IdentificatorType.Null;
        }

        return IdentificatorType.Unknown;
    }

    public int GetLexemmeCode(string lexemm)
    {
        foreach (KeyValuePair<string, int> pair in lexemmsTable)
            if (pair.Key == lexemm || pair.Key.Contains(lexemm))
                return pair.Value;
        return 0;
    }

    public bool IsStrArrayContainsLexemm(string lexemm)
    {
        foreach (KeyValuePair<string, int> pair in lexemmsTable)
            if (pair.Key.Contains(lexemm))
                return true;

        return false;
    }

    // Проверки класса лексеммы принадлежности

    public bool IsRegister(string lex)
    {
        foreach(Regname regname in Enum.GetValues(typeof(Regname)))
            if(lex == regname.ToString())
                return true;

        return false;
    }

    public bool IsCanBeAloneInStr(string lex)
    {
        foreach (AloneLexemms regname in Enum.GetValues(typeof(AloneLexemms)))
            if (lex == regname.ToString())
                return true;

        return false;
    }

    public bool IsOneOrMoreOperands(string lex)
    {
        foreach (OneOrMoreParamsCommands regname in Enum.GetValues(typeof(OneOrMoreParamsCommands)))
            if (lex == regname.ToString())
                return true;

        return false;
    }

    public bool IsCorrectCommand(string str)
    {
        return true;
    }

    public void FindAndRemoveComments(List<string> code)
    {
        string processed = string.Empty;

        for(int i = 0; i < code.Count; i++)
        {
            if(code[i].StartsWith(";"))
            {
                code.RemoveAt(i);
            }

            if(code[i].Contains(";"))
            {
                for(int j = 0; j < code[i].Length; j++)
                {
                    if(code[i][j] == ';')
                    {
                        break;
                    }
                    processed += code[i][j];
                }
                code[i] = processed;
            }
        }
    }

    // Анализ кода

    public bool Analyze()
    {
        Debug.Log("LEX_ANALYZER: Analyze started...");
        bool isDone = false;

        Debug.Log("LEX_ANALYZER: code parsed");
        FindAndRemoveComments(code);
        Debug.Log("LEX_ANALYZER: comments removed");
        foreach (string line in code)
            Debug.Log(line);
        Debug.Log("\n---------------------------------\n");

        for (int i = 0; i < code.Count; i++)
        {
            isDone = IsCorrect(code[i], i+1);
        }
        return isDone;
    }

    // Проверка правильности строки

    public bool IsCorrect(string str, int line)
    {
        int correct = 0;

        strLexemms = str.Split(new char[] { ' ' });

        string log = "";
        foreach (string s in strLexemms)
            log += s + " ";
        Debug.Log(log);

        for (int i = 0; i < strLexemms.Length; i++)
            if (strLexemms[i].EndsWith(","))
                strLexemms[i] = strLexemms[i].Remove(strLexemms[i].Length - 1);
        

        foreach (string lexemm in strLexemms)
        {
            if (lexemmsTable.ContainsKey(lexemm) || IsStrArrayContainsLexemm(lexemm))
            {
                lexemm.ToLower();

                analyzedCodeTable.Add($" Line {line,5} | {lexemm} | {GetLexemmeCode(lexemm)}");
                Debug.Log($" Line {line,5} | {lexemm} | {GetLexemmeCode(lexemm)}");

                // TODO: Save variables

                correct++;
            }
            else
            {
                if(isIdentificator(lexemm))
                {
                    

                    //if(DefineIdentificatorType(lexemm) == IdentificatorType.Null)
                    //{
                        Debug.Log(lexemmsTable.ElementAt(1).Key);
                        if(lexemmsTable.ElementAt(1).Key.Contains(strLexemms[strLexemms.Length - 2]))
                        {
                            Debug.Log($"LEX_ANALYZER: String {line,5} | variable {lexemm} after {strLexemms[strLexemms.Length - 2]}");
                            variables.Add(lexemm);
                            correct++;
                        }
                        Debug.Log($"LEX_ANALYZER: String {line,5} | identificator {lexemm} | type: {DefineIdentificatorType(lexemm)}");                      
                        correct++;
                        
                    //}

                    Debug.Log($"LEX_ANALYZER: String {line,5} | identificator {lexemm} | type: {DefineIdentificatorType(lexemm)}");
                    correct++;
                }
                else
                {
                    errorEvent?.Invoke(Error.errTestErr);
                    Debug.Log($"LEX_ANALYZER: String {line,5} ===> ERROR! Undefine identificator type!");
                }
            }
        }


        if (correct == strLexemms.Length)
        {
            Debug.Log($"LEX_ANALYZER: String {line, 5} ===> Done");
            return true;
        }

        errorEvent?.Invoke(Error.errTestErr);
        Debug.Log($"LEX_ANALYZER: String {line,5} ===> ERROR!");
        return false;
    }

}

