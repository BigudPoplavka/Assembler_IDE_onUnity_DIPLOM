
using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;

public static class FileDialogForWindows
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class DialogConfig
    {
        #region Config Field
        public int structSize = 0;
        public IntPtr dlgOwner = IntPtr.Zero;
        public IntPtr instance = IntPtr.Zero;
        public String filter = null;
        public String customFilter = null;
        public int maxCustFilter = 0;
        public int filterIndex = 0;
        public String file = null;
        public int maxFile = 0;
        public String fileTitle = null;
        public int maxFileTitle = 0;
        public String initialDir = null;
        public String title = null;
        public int flags = 0;
        public short fileOffset = 0;
        public short fileExtension = 0;
        public String defExt = null;
        public IntPtr custData = IntPtr.Zero;
        public IntPtr hook = IntPtr.Zero;
        public String templateName = null;
        public IntPtr reservedPtr = IntPtr.Zero;
        public int reservedInt = 0;
        public int flagsEx = 0;
        #endregion
    }

    #region Win32API WRAP
    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    static extern bool GetOpenFileName([In, Out]DialogConfig dialog);  //GetOpenFileName
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    static extern bool GetSaveFileName([In, Out]DialogConfig dialog);  //GetSaveFileName
    #endregion

    static string Filter(params string[] filters)
    {
        return string.Join("\0", filters) + "\0";
    }

    public static string FileDialog(string title, params string[] extensions)
    {
        DialogConfig ofn = new DialogConfig();
        ofn.structSize = Marshal.SizeOf(ofn);

        var filters = new List<string>();
        filters.Add("All Files"); filters.Add("*.*");
        foreach (var ext in extensions)
        {
            filters.Add(ext); filters.Add("*" + ext);
        }
        ofn.filter = Filter(filters.ToArray());
        ofn.filterIndex = 2;
        ofn.file = new string(new char[256]);
        ofn.maxFile = ofn.file.Length;
        ofn.fileTitle = new string(new char[64]);
        ofn.maxFileTitle = ofn.fileTitle.Length;
        ofn.initialDir = UnityEngine.Application.dataPath;
        ofn.title = title;
        ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;
        ofn.dlgOwner= GetForegroundWindow(); 
        if (!GetOpenFileName(ofn))
        {
            return null;
        }
        return ofn.file;
    }

    public static string SaveDialog(string title, string path)
    {
        var extension = Path.GetExtension(path);
        DialogConfig ofn = new DialogConfig();
        ofn.structSize = Marshal.SizeOf(ofn);
        ofn.filter = Filter("All Files", "*.*", extension, "*" + extension);
        ofn.filterIndex = 2;
        var chars = new char[256];
        var it = Path.GetFileName(path).GetEnumerator();
        for (int i = 0; i < chars.Length && it.MoveNext(); ++i)
        {
            chars[i] = it.Current;
        }
        ofn.file = new string(chars);
        ofn.maxFile = ofn.file.Length;
        ofn.fileTitle = new string(new char[64]);
        ofn.maxFileTitle = ofn.fileTitle.Length;
        ofn.initialDir = Path.GetDirectoryName(path);
        ofn.title = title;
        ofn.flags = 0x00000002 | 0x00000004;
        ofn.dlgOwner= GetForegroundWindow(); 

        if (!GetSaveFileName(ofn))
        {
            return null;
        }
        return ofn.file;
    }
}

