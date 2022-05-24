using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.Events;

[System.Serializable]
public class OnFileOpen: UnityEvent<List<string>> { }

public class OpenFileByWin32 : MonoBehaviour
{
    private string msg = string.Empty;
    private string titleOpen = "Открыть файл：";
    private string titleSave = "Сохранить файл：";
    private string saveAsName = string.Empty;

    private string[] ext = new string[] { "asm", "txt" };

    [SerializeField] private OnFileOpen onFileOpen = new OnFileOpen();

    public void OpenFile()
    {
        List<string> readedLines = new List<string>();

        string msg = string.Empty;
        string path = FileDialogForWindows.FileDialog(titleOpen, ext);
        if (!string.IsNullOrEmpty(path))
        {
            msg = "Открыть файл: " + path;
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                    readedLines.Add(reader.ReadLine());
            }
            Debug.Log($"FILE OPENED: LINES {readedLines.Count} first line: {readedLines[0]}");
            GlobalVariables._isEmulationAvailable = false;
            onFileOpen?.Invoke(readedLines);
        }
        else
            msg = "Путь некорректен！";
    }

    public void SaveFile()
    {
        saveAsName = "asm_code_" + DateTime.Now.ToString() + ".asm";
        string path = FileDialogForWindows.SaveDialog(titleSave, Path.Combine(Application.streamingAssetsPath, saveAsName));
        if (!string.IsNullOrEmpty(path))
            msg = "Сохранить файл: " + path;
        else
            msg = "Путь некорректен！";
    }
}
