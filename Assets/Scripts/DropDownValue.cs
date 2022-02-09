using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class DropDownValue : MonoBehaviour
{

   [SerializeField] private TMPro.TMP_Dropdown _dropdown;
    void Start()
    {
        _dropdown = GetComponent<TMPro.TMP_Dropdown>();

        Debug.Log("Starting Dropdown Value : " + _dropdown.value);
        Debug.Log("Selected " + _dropdown.options[_dropdown.value].text);
    }

    public string GetSelectedOption()
    {
        return _dropdown.options[_dropdown.value].text; 
    }
}
