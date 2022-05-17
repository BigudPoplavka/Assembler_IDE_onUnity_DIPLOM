using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class CodeColorizer : MonoBehaviour
{
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

        _keys = _keyWordsColors.Keys.ToList();
    }

    void Update()
    {
        
    }

    public void ColorizeCode()
    {
        words = _codeInput.text.Split(new char[] { ',', ' ' });
        Debug.Log($"Words: {words.Length}");

        foreach (string word in words)
            for (int i = 0; i < _keyWordsColors.Keys.Count; i++)
            {               
                if (_keys[i].Contains(word))
                {
                    Debug.Log($"Key: {_keys[i]}\n Word: {word}\n Replace: {_keyWordsColors[_keys[i]] + word + endtag}");
                    _codeInput.text = _codeInput.text.Replace(word, _keyWordsColors[_keys[i]] + word + endtag);
                }
                else if(RegExpressions.hexIdentificator.IsMatch(word) || 
                    RegExpressions.binIdentificator.IsMatch(word) || 
                    RegExpressions.decIdentificator.IsMatch(word))
                {
                    _codeInput.text = _codeInput.text.Replace(word, valueColor + word + endtag);
                }
            }
    }
}
