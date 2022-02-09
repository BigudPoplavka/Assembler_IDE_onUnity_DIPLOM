using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class FilesConfig
{
    public static string EXT_MAIN = ".txt";

    #region Files info

    public static string OpenFileTitle = "Choose Assembler file";
    public static string SaveFileTitle = "Save Assembler file";
    public static string SaveFileDefaultName = "File" + DateTime.Now.Date.ToString();

    #endregion Files info

    #region Folders info

    public static string OpenFolderTitle = "Choose Folder";

    #endregion Folders info
}
