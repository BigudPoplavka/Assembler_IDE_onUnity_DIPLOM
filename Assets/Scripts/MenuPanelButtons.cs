using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Windows;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System;
using System.Runtime.InteropServices;


using Application = UnityEngine.Application;
using UnityEngine.Events;
using TMPro;

public class OnDebugStarted : UnityEvent<string> { }

public class MenuPanelButtons : MonoBehaviour
{
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [SerializeField] private DropdownMenu _FileMenu;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _currSettingsActiveTab;
    [SerializeField] private OnDebugStarted _onDebugStarted = new OnDebugStarted();
    [SerializeField] private UnityEvent _onClosing = new UnityEvent();
    [SerializeField] private UnityEvent _onSettingsClosing = new UnityEvent();
    
    // Источники данных 

    [SerializeField] private TMP_InputField codeInputField;

    public Dictionary<string, Action> FileMenuActions = new Dictionary<string, Action>();

    void Start()
    {
        FileMenuActions.Add("Новый", new Action(FileNew));
        FileMenuActions.Add("Открыть файл", new Action(FileOpen));
        FileMenuActions.Add("Открыть папку", new Action(FolderOpen));
        FileMenuActions.Add("Сохранить файл", new Action(FileSave));
        FileMenuActions.Add("Настройки", new Action(Settings));
        FileMenuActions.Add("Выход", new Action(Close));

        CheckRunFlag();
    }
    public void CheckRunFlag()
    {
        if (!File.Exists(CoreDirictories.runFlagPath))
        {
            File.Create(CoreDirictories.runFlagPath);
            File.Create(CoreDirictories.IOConfigPath);
        }
    }
    public void FileNew()
    {
        
    }

    public void FileOpen()
    {
#if UNITY_EDITOR
        string fileName = EditorUtility.OpenFilePanel(FilesConfig.OpenFileTitle, CoreDirictories.defaultOpenFilePath, FilesConfig.EXT_MAIN);
#endif
        string filePath = string.Empty;
    }

    public void FileSave()
    {
#if UNITY_EDITOR
        EditorUtility.SaveFilePanel(FilesConfig.OpenFileTitle, FilesConfig.SaveFileDefaultName, CoreDirictories.defaultOpenFilePath, FilesConfig.EXT_MAIN);
#endif
        string filePath = string.Empty;
    }

    public void FileSaveAs()
    {
#if UNITY_EDITOR

#endif
        string filePath = string.Empty;    
    }
    public void FolderOpen()
    {
#if UNITY_EDITOR
        EditorUtility.OpenFolderPanel(FilesConfig.OpenFolderTitle, CoreDirictories.defaultOpenFilePath, CoreDirictories.defaultOpenFilePath);
#endif
        string folderPath = string.Empty;
    }

    public void Settings()
    {
        _settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        _onSettingsClosing?.Invoke();
        _settingsPanel.SetActive(false);
    }

    public void ShowSettingsTab(GameObject panel)
    {
        _currSettingsActiveTab.SetActive(false);
        panel.SetActive(true);
        _currSettingsActiveTab = panel;
    }

    public void Close()
    {
        _onClosing?.Invoke();
        Application.Quit();
    }

    public void ChangeFullscreenMode()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void Minimize()
    {
        ShowWindow(GetActiveWindow(), 2);
    }

    public void StartEmulation()
    {

    }

    public void Build()
    {
        Debug();
        Core.BuildAnalyzedCode();
    }

    public void Debug()
    {
        EventsBase.onDebugStarted?.Invoke();
        Core.ClearConsole();
        Core.ResetSyntaxAnalyzerState();
        Core.SetSyntaxAnalyzeResult(codeInputField.text);
    }
}
