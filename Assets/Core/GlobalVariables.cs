using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static bool _isCodeInputSelected;
    public static bool _isCodeAnalyzed;
    public static bool _isSyntaxCorrect;
    public static bool _isCodeBuilded;
    public static bool _isEmulationAvailable;
    public static bool _isEmulationStarted;

    public static int _variablesCount;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnInuptFieldSelect()
    {
        _isCodeInputSelected = true;
        Debug.Log($"OnInuptFieldSelect: _isCodeInputSelected {_isCodeInputSelected}");
    }

    public void OnInuptFieldDeselect()
    {
        _isCodeInputSelected = false;
        Debug.Log($"OnInuptFieldDeselect: _isCodeInputSelected {_isCodeInputSelected}");
    }
}
