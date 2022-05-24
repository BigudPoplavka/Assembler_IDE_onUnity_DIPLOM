using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CodeTemplateInitilizer : MonoBehaviour
{
    [SerializeField] private GameObject _progTypeSwitch;
    [SerializeField] private TMPro.TMP_Text _SwitchButtonText;
    [SerializeField] private string _value;
    [SerializeField] private string[] _templateCodeCOM;
    [SerializeField] private string[] _templateCodeEXE;
    [SerializeField] private TMPro.TMP_InputField _codeInput;

    private string templateCom, templateExe;
    private string[] _programType = new string[] { ".COM", ".EXE" };
    private Dictionary<bool, (string, string[])> _progTypes = new Dictionary<bool, (string, string[])>();

    public List<string> ReadTemplate(string path)
    {
        List<string> lines = new List<string>();
        string tmp;
        using (StreamReader reader = new StreamReader(path))
        {
            while (!reader.EndOfStream)
                lines.Add(reader.ReadLine());
        }
        return lines;
    }

    void Start()
    {
        _templateCodeCOM = new string[] {
        "<#A6E22E>newprog</color> <#F92672>segment</color> <#F92672>para</color> <#E6DB74>'code'</color>",
            "<#F92672>assume</color> <#66D9EF>cs</color>:<#A6E22E>newprog</color>, <#66D9EF>ds</color>:<#A6E22E>newprog</color>," +
            " <#66D9EF>ss</color>:<#A6E22E>newprog</color>, <#66D9EF>es</color>:<#A6E22E>newprog</color>  ",
            "<#F92672>org</color> <#AE81FF>100h</color>",
            "begin: <#F92672>jmp</color> main", "; Определение данных",
            "\n<#66D9EF>main</color> <#F92672>proc</color> <#F92672>near</color>  ",
            "; Остальной код\n", "<#66D9EF>main</color> <#F92672>endp",
            "<#A6E22E>newprog</color>  <#F92672>ends</color>", "<#F92672>end</color> begin"};
        _templateCodeEXE = new string[]
        {
            "<#F92672>CSEG</color>"
        };

        _progTypes.Add(false, (".COM", _templateCodeCOM));
        _progTypes.Add(true, (".EXE", _templateCodeEXE));

        _SwitchButtonText = _progTypeSwitch.GetComponentInChildren<TMPro.TMP_Text>();
        _SwitchButtonText.text = _progTypes[false].Item1;

        WriteTemplate(false);
    }


    void Update()
    {
        
    }

    public void WriteTemplate(bool state)
    {
        _codeInput.text = "";
        foreach (string line in _progTypes[state].Item2)
        {
            _codeInput.text += line + "\n";
        }
    }

    public void SetDefaultCodeTemplate()
    {
        Core.programType = !Core.programType;
        WriteTemplate(Core.programType);
        _SwitchButtonText.text = _progTypes[Core.programType].Item1;
    }

    public void LoadCodeFromFile(List<string> code)
    {
        _codeInput.text = "";
        foreach(string line in code)
        {
            _codeInput.text += line; 
        } 
    }
}
