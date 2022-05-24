using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StatusBar : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField _inputField;

    [SerializeField] private TMPro.TMP_Text _curStr; 
    [SerializeField] private TMPro.TMP_Text _curCharPos; 
    [SerializeField] private TMPro.TMP_Text _strings;

    [SerializeField] private string[] _tmp;

    void Start()
    {

    }

    void Update()
    {

    }

    public int GetLinesCount()
    {
        List<string> codeList = new List<string>();
        _tmp = _inputField.text.Split(new char[] { '\n' });

        if (_inputField.text != null || _inputField.text != string.Empty)
        {
            foreach (string line in _tmp)
                    codeList.Add(line);
            return codeList.Count;
        }
        return 0;
    }

    public void ShowCodeInputFieldData()
    {
        _strings.text = GetLinesCount().ToString();
    }
}
