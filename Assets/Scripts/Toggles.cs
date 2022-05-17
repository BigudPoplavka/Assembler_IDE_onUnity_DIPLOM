using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Toggles : MonoBehaviour
{
    [SerializeField] private Toggle _solveWithErrorsToggle;
    [SerializeField] private Toggle _dontShowLogToggle;
    [SerializeField] private Toggle _buildOnlyAfterSyntaxAnalyzeToggle;
    [SerializeField] private Toggle _autoRunEmulAfterBuildToggle;
    [SerializeField] private Toggle _manageEmulationToggle;
    [SerializeField] private Toggle _showAddresesToggle;
    [SerializeField] private Toggle _autoEmulationStepsToggle;

    public Toggle[] toggles = new Toggle[7];
    private bool[] currValues = new bool[7];

    void Start()
    {
        toggles = new Toggle[]
        {
            _solveWithErrorsToggle,
            _dontShowLogToggle,
            _buildOnlyAfterSyntaxAnalyzeToggle,
            _autoRunEmulAfterBuildToggle,
            _manageEmulationToggle,
            _showAddresesToggle,
            _autoEmulationStepsToggle
        };
    }

    public bool[] GetCurrToggleValues()
    {
        for (int i = 0; i < toggles.Length; i++)
            currValues[i] = toggles[i].isOn;
        return currValues;
    }

    public void SetTogglesValues(bool[] values)
    {
        string tmp = string.Empty;
        foreach (bool val in values)
            tmp += val.ToString() + " ";
        Debug.Log(tmp);

        for (int i = 0; i < values.Length; i++)
        {
            toggles[i].isOn = values[i];
        }
    }

    public Toggle[] GetToggles()
    {
        return toggles;
    }
}
