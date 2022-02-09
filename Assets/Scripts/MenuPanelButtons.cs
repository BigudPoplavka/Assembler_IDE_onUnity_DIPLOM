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


using Application = UnityEngine.Application;
using UnityEngine.Events;
using TMPro;

public class OnDebugStarted : UnityEvent<string> { }

public class MenuPanelButtons : MonoBehaviour
{
    [SerializeField] private DropdownMenu _FileMenu;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private OnDebugStarted _onDebugStarted = new OnDebugStarted();

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

    //public void File()
    //{
        
    //}

    public void FileNew()
    {
        
    }

    public void FileOpen()
    {
#if UNITY_EDITOR
        string fileName = EditorUtility.OpenFilePanel(FilesConfig.OpenFileTitle, CoreDirictories.defaultOpenFilePath, FilesConfig.EXT_MAIN);
#endif
        string filePath = string.Empty;

        //using (OpenFileDialog openFileDialog = new OpenFileDialog())
        //{
        //    openFileDialog.InitialDirectory = CoreDirictories.defaultOpenFilePath;
        //    if (openFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        filePath = openFileDialog.FileName;

        //    }
        //}


    }

    public void FileSave()
    {
#if UNITY_EDITOR
        EditorUtility.SaveFilePanel(FilesConfig.OpenFileTitle, FilesConfig.SaveFileDefaultName, CoreDirictories.defaultOpenFilePath, FilesConfig.EXT_MAIN);
#endif
        string filePath = string.Empty;

        //using (SaveFileDialog openFileDialog = new SaveFileDialog())
        //{
        //    openFileDialog.InitialDirectory = CoreDirictories.defaultOpenFilePath;
        //    if (openFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        filePath = openFileDialog.FileName;
        //    }
        //}
    }

    public void FileSaveAs()
    {
#if UNITY_EDITOR

#endif
        string filePath = string.Empty;

        //using (SaveFileDialog openFileDialog = new SaveFileDialog())
        //{
        //    openFileDialog.InitialDirectory = CoreDirictories.defaultOpenFilePath;
        //    if (openFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        filePath = openFileDialog.FileName;
        //    }
        //}
    }

    public void FolderOpen()
    {
#if UNITY_EDITOR
        EditorUtility.OpenFolderPanel(FilesConfig.OpenFolderTitle, CoreDirictories.defaultOpenFilePath, CoreDirictories.defaultOpenFilePath);
#endif
        string folderPath = string.Empty;

        //using (FolderBrowserDialog openFolderDialog = new FolderBrowserDialog())
        //{       
        //    if (openFolderDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        folderPath = openFolderDialog.SelectedPath;
        //    }
        //}
    }

    public void Settings()
    {
        _settingsPanel.SetActive(true);
    }

    public void Close()
    {
        Application.Quit();
    }

    public void Minimize()
    {
        
    }

    public void StartEmulation()
    {

    }

    public void Build()
    {
        
    }

    public void Debug()
    {
        EventsBase.onDebugStarted?.Invoke();

        Core.SetSyntaxAnalyzeResult(codeInputField.text);
    }
}
