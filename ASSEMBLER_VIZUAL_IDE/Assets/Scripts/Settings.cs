using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class Settings: MonoBehaviour
{
    private Settings _instance;

    [SerializeField] private bool _solveWithErrors;
    [SerializeField] private bool _dontShowLog;
    [SerializeField] private bool _buildOnlyAfterSyntaxAnalyze;
    [SerializeField] private bool _autoRunEmulAfterBuild;
    [SerializeField] private bool _manageEmulation;
    [SerializeField] private bool _showAddreses;

    private bool[] _flags, _tmp;
    private Toggle[] _toggles = new Toggle[6];
    public Settings Instance { get => _instance; set => _instance = value; }
    public bool[] Flags { get => _flags; set => _flags = value; }
    public Toggle[] Toggles { get => _toggles; set => _toggles = value; }

    private string serializedSettings;

    public Settings()
    {
        Instance = this;
        Flags = new bool[] 
        { 
            _solveWithErrors, _dontShowLog, _buildOnlyAfterSyntaxAnalyze, _autoRunEmulAfterBuild, _manageEmulation, _showAddreses//, _autoEmulationSteps
        };
        
    }

    public void ChangeFlagsValues()
    {
        Flags = new bool[]
        {
            _solveWithErrors, _dontShowLog, _buildOnlyAfterSyntaxAnalyze, _autoRunEmulAfterBuild, _manageEmulation, _showAddreses//, _autoEmulationSteps
        };
        string tmp = string.Empty;
        foreach (bool val in Flags)
            tmp += val.ToString() + " ";
        Debug.Log(tmp);
    }
    public void ChangeFlagsValues(bool[] values)
    {
        _solveWithErrors = values[0];
        _dontShowLog = values[1]; 
        _buildOnlyAfterSyntaxAnalyze = values[2];
        _autoRunEmulAfterBuild = values[3];
        _manageEmulation = values[4];
        _showAddreses = values[5];
    }
    public void ChangeTogglesValues()
    {
        for (int i = 0; i < Toggles.Length; i++)
        {
            Toggles[i].isOn = Instance.Flags[i];
        }
    }
    public void SetDefaultSettings()
    {
        _solveWithErrors = true;
        _dontShowLog = false;
        _buildOnlyAfterSyntaxAnalyze = false;
        _autoRunEmulAfterBuild = false;
        _manageEmulation = true;
        _showAddreses = true;

        ChangeFlagsValues();
        serializedSettings = JsonUtility.ToJson(this, true);
        Debug.Log(serializedSettings);
    }

    public void CheckSettingsPresetFile()
    {
        if (!File.Exists(CoreDirictories.settings))
        {
            File.Create(CoreDirictories.settings).Dispose();
            SetDefaultSettings();
            WriteSettingsToFile();
        }
        LoadSettings();
    }

    public void SaveSettings()
    {
        _tmp = Core.toggles.GetCurrToggleValues();
        Debug.Log($"{_tmp[0]} {_tmp[1]} {_tmp[2]} {_tmp[3]} {_tmp[4]} {_tmp[5]} ");
        ChangeFlagsValues(_tmp);
        serializedSettings = JsonUtility.ToJson(Instance, true);
        Debug.Log(serializedSettings);
    }

    public void WriteSettingsToFile()
    {
        if(File.Exists(CoreDirictories.settings))
        {
            File.Delete(CoreDirictories.settings);
            File.Create(CoreDirictories.settings).Close();
        }

        using (StreamWriter writer = new StreamWriter(CoreDirictories.settings, false))
        {
            writer.Write(serializedSettings);
        }
    }

    public void LoadSettings()
    {
        using (StreamReader reader = new StreamReader(CoreDirictories.settings))
        {
            serializedSettings = reader.ReadToEnd();
        }
        Debug.Log(serializedSettings);
    }

}
