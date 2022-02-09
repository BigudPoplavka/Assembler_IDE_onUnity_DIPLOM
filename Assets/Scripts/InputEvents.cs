using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class InputEvents : MonoBehaviour
{

    [SerializeField] private TMPro.TMP_InputField _inputField;
    private List<string> help = EnumTypesBase.GetCommandsArray();

    private int caretPosition;

    private bool isInputSelected;

    private string tmpText = string.Empty;

    private void Start()
    {
        _inputField.onValueChanged.AddListener(OnTextChanged);
        _inputField.onSelect.AddListener(delegate { OnInputFieldSelected(); });
        _inputField.onDeselect.AddListener(delegate { OnInputFieldDeselected(); });
        _inputField.onEndEdit.AddListener(delegate { OnTextEndEdit(); });
    }

    private void Update()
    {
        if (_inputField.isFocused)
        {
            caretPosition = _inputField.caretPosition;
        }
    }

    public void OnTextChanged(string text)
    {
        

        if (!string.IsNullOrEmpty(text) && text != "")
        {
            var listofvalues = help.Where(x => x.ToLower().StartsWith(text.ToLower(), StringComparison.CurrentCulture)).ToList();

            if (listofvalues.Count > 0)
            {
                foreach (var value in listofvalues)
                {

                }
            }
            else
            {

            }
        }
    }

    public void OnInputFieldSelected()
    {
        Debug.Log("INPUT EVENTS: OnInputFieldSelected");
        isInputSelected = true;
    }

    public void OnInputFieldDeselected()
    {
        Debug.Log("INPUT EVENTS: OnInputFieldDeselected ");
        isInputSelected = false;
    }
    public void OnTextEndEdit()
    {
        Debug.Log("INPUT EVENTS: OnTextEndEdit");
    }
}
