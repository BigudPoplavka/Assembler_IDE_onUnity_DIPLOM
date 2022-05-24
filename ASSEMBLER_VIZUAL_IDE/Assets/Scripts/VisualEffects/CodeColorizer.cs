using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

[System.Serializable]
public class OnOpenedCodeColorized: UnityEvent<List<string>> { }

public class CodeColorizer : MonoBehaviour
{
    [SerializeField] private OnOpenedCodeColorized _onOpenedCodeColorized = new OnOpenedCodeColorized();
    [SerializeField] private TMPro.TMP_InputField _codeInput;
    public static TMPro.TMP_InputField _input;
    private Dictionary<List<string>, string> _keyWordsColors = new Dictionary<List<string>, string>();
    private List<List<string>> _keys = new List<List<string>>();
    string[] words;
    string endtag = "</color>";
    string valueColor = "<#AE81FF>";

    public static string[] tags;

    void Start()
    {
        _input = _codeInput;

        tags = new string[] { "</color>", "<#A6E22E>", "<#FD971F>", 
            "<#F92672>", "<#E6DB74>", "<#AE81FF>", "<#66D9EF>" };

        _keyWordsColors.Add(EnumTypesBase.GetCommandsArray(), "<#F92672>");
        _keyWordsColors.Add(new List<string>() { "cs", "ds", "ss", "es", "main" }, "<#66D9EF>");
        _keyWordsColors.Add(new List<string>() {
        "al", "ah", "bl", "b", "cl", "ch", "dl", "dh", "ax", "bx", "cx",
        "dx", "eax", "ebx", "ecx", "edx", "si", "di", "sp", "bp", "ip"
        }, "<#FD971F>");
        _keyWordsColors.Add(new List<string>() { "segment", "para", "assume", 
            "org", "jmp", "proc", "near", "ends", "endp", "end", "begin" }, "<#F92672>");

        _keys = _keyWordsColors.Keys.ToList();
    }

    void Update()
    {
        
    }

    public void ColorizeCode(List<string> lines)
    {
        List<string> resultLines = new List<string>();
        List<string> tmp = new List<string>();
        string newLine = string.Empty;
        string tmpAfter = string.Empty;
        List<string> key = new List<string>();

        for(int i = 0; i < lines.Count; i++)
        {
            if (lines[i] == "") continue;
            tmpAfter = "";
            tmp = lines[i].Split(new char[] { ' ' }).ToList();

            //if (lines[i].Contains("segment"))
            //{
            //    tmpAfter = "<#A6E22E>" + tmp[0] + endtag + " ";
            //}

            for (int j = 0; j < tmp.Count; j++)
            {
                if (tmp[j] != "")
                {
                    key = _keys.Find(x => x.Contains(tmp[j]));
                    if (key != null)
                    { 
                        newLine = lines[i].Replace(tmp[j], _keyWordsColors[key] + tmp[j] + endtag);
                        tmpAfter += _keyWordsColors[key] + tmp[j] + endtag + " ";
                    }
                    else if(tmp[j].EndsWith(",") || tmp[j].EndsWith(":"))
                    {
                        key = _keys.Find(x => x.Contains(tmp[j].Substring(0, tmp[j].Length - 1)));
                        if(key != null)
                            tmpAfter += _keyWordsColors[key] + tmp[j] + endtag + " ";
                        else tmpAfter += "<#AE81FF>" + tmp[j] + endtag + " ";
                    }
                    else
                    {
                        tmpAfter += "<#AE81FF>" + tmp[j] + endtag + " ";
                    }
                }
            }
            resultLines.Add(tmpAfter + "\n");
        }
        _onOpenedCodeColorized?.Invoke(resultLines);
    }
}
