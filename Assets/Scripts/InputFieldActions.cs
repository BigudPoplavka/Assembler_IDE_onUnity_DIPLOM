using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class InputFieldActions: MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField _inputField;
    [SerializeField] private TMPro.TMP_Dropdown _dropdown;
    [SerializeField] private GameObject _intellisenseBlockPosition;

    [SerializeField] private DropDownValue _optionValue;

    List<string> help = EnumTypesBase.GetCommandsArray();
    List<string> tmp = new List<string>();

    private int _currCaretPosition;
    private int _stringIndex;

    private bool _isInputFieldSelected = false;

    private void Start()
    {
        _inputField.onValueChanged.AddListener(delegate { OnTextChanged(); });
        _inputField.onSelect.AddListener(delegate { OnInputFieldSelected(); });
        _inputField.onDeselect.AddListener(delegate { OnInputFieldDeselected(); });
        _inputField.onEndEdit.AddListener(delegate { OnTextEndEdit(); });

        _dropdown.transform.position = _intellisenseBlockPosition.transform.position;
    }

    public void Update()
    {
        if(_isInputFieldSelected == true)
        {
            _currCaretPosition = _inputField.caretPosition;
        }
    }

   

    public void SetIntellisensePosition()
    {
        tmp = _inputField.text.Split(new char[] { '\n' }).ToList();
        _dropdown.transform.position = _inputField.transform.position; // + ...

    }

    public void OnTextChanged()
    {
        // TODO: получать последнее слово отдельно для нового поиска каждое слово
        // если нажат пробел - вызывать инвоком метод?

        _dropdown.gameObject.SetActive(true);

        if (!string.IsNullOrEmpty(_inputField.text) && _inputField.text != "")
        {
            var listofvalues = help.Where(x => x.ToLower().StartsWith(_inputField.text.ToLower(), StringComparison.CurrentCulture)).ToList();

            _dropdown.ClearOptions(); 

            if (listofvalues.Count > 0)
            {
                _dropdown.AddOptions(listofvalues);     
            }
            else
            {
                _dropdown.gameObject.SetActive(false);
            }
        }

        
    }

    public void OnOptionSelected()
    {
        string replaced = string.Empty;
        int replacedWordPos = 0;
        int tmp = _currCaretPosition - _inputField.text.Length;

        Debug.Log(_inputField.text.Length);
        Debug.Log($"tmp {tmp}");

        for (int i = _currCaretPosition - 1; i >= 0; i--)
        {
            Debug.Log($"i = {i+1}   i-1 = {i}  text[i] = {_inputField.text[i] }");

            if (_inputField.text[i] != ' ')
            {
                replaced += _inputField.text[i];
                replacedWordPos = i;
                // ?
                _inputField.text = _inputField.text.Replace(_inputField.text[i], ' ');
                // ?
            }
            else
            {
                break;
            }
        }

        char[] tmpStr = replaced.ToCharArray();
        Array.Reverse(tmpStr);
        replaced = new string(tmpStr);


       // string selected = _optionValue.GetSelectedOption();
       // Debug.Log($"selected {selected}");

        Debug.Log($"replaced = {replaced} pos = {replacedWordPos}");
        // ?
        _inputField.text = _inputField.text.Insert(replacedWordPos, "repl");
        // ?
    }

    public void OnInputFieldSelected()
    {
        _isInputFieldSelected = true;
    }

    public void OnInputFieldDeselected()
    {
        _isInputFieldSelected = false;
    }
    public void OnTextEndEdit()
    {

    }

    public void ResizeText(int param)
    {
        _inputField.pointSize += 2 * param;
    }

}
